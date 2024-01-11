using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TMP_Text counterText;

    public void UpdateCounter1Text()
    {
        counterText.text = "Shot Player 1";
    }

    public void UpdateCounter2Text()
    {
        counterText.text = "Shot Player 2";
    }

    public void UpdateCounter3Text()
    {
        counterText.text = "Shot Player 3";
    }
}