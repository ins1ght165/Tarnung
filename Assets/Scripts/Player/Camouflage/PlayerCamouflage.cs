using UnityEngine;
using UnityEngine.UI;

public class PlayerCamouflage : MonoBehaviour
{
    private PlayerCamouflageViewModel viewModel;

    private Animator animator;
    private Rigidbody2D rb;
    private AudioSource audioSource;

    public Button cloakButton;
    public float cooldownDuration = 10f;
    public KeyCode activateKey = KeyCode.C;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();

        viewModel = new PlayerCamouflageViewModel(this, animator, rb, audioSource, cloakButton, cooldownDuration, GetComponent<SpriteRenderer>());
    }

    void Update()
    {
        viewModel.Update();

        // UI input or button press
        if (Input.GetKeyDown(activateKey))
        {
            viewModel.TriggerCamouflage();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        viewModel.OnTriggerEnter(other);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        viewModel.OnTriggerExit(other);
    }
    
    public void TriggerCamouflageFromButton()
    {
        viewModel.TriggerCamouflage();
    }
}