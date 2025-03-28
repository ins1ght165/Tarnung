using UnityEngine;

public class PlayerCamouflage : MonoBehaviour
{
    private Animator animator;
    
    // Initializing different states that need to be tracked
    private bool isNearWall = false;
    private bool isCamouflaged = false;
    private bool isOnCooldown = false;
    
    // Activating the ability
    public KeyCode activateKey = KeyCode.C;
    
    // Cooldown and duration
    public float cooldownDuration = 10f;

    // Using the wall type to swap the appropiate sprite
    private WallType nearbyWall;
    
    private Rigidbody2D rb;
    private Vector2 lastPosition;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        lastPosition = rb.position;
    }

    void Update()
    {
        // If we meet the requirements we call the enable the camouflage
        if (Input.GetKeyDown(activateKey) && isNearWall && !isOnCooldown && !isCamouflaged)
        {
            enableCamouflage();
        }

        
        // Check if player is camouflaged AND moved from last frame
        if (isCamouflaged)
        {
            // IF we move we will disable the camouflage and start the cooldown
            Vector2 currentPosition = rb.position;
            // If the player's velocity is above a small threshold, treat it as movement
            if (rb.velocity.magnitude > 0.1f)
            {
                disableCamouflage();
            }


            lastPosition = currentPosition;
        }
        
    }

    void OnTriggerEnter2D(Collider2D other)
        {
            // When we are touching a wall we will get the wall type and if it is valid wall we will update that we are touching a valid wall. 
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
        void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Wall"))
            {
                isNearWall = false;
                nearbyWall = null;

                if (isCamouflaged)
                {
                    disableCamouflage();
                    StartCoroutine(Cooldown());
                }
            }
        }

        // Function to enable the camouflage and triggering a cooldown
        void enableCamouflage()
        {
            // For now for we only have one wall type
            if (nearbyWall != null && nearbyWall.camoType == "metal")
            {
                isCamouflaged = true;
                // Swapping the sprite
                animator.SetBool("isCamo", true);
            }
        }

        // Disabling the cammouflage function and reseting the sprite
        void disableCamouflage()
        {
            isCamouflaged = false;
            animator.SetBool("isCamo", false);
        }

        // Camouflage duration and cooldown timer
        System.Collections.IEnumerator Cooldown()
        {
            isOnCooldown = true;
            yield return new WaitForSeconds(cooldownDuration);
            isOnCooldown = false;
        }
    
}