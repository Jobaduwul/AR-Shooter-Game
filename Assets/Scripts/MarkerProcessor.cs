using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class MarkerProcessor : MonoBehaviour
{
    public bool isMarkerTracked = false;

    private void Start()
    {

    }

    public bool ProcessMarker(ARTrackedImage trackedImage)
    {
        if (trackedImage.referenceImage.name == "marker1" || 
            trackedImage.referenceImage.name == "marker2" || 
            trackedImage.referenceImage.name == "marker3")
        {
            if (trackedImage.trackingState == UnityEngine.XR.ARSubsystems.TrackingState.Tracking)
            {
                isMarkerTracked = true;
                return true;
            }
            else
            {
                isMarkerTracked = false;
                return false;
            }
        }
        else{
            return false;
        }
    }
}