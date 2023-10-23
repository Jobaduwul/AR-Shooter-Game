using UnityEngine;
using UnityEngine.XR.ARFoundation;
using TMPro;
using UnityEngine.XR.ARSubsystems;

public class ARMarkerDetection : MonoBehaviour
{
    public TMP_Text counterText;

    private int counter = 0;
    private ARTrackedImage trackedImage;
    private bool isButtonPressed = false;

    void Start()
    {
        ARTrackedImageManager trackedImageManager = FindObjectOfType<ARTrackedImageManager>();

        if (trackedImageManager != null)
        {
            trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
        }
        else
        {
            Debug.LogError("AR Tracked Image Manager not found. Make sure it's in your scene.");
        }
    }

    void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var trackedImage in eventArgs.added)
        {
            UpdateMarkerDetection(trackedImage);
        }

        foreach (var trackedImage in eventArgs.updated)
        {
            UpdateMarkerDetection(trackedImage);
        }
    }

    void UpdateMarkerDetection(ARTrackedImage trackedImage)
    {
        if (trackedImage.referenceImage.name == "marker" && isButtonPressed)
        {
            IncrementCounter();
            OnButtonReleased();
        }
        UpdateCounterText();
    }

    void IncrementCounter()
    {
        counter++;
    }

    void UpdateCounterText()
    {
        counterText.text = counter.ToString();
    }

    public void OnButtonPressed()
    {
        isButtonPressed = true;
    }

    public void OnButtonReleased()
    {
        isButtonPressed = false;
    }
}
