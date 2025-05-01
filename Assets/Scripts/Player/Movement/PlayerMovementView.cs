using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementView : MonoBehaviour
{
    private PlayerMovementModel model;
    private PlayerMovementViewModel viewModel;

    void Start()
    {
        // Geting the Rigidbody2D component so that we can modify it
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        // Getting the animator component 
        Animator animator = GetComponent<Animator>();

        model = new PlayerMovementModel();
        viewModel = new PlayerMovementViewModel(rb, animator, model);
    }

    // Based on the UI button pressed we set the boolean values. 
    // So if we are pressing a button = true : false
    public void PressUp() => model.MoveUp = true;
    public void ReleaseUp() => model.MoveUp = false;

    public void PressDown() => model.MoveDown = true;
    public void ReleaseDown() => model.MoveDown = false;

    public void PressLeft() => model.MoveLeft = true;
    public void ReleaseLeft() => model.MoveLeft = false;

    public void PressRight() => model.MoveRight = true;
    public void ReleaseRight() => model.MoveRight = false;

    void Update()
    {
        viewModel.HandleInput();
    }

    private void FixedUpdate()
    {
        viewModel.MoveCharacter();
    }
}
