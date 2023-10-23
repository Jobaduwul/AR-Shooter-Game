using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TMP_Text counterText;

    public void UpdateCounterText(int counter)
    {
        counterText.text = counter.ToString();
    }
}