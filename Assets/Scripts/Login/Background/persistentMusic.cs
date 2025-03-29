using UnityEngine;

public class PersistentMusic : MonoBehaviour
{
    private static PersistentMusic instance;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject); 
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject); 
        }
    }
}