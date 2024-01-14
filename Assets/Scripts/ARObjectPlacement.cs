using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARObjectPlacement : MonoBehaviour
{
    public ARSessionOrigin arSessionOrigin;
    public Vector3 offset;

    void Start()
    {
        if (arSessionOrigin == null)
        {
            arSessionOrigin = FindObjectOfType<ARSessionOrigin>();
        }

        if (arSessionOrigin == null)
        {
            Debug.LogError("AR Session Origin not found. Make sure it's in your scene.");
        }
    }

    void Update()
    {
        if (arSessionOrigin != null)
        {
            Vector3 cameraPosition = arSessionOrigin.camera.transform.position;
            Quaternion cameraRotation = arSessionOrigin.camera.transform.rotation;

            Vector3 newPosition = cameraPosition + cameraRotation * offset;

            transform.position = newPosition;

            transform.rotation = cameraRotation;
        }
    }
}