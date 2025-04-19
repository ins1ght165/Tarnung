/*
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
*/

using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PolygonCollider2D))]
public class ConeVision : MonoBehaviour
{
    public LayerMask visionMask;
    public float visionAngle = 90f;
    public int rayCount = 15;
    [SerializeField] private float coneLength = 4f;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        Animator checker = other.GetComponent<Animator>();
        if (checker != null && checker.GetBool("isCamo")) return;

        Physics2D.queriesHitTriggers = true;

        Vector2 origin = transform.position;
        Vector2 forward = -transform.up;

        float angleStep = visionAngle / (rayCount - 1);
        float startAngle = -visionAngle / 2f;

        for (int i = 0; i < rayCount; i++)
        {
            float currentAngle = startAngle + i * angleStep;
            Vector2 direction = Quaternion.Euler(0, 0, currentAngle) * forward;

            RaycastHit2D hit = Physics2D.Raycast(origin, direction, coneLength, visionMask);
            Debug.DrawRay(origin, direction * coneLength, Color.red, 1f);

            if (hit.collider != null)
            {
                Debug.Log($"👁️ Ray {i} hit: {hit.collider.name}");

                if (hit.collider.CompareTag("Player"))
                {
                    Debug.Log("🎯 Player detected!");
                    PlayerPrefs.SetString("LastLevel", SceneManager.GetActiveScene().name);
                    SceneManager.LoadScene("Game Over");
                    return;
                }
            }
            else
            {
                Debug.Log($"❌ Ray {i} hit nothing.");
            }
        }

        Debug.Log("🧱 Player is blocked from all cone rays.");
    }
}



