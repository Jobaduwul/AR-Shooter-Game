using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

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

    // New method to check if any marker is being tracked
    public bool IsTrackingAnyMarker()
    {
        foreach (var trackedImage in trackedImageManager.trackables)
        {
            if (trackedImage.trackingState == TrackingState.Tracking)
            {
                return true;
            }
        }
        return false;
    }
}
