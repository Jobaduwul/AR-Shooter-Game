using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class MarkerProcessor : MonoBehaviour
{
    private static int counter = 0;

    private UIManager uiManager; // Reference to UIManager

    public ButtonController buttonController;

    private void Start()
    {
        // Find the UIManager component in your scene
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
        // Call the non-static method on the instance of UIManager
        uiManager.UpdateCounterText(counter);
    }
}
