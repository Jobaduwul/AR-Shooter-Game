using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARObjectPlacement : MonoBehaviour
{
    public ARSessionOrigin arSessionOrigin;
    public Transform playerCamera;
    public Vector3 offset;
    public Vector3 rotationOffset;
    public AudioSource shootingSound;
    public ParticleSystem shootingParticles;

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

    public void ShootSound()

    {
        shootingSound.Play();
    }

    public void ShootEffect()
    {
        shootingParticles.Play();
    }

    void Update()
    {
        if (arSessionOrigin != null)
        {
            transform.position = playerCamera.position + playerCamera.rotation * offset;
            transform.rotation = playerCamera.rotation * Quaternion.Euler(rotationOffset);
        }
    }
}