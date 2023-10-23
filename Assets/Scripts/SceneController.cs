using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void LoadGameScene()
    {
        SceneManager.LoadScene(1); // Load your gameplay scene
        Debug.Log("Scene 1 loaded");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0); // Load your main menu scene
        Debug.Log("Scene 0 loaded");
    }

    public void ExitGame()
    {
        Application.Quit(); // Exit the application (for standalone builds)
    }
}
