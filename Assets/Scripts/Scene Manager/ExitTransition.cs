using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitTransition : MonoBehaviour
{
    [SerializeField] private string nextScene;
    
    private Timer timer;

    void Start()
    {
        timer = FindObjectOfType<Timer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Saving the completion time of the level before loading to the next scene
            if (timer != null)
                timer.SaveTime(); 
            SceneManager.LoadScene(nextScene);
        }
    }
}

