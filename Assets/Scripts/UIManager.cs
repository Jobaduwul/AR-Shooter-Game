using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TMP_Text counterText;

    public void UpdateCounter1Text(string counter)
    {
        counterText.text = counter;
    }

    public void UpdateCounter2Text(string counter)
    {
        counterText.text = counter;
    }

    public void UpdateCounter3Text(string counter)
    {
        counterText.text = counter;
    }
}
