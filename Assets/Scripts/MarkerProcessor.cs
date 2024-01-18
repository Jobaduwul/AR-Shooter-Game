using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class MarkerProcessor : MonoBehaviour
{
    public bool isMarkerTracked = false;

    private int counter = 0; // Individual counters for each marker

    public UIManager uiManager;

    public ButtonController buttonController;

    void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
    }

    public void ProcessMarker(ARTrackedImage trackedImage)
    {
        if (trackedImage.referenceImage.name == "marker1" && buttonController.isButtonPressed)
        {
            UpdateCounter1();
            buttonController.OnButtonReleased();
        }

        if (trackedImage.referenceImage.name == "marker2" && buttonController.isButtonPressed)
        {
            UpdateCounter2();
            buttonController.OnButtonReleased();
        }

        if (trackedImage.referenceImage.name == "marker3" && buttonController.isButtonPressed)
        {
            UpdateCounter3();
            buttonController.OnButtonReleased();
        }
    }

    void UpdateCounter1()
    {
        counter++;
        uiManager.UpdateCounter1Text(counter.ToString());
    }

    void UpdateCounter2()
    {
        counter++;
        uiManager.UpdateCounter2Text(counter.ToString());
    }

    void UpdateCounter3()
    {
        counter++;
        uiManager.UpdateCounter3Text(counter.ToString());
    }
}
