using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartLevel : MonoBehaviour
{
    public void restartLevel()
    {
        // To allow the restart scene to work for all scenes we will load the scene that got saved as soon as the player got spotted
        string lastLevel = PlayerPrefs.GetString("LastLevel", "LevelOne"); 
        SceneManager.LoadScene(lastLevel);
    }
}