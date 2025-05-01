using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// CharacterController2D from the lab is not a built in functionality
// So we are using the RigidBody2D from unity instead
public class PlayerMovementViewModel
{
    private readonly Rigidbody2D rb;
    private readonly Animator animator;
    private readonly PlayerMovementModel model;

    private float horizontalMove = 0f;
    private float verticalMove = 0f;

    public PlayerMovementViewModel(Rigidbody2D rigidbody2D, Animator animatorComponent, PlayerMovementModel movementModel)
    {
        rb = rigidbody2D;
        animator = animatorComponent;
        model = movementModel;
    }

    public void HandleInput()
    {
        // Keyboard input
        horizontalMove = Input.GetAxisRaw("Horizontal");
        verticalMove = Input.GetAxisRaw("Vertical");
        
        if (model.MoveLeft) horizontalMove = -1;
        else if (model.MoveRight) horizontalMove = 1;
        else if (model.MoveUp || model.MoveDown) horizontalMove = 0; 

        if (model.MoveUp) verticalMove = 1;
        else if (model.MoveDown) verticalMove = -1;
        else if (model.MoveLeft || model.MoveRight) verticalMove = 0; 

        // Preventing the player from moving diagonally
        // Since we only have animations for vertical and horizontal movements
        // If we are currently moving either left or right then we cannot move up or down
        // It will always prioritize horizontal movements
        if (horizontalMove != 0)
        {
            verticalMove = 0;
        }

        model.MovementDirection = new Vector2(horizontalMove, verticalMove);

        HandleAnimations();
    }

    private void HandleAnimations()
    {
        // Animation handling section

        // Checking if we are moving in any direction
        bool isMoving = horizontalMove != 0 || verticalMove != 0;

        // Since we are using blend trees we can update the animator parameters we set in them to trigger the proper animation
        animator.SetFloat("horizontalMovement", model.MovementDirection.x);
        animator.SetFloat("verticalMovement", model.MovementDirection.y);

        // Ternary operator to set the speed of the character
        // The player is either going to be moving = 1 or idle = 0
        // Based of this information we can then display either the running animation or the idle
        animator.SetFloat("runSpeed", isMoving ? 1f : 0f);

        // Since we have more than 1 idle position we store the last direction when stopping
        if (isMoving)
        {
            model.LastMovementDirection = model.MovementDirection;
        }

        // As soon as we stop moving we will now display the proper idle animation based on the last direction
        if (!isMoving)
        {
            animator.SetFloat("horizontalMovement", model.LastMovementDirection.x);
            animator.SetFloat("verticalMovement", model.LastMovementDirection.y);
        }
    }

    public void MoveCharacter()
    {
        rb.velocity = model.MovementDirection * model.MovementSpeed;
    }
}

