using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene("ItemCollecting");
    }

    public void LoadMainMenu()
    {
        // Replace "MainMenu" with your actual main menu scene name
        SceneManager.LoadScene("MainMenuUI");
        // OR use the build index if you prefer
        // SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Debug.Log("The Game is closed now (not here in the unity editor).");
        Application.Quit();
    }
}
