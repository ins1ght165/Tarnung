using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    // CharacterController2D from the lab is not a built in functionality
    // So we are using the RigidBody2D from unity instead
    private Rigidbody2D rb; 
    private Vector2 movementDirection;
    [SerializeField] private float movementSpeed = 5f;
    
    private float horizontalMove = 0f;
    
    private float verticalMove = 0f;

    private Animator animator;

    private Vector2 lastMovementDirection = Vector2.down; 
    
    private bool moveUp, moveDown, moveLeft, moveRight;

    
    void Start()
    {
        // Geting the Rigidbody2D component so that we can modify it
        rb = GetComponent<Rigidbody2D>(); 
        
        // Getting the animator component 
        animator = GetComponent<Animator>();
        
        

    }
    
    public void PressUp() => moveUp = true;
    public void ReleaseUp() => moveUp = false;

    public void PressDown() => moveDown = true;
    public void ReleaseDown() => moveDown = false;

    public void PressLeft() => moveLeft = true;
    public void ReleaseLeft() => moveLeft = false;

    public void PressRight() => moveRight = true;
    public void ReleaseRight() => moveRight = false;

    // Update is called once per frame
    void Update()
    {
        
        // Keyboard fallback
        horizontalMove = Input.GetAxisRaw("Horizontal");
        verticalMove = Input.GetAxisRaw("Vertical");

        // Override with on-screen button input if active
        if (moveLeft) horizontalMove = -1;
        else if (moveRight) horizontalMove = 1;
        else if (moveUp || moveDown) horizontalMove = 0; // prevent diagonal

        if (moveUp) verticalMove = 1;
        else if (moveDown) verticalMove = -1;
        else if (moveLeft || moveRight) verticalMove = 0; // prevent diagonal

        
        // Preventing the player from moving diagonally
        // Since we only have animations for vertical and horizontal movements
        // If we are currently moving either left or right then we cannot move up or down
        // It will always prioritize horizontal movements
        if (horizontalMove != 0)
        {
            verticalMove = 0;
        }

        movementDirection = new Vector2(horizontalMove, verticalMove);
        
        //Animation handeling section
        
        // Checking if we are moving in any direction
        bool isMoving = horizontalMove != 0 || verticalMove != 0;
        
        // Since we are using blend trees we can update the animator parameters we set in them to trigger the proper animation
        animator.SetFloat("horizontalMovement", movementDirection.x);
        animator.SetFloat("verticalMovement", movementDirection.y);
        
        // Ternary operator to set the speed of the character
        // The player is either going to be moving = 1 or idle = 0
        // Based of this information we can then display either the running animation or the idle
        animator.SetFloat("runSpeed", isMoving ? 1f : 0f); 

        // Since we have more than 1 idle position we store the last direction when stopping
        if (isMoving)
        {
            lastMovementDirection = movementDirection;
        }

        // As soon as we stop moving we will now display the proper idle animation based on the last direction
        if (!isMoving)
        {
            animator.SetFloat("horizontalMovement", lastMovementDirection.x);
            animator.SetFloat("verticalMovement", lastMovementDirection.y);
        }
        
    }
    
    private void FixedUpdate()
    {
        rb.velocity = movementDirection * movementSpeed;
    }
}

