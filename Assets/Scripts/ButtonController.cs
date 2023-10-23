using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public bool isButtonPressed = false;

    public void OnButtonPressed()
    {
        isButtonPressed = true;
    }

    public void OnButtonReleased()
    {
        isButtonPressed = false;
    }

    public bool IsButtonPressed()
    {
        return isButtonPressed;
    }
}
