using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class MarkerProcessor : MonoBehaviour
{
    private static int counter = 0;

    private UIManager uiManager;

    public ButtonController buttonController;

    private void Start()
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
        //counter++;
        uiManager.UpdateCounter1Text();
    }

    void UpdateCounter2()
    {
        //counter++;
        uiManager.UpdateCounter2Text();
    }

    void UpdateCounter3()
    {
        //counter++;
        uiManager.UpdateCounter3Text();
    }
}
