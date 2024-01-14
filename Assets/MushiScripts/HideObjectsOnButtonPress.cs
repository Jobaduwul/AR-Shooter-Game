using UnityEngine;
using UnityEngine.UI;

public class HideObjectsOnButtonPress : MonoBehaviour
{
    public GameObject uiElementToHide;

    public void HideUIElement()
    {
        if (uiElementToHide != null)
        {
            uiElementToHide.SetActive(false);
        }
        else
        {
            Debug.LogError("UI Element to hide is not assigned!");
        }
    }
}
