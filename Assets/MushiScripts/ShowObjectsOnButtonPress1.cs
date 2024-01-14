using UnityEngine;

public class ShowObjectsOnButtonPress : MonoBehaviour
{
    public GameObject objectToShowHide; 
    private bool isObjectVisible = false;

    public void ToggleVisibility()
    {
        if (objectToShowHide != null)
        {
            isObjectVisible = !isObjectVisible;
            objectToShowHide.SetActive(isObjectVisible);
            Debug.Log("Visibility toggled. New state: " + isObjectVisible);
        }
        else
        {
            Debug.LogError("Object to show/hide is not assigned!");
        }
    }


    void Start()
    {
        if (objectToShowHide != null)
        {
            objectToShowHide.SetActive(false);
        }
    }
}
