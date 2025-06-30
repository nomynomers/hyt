using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    public float parallaxMultiplier = 0.5f;
    private Transform cameraTransform;
    private Vector3 lastCameraPosition;
    
    void Start()
    {
        cameraTransform = Camera.main.transform;
        lastCameraPosition = cameraTransform.position;
    }
    
    void Update()
    {
        Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;
        transform.position += deltaMovement * parallaxMultiplier;
        lastCameraPosition = cameraTransform.position;
    }
}
