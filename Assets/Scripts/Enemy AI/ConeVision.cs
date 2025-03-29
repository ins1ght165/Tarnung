using UnityEngine;
public class ConeVision : MonoBehaviour
{
    
    void OnTriggerEnter2D(Collider2D other)
    {   
        // Getting the bool value from the animator so we can check if the player is camouflaged
        Animator checker = other.GetComponent<Animator>();
        // To also prevent any bugs we will only trigger it if the game object is tagged as a player
        if (other.CompareTag("Player") && !checker.GetBool("isCamo"))
        {     // Switching scenes when spotted 
            UnityEngine.SceneManagement.SceneManager.LoadScene("Game Over");
        }
    }
}