using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementModel
{
    public float MovementSpeed = 5f;
    public Vector2 MovementDirection;
    public Vector2 LastMovementDirection = Vector2.down;

    public bool MoveUp, MoveDown, MoveLeft, MoveRight;
}
