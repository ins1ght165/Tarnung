using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    // Manually setting the center of the room and zoom of the camera
    public Vector2 roomCenter;
    public float roomZoom = 5f; 
    
    // If the player enters a specific zone we call the moving camera functionality from the other script
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CameraController cam = Camera.main.GetComponent<CameraController>();
            cam.MoveToRoom(roomCenter, roomZoom);
        }
    }
}


