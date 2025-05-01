using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCamouflageViewModel
{
    private bool isNearWall = false;
    private bool isCamouflaged = false;
    private bool isOnCooldown = false;

    private WallType nearbyWall;
    private Rigidbody2D rb;
    private Animator animator;
    private AudioSource audioSource;
    private Button cloakButton;
    private MonoBehaviour host;
    private float cooldownDuration;
    private SpriteRenderer spriteRenderer;


    public PlayerCamouflageViewModel(MonoBehaviour host, Animator animator, Rigidbody2D rb, AudioSource audioSource, Button cloakButton, float cooldownDuration, SpriteRenderer spriteRenderer)
    {
        this.host = host;
        this.animator = animator;
        this.rb = rb;
        this.audioSource = audioSource;
        this.cloakButton = cloakButton;
        this.cooldownDuration = cooldownDuration;
        this.spriteRenderer = spriteRenderer;
    }
    
    // Changing the player opacity when entering or exiting camouflage
    private void SetPlayerOpacity(float alpha)
    {
        if (spriteRenderer != null)
        {
            Color c = spriteRenderer.color;
            c.a = alpha;
            spriteRenderer.color = c;
        }
    }


    // If we meet the requirements we call the enableCamouflage
    // Also made it into a public method so we can call this function using a button in the UI
    public void TriggerCamouflage()
    {
        if (isNearWall && !isOnCooldown && !isCamouflaged)
        {
            EnableCamouflage();
            SetButtonOpacity(1f);
            SetPlayerOpacity(0.3f);
        }
    }

    public void Update()
    {
        // Check if player is camouflaged AND moved from last frame
        if (isCamouflaged)
        {
            // If the player's velocity is above a small threshold, treat it as movement
            if (rb.velocity.magnitude > 0.1f)
            {
                DisableCamouflage();
            }
        }
    }

    public void OnTriggerEnter(Collider2D other)
    {
        // When we are touching a wall we will get the wall type
        // If it is a valid wall we update that we are touching a valid wall
        if (other.CompareTag("Wall"))
        {
            nearbyWall = other.GetComponent<WallType>();
            if (nearbyWall != null)
            {
                isNearWall = true;
            }
        }
    }

    // Function of when we are no longer touching a wall
    // Simply disabling all the different states and disabling the camouflage ability
    public void OnTriggerExit(Collider2D other)
    {
        if (other.CompareTag("Wall"))
        {
            isNearWall = false;
            nearbyWall = null;

            if (isCamouflaged)
            {
                DisableCamouflage();
                host.StartCoroutine(Cooldown());
            }
        }
    }

    // Function to enable the camouflage and triggering a cooldown
    void EnableCamouflage()
    {
        // For now we only have one wall type
        if (nearbyWall != null /*&& nearbyWall.camoType == "metal"*/)
        {
            isCamouflaged = true;
            // Swapping the sprite
            animator.SetBool("isCamo", true);
        }

        // Triggering a rumble effect if vibration is enabled
        if (PlayerPrefs.GetInt("vibration", 1) == 1)
        {
            Handheld.Vibrate();
        }

        // Playing a sound effect when entering camouflage
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }

    // Disabling the camouflage function and resetting the sprite
    void DisableCamouflage()
    {
        isCamouflaged = false;
        animator.SetBool("isCamo", false);
        SetButtonOpacity(0.6f);
        SetPlayerOpacity(1f);
    }

    // Camouflage duration and cooldown timer
    IEnumerator Cooldown()
    {
        isOnCooldown = true;
        yield return new WaitForSeconds(cooldownDuration);
        isOnCooldown = false;
    }

    // Helper function to set the opacity of the UI button
    void SetButtonOpacity(float alpha)
    {
        if (cloakButton != null)
        {
            Image img = cloakButton.GetComponent<Image>();
            if (img != null)
            {
                Color c = img.color;
                c.a = alpha;
                img.color = c;
            }
        }
    }
}
