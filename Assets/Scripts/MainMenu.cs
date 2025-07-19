using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Handles main menu UI actions such as scene loading and quit confirmation
public class MainMenu : MonoBehaviour
{
    // Reference to the quit confirmation panel UI
    public GameObject QuitPanel;

    // Called when the script instance is loaded
    // Ensures the quit confirmation panel is hidden at start
    void Awake()
    {
        QuitPanel.SetActive(false);
    }

    // Loads the main game scene
    public void GameScene()
    {
        SceneManager.LoadScene("GameScene");
    }

    // Loads the tutorial scene
    public void TutorialScene()
    {
        SceneManager.LoadScene("TutorialScene");
    }

    // Shows the quit confirmation panel
    public void ExitGame()
    {
        QuitPanel.SetActive(true);
    }
    
    // Confirms quitting the application
    public void YesButton()
    {
        Application.Quit();
    }

    // Cancels quitting and hides the quit confirmation panel
    public void NoButton()
    {
        QuitPanel.SetActive(false);
    }
    
    // Restarts the game scene.
    public void Restart()
    {
        SceneManager.LoadScene("GameScene");
    }

    // Loads the feedback scene
    public void FeedbackScene()
    {
        SceneManager.LoadScene("FeedbackScene");
    }

    // Loads the main menu scene
    public void MenuScene()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
