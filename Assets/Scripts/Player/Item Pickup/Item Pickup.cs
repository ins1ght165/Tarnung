using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ItemPickup : MonoBehaviour
{
    public Transform player;              
    public float triggerDistance = 2f;     
    public GameObject triggerToEnable;     

    private SpriteRenderer spriteRenderer;
    private bool hasDisappeared = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (!hasDisappeared)
        {
            float distance = Vector2.Distance(transform.position, player.position);

            if (distance < triggerDistance)
            {
                spriteRenderer.enabled = false;     
                hasDisappeared = true;           

                if (triggerToEnable != null)
                {
                    triggerToEnable.SetActive(true); 
                }
            }
        }
    }
}
