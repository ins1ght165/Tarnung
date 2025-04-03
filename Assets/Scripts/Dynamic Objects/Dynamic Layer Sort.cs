using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Making it a requirement that the object we are trying to adjust has a sprite render component
// If not then we will automaitcally create one
[RequireComponent(typeof(SpriteRenderer))]

public class DynamicLayerSort : MonoBehaviour
{

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        // Fetching the spriterender component so we can adjust it
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void LateUpdate()
    {
        // Essentially we want to move the character behind the an object or in front depending on the y value.
        // We want objects that are located lower on the screen to be displayed in the front and vice versa.
        // By multiplying the y value by -100 even the slightest changes should be accounted for so we have a smooth experience
        spriteRenderer.sortingOrder = Mathf.RoundToInt(transform.position.y * -100);
    }
}
