using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeSwing : MonoBehaviour
{
    
    public float maxAngle = 45f;     
    public float speed = 2f;         

    void Update()
    {
        float angle = Mathf.Sin(Time.time * speed) * maxAngle;

        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}

