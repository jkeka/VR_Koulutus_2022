using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public GameObject QuitPanel;

    void Awake()
    {
        QuitPanel.SetActive(false);
    }

    public void GameScene()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void TutorialScene()
    {
        SceneManager.LoadScene("TutorialScene");
        //Debug.Log("Launch tutorial scene");
    }

    public void ExitGame()
    {
        QuitPanel.SetActive(true);
    }

    public void YesButton()
    {
        Application.Quit();
    }

    public void NoButton()
    {
        QuitPanel.SetActive(false);
    }

    public void Restart()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void FeedbackScene()
    {
        SceneManager.LoadScene("FeedbackScene");
    }

    public void MenuScene()
    {
        SceneManager.LoadScene("MenuScene");
    }

}
