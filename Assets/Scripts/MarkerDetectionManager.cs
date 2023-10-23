using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class MarkerDetectionManager : MonoBehaviour
{
    public ARTrackedImageManager trackedImageManager;
    public MarkerProcessor markerProcessor; // Reference to MarkerProcessor

    void Start()
    {
        trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var trackedImage in eventArgs.added)
        {
            // Forward to MarkerProcessor.
            markerProcessor.ProcessMarker(trackedImage);
        }

        foreach (var trackedImage in eventArgs.updated)
        {
            // Forward to MarkerProcessor.
            markerProcessor.ProcessMarker(trackedImage);
        }
    }
}
