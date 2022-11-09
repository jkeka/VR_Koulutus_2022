using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class InfoPanel : MonoBehaviour
{
    
    public GameManager gameManager;
    public TMPro.TMP_Text stageIntText;


    //public GameObject menuPanel;
    public TMPro.TMP_Text taskText;    
    public TMPro.TMP_Text infoText;


    public InputActionReference toggleReference = null;

    private void Awake()
    {
        gameObject.SetActive(true);

        toggleReference.action.started += Toggle;

    }

    private void OnDestroy()
    {
        toggleReference.action.started -= Toggle;
    }

    private void Update()
    {
        stageIntText.text = gameManager.stageInt.ToString();
    }



    private void Toggle(InputAction.CallbackContext context)
    {
        bool isActive = !gameObject.activeSelf;
        gameObject.SetActive(isActive);
        //menuPanel.SetActive(false);
    }

    public void LevelOne()
    {

        taskText.text = "Push down the emergency switch of the robot";
        infoText.text = "Power must be shut down before entering the robot cell";

    }

    public void LevelTwo()
    {

        taskText.text = "Open the cell door";
        infoText.text = "";
    }

    public void LevelThree()
    {
        taskText.text = "Remove the bolts of bearing cover plate with impact wrench";
        infoText.text = "Impact wrench can be found from the table next to robot";
    }

    public void LevelFour()
    {
        taskText.text = "Remove the covering plate";
        infoText.text = "";
    }

    public void LevelFive()
    {
        taskText.text = "Remove the old bearing with an bearing extractor";
        infoText.text = "Extractor can be found from the table next to robot";
    }

    public void LevelSix()
    {
        taskText.text = "Insert new bearing in to the axle";
        infoText.text = "New bearing can be found from the table next to robot";
    }

    public void LevelSeven()
    {
        taskText.text = "Insert covering plate back to it's position";
        infoText.text = "";
    }

    public void LevelEight()
    {
        taskText.text = "Tighten the bolts with the impact wrench";
        infoText.text = "";
    }

    public void LevelNine()
    {
        taskText.text = "Task accomplished, well done!";
        infoText.text = "Select restart or go to feedback";
    }

}
