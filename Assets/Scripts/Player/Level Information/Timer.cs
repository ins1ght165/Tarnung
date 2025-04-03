using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private float timer;

    void Update()
    {
        timer += Time.deltaTime;
    }

    public float GetTime()
    {
        return timer;
    }

    public void SaveTime()
    {
        LevelStats.completionTime = timer;
    }
}


