using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeArea : MonoBehaviour
{
    private RectTransform rectTransform;

    void OnEnable()
    {
        rectTransform = GetComponent<RectTransform>();
        UpdateSafeArea();
    }

    private void UpdateSafeArea()
    {
        // Get the safe area of the screen in pixels
        Rect safeArea = Screen.safeArea;

        // Convert the safe area to anchor positions
        Vector2 minAnchor = safeArea.position;
        Vector2 maxAnchor = safeArea.position + safeArea.size;

        // Convert the anchor positions to normalized values (0-1)
        minAnchor.x /= Screen.width;
        minAnchor.y /= Screen.height;
        maxAnchor.x /= Screen.width;
        maxAnchor.y /= Screen.height;

        // Set the anchor positions of the UI element to the safe area
        rectTransform.anchorMin = minAnchor;
        rectTransform.anchorMax = maxAnchor;
    }
}

