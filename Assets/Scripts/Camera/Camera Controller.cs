using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Moving and zoom speed of the camera transition
    public float moveSpeed = 5f;
    public float zoomSpeed = 3f;

    // The targer position based on the center that we specify in the "CameraTrigger" script
    private Vector3 targetPosition;
    private float targetZoom;
    private bool shouldMove = false;

    private Camera cam;

    void Start()
    {
        // Initializing our camera and target position and zoom
        cam = GetComponent<Camera>();
        targetPosition = transform.position;
        targetZoom = cam.orthographicSize;
    }

    void Update()
    {
        if (shouldMove)
        {
            // This will smoothly move and zoom our camera to the target positon
            transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetZoom, zoomSpeed * Time.deltaTime);

            // Once we are close to the target we will stop moving and adjusting the zoom
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f && Mathf.Abs(cam.orthographicSize - targetZoom) < 0.05f)
                shouldMove = false;
        }
    }
    
    // Function that is used by "CameraTrigger" script to move the camera to the next room
    public void MoveToRoom(Vector3 roomCenter, float newZoom)
    {
        targetPosition = new Vector3(roomCenter.x, roomCenter.y, transform.position.z);
        targetZoom = newZoom;
        shouldMove = true;
    }
}

