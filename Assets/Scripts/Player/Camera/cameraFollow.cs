using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float smoothing = 5f;
    [SerializeField] private Transform minBoundary;
    [SerializeField] private Transform maxBoundary;

    private Vector3 offset;

    void Start()
    {
        offset = transform.position - target.position;
    }

    public void LateUpdate()
    {
        Vector3 targetCamPos = target.position + offset;
        targetCamPos.x = Mathf.Clamp(targetCamPos.x, minBoundary.position.x, maxBoundary.position.x);
        targetCamPos.y = Mathf.Clamp(targetCamPos.y, minBoundary.position.y, maxBoundary.position.y);

        // Keep the z-axis 
        targetCamPos.z = transform.position.z;

        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
    }
}