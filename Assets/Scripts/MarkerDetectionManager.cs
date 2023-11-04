using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class MarkerDetectionManager : MonoBehaviour
{
    public ARTrackedImageManager trackedImageManager;
    public MarkerProcessor markerProcessor;

    void Start()
    {
        trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var trackedImage in eventArgs.added)
        {
            markerProcessor.ProcessMarker(trackedImage);
        }

        foreach (var trackedImage in eventArgs.updated)
        {
            markerProcessor.ProcessMarker(trackedImage);
        }
    }
}
