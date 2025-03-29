using UnityEngine;

public class enemyAnimation : MonoBehaviour
{
    
    // Declaring our vairables
    private Rigidbody2D rb; 
    private Animator animator;
    
    // keeping track of the last position and direction
    private Vector2 lastPosition;
    private Vector2 lastMovementDirection = Vector2.down;
    
    public Transform visionCone;
    public Transform coneSprite; 


    void Start()
    {
        // As soon as we start we will assign the necessary values
        rb = GetComponent<Rigidbody2D>(); 
        animator = GetComponent<Animator>();
        // Initial position
        lastPosition = rb.position; 
    }

    void Update()
    {
        // Get movement direction from delta position
        Vector2 currentPosition = rb.position;
        
        // Adopting a difference approach in tracking moving direction as we no longer rely on input
        // We will simply check every frame if there was any difference in the movement. If not then we have the value 0,0
        Vector2 movementDirection = (currentPosition - lastPosition);
        
        // Different approach of checking for movement since we are not tracking input anymore as they move by themselves
        bool isMoving = movementDirection.magnitude > 0.01f;
        
        // Below is the same approach as the player movement script for updating 
        animator.SetFloat("horizontalMovement", movementDirection.x);
        animator.SetFloat("verticalMovement", movementDirection.y);
        animator.SetFloat("runSpeed", isMoving ? 1f : 0f);
        
        if (isMoving)
        {
            lastMovementDirection = movementDirection;
            
            
            // Cone Vision update
            if (Mathf.Abs(lastMovementDirection.x) > Mathf.Abs(lastMovementDirection.y))
            {
                if (lastMovementDirection.x > 0)
                {   // Right facing
                    // Using Unity's built in rotation system = Quaternion.Euler 
                    // We always have to ad +90 to the angle since our initial position of the cone is facing downwards. (default would be right facing)
                    visionCone.rotation = Quaternion.Euler(0f, 0f, 0f + 90f); 
                    // Manaually adjusted the coordinates to fit it perfecrtly in front of the gameobject
                    coneSprite.localPosition = new Vector3(0.8f, -3f, 0f);
                }
                else
                {
                    // Using the same logic for every direction:
                    //Left facing
                    visionCone.rotation = Quaternion.Euler(0f, 0f, 180f + 90f); 
                    coneSprite.localPosition = new Vector3(-0.8f, -3f, 0f);
                }
            }
            else
            {
               // For up or downward movements
                if (lastMovementDirection.y > 0)
                {
                    // Upwards
                    visionCone.rotation = Quaternion.Euler(0f, 0f, 90f + 90f); 
                    coneSprite.localPosition = new Vector3(0f, -2f, 0f);
                }
                else
                {
                    // Downwards
                    visionCone.rotation = Quaternion.Euler(0f, 0f, 270f + 90f); 
                    coneSprite.localPosition = new Vector3(0f, -3.5f, 0f);
                }
            }




        }
        else
        {
            // Show idle in last stance based on the last direction
            animator.SetFloat("horizontalMovement", lastMovementDirection.x);
            animator.SetFloat("verticalMovement", lastMovementDirection.y);
        }

        lastPosition = currentPosition;

    }
}