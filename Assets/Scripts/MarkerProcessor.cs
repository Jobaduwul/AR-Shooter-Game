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
        if (trackedImage.referenceImage.name == "marker" && buttonController.isButtonPressed)
        {
            IncrementCounter();
            buttonController.OnButtonReleased();
        }
    }

    void IncrementCounter()
    {
        counter++;
        uiManager.UpdateCounterText(counter);
    }
}
