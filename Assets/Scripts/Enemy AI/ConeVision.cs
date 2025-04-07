/*

using UnityEngine;
using UnityEngine.SceneManagement;
public class ConeVision : MonoBehaviour
{
   // Assigning a layer mask that will identify what is of wall type
    [SerializeField] private LayerMask wallLayer;

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if we are hitting the player
        if (other.CompareTag("Player"))
        {
            // Getting the bool value from the animator so we can check if the player is camouflaged
            Animator checker = other.GetComponent<Animator>();

            // Only proceed if the player is not camouflaged
            if (checker != null && !checker.GetBool("isCamo"))
            {
                Vector2 origin = transform.position;
                Vector2 target = other.transform.position;
                Vector2 direction = target - origin;

                RaycastHit2D hit = Physics2D.Raycast(origin, direction.normalized, direction.magnitude);

                if (hit.collider != null)
                {
                    // Check if we hit the wall before hitting the player
                    if (hit.collider.CompareTag("Wall"))
                    {
                        // Player is behind a wall — do nothing
                        return;
                    }
                }
                // Save current level name before switching scenes
                PlayerPrefs.SetString("LastLevel", SceneManager.GetActiveScene().name);

                // Load Game over scene
                SceneManager.LoadScene("Game Over");
            }
        }
    }
}
*/


using UnityEngine;
using UnityEngine.SceneManagement;
public class ConeVision : MonoBehaviour
{
    
    void OnTriggerEnter2D(Collider2D other)
    {   
        // Getting the bool value from the animator so we can check if the player is camouflaged
        Animator checker = other.GetComponent<Animator>();
        // To also prevent any bugs we will only trigger it if the game object is tagged as a player
        if (other.CompareTag("Player") && !checker.GetBool("isCamo"))
        {   
            // Right before we swtich to the game over screen we will save the current scene name that we are on so we can restart
            PlayerPrefs.SetString("LastLevel", SceneManager.GetActiveScene().name);

            // Load Game over scene
            SceneManager.LoadScene("Game Over");
        }
    }
}