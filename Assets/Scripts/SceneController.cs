using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void LoadGameScene()
    {
        SceneManager.LoadScene(1); // Load your gameplay scene
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0); // Load your main menu scene
    }

    public void ExitGame()
    {
        Application.Quit(); // Exit the application (for standalone builds)
    }
}
