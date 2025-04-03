using UnityEngine;

public class WallColliderSwitcher : MonoBehaviour
{
    public Collider2D frontCollider;
    public Collider2D backCollider;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            frontCollider.enabled = false;
            backCollider.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            frontCollider.enabled = true;
            backCollider.enabled = false;
        }
    }
}