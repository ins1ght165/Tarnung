using UnityEngine;
using UnityEngine.SceneManagement;
public class ConeVision : MonoBehaviour
{
    public bool ignoreCamouflage;
    void OnTriggerEnter2D(Collider2D other)
    {
        // Getting the bool value from the animator so we can check if the player is camouflaged
        Animator checker = other.GetComponent<Animator>();
        // To also prevent any bugs we will only trigger it if the game object is tagged as a player
        if (other.CompareTag("Player") && !checker.GetBool("isCamo") || other.CompareTag("Player") && ignoreCamouflage)
        {
            // Right before we swtich to the game over screen we will save the current scene name that we are on so we can restart
            PlayerPrefs.SetString("LastLevel", SceneManager.GetActiveScene().name);

            // Load Game over scene
            SceneManager.LoadScene("Game Over");
        }
    }
}





