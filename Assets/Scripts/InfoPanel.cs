using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class InfoPanel : MonoBehaviour
{

    public GameObject menuPanel;
    public TMPro.TMP_Text taskText;    
    public TMPro.TMP_Text infoText;

    public InputActionReference toggleReference = null;

    private void Awake()
    {
        gameObject.SetActive(false);

        toggleReference.action.started += Toggle;
    }

    private void OnDestroy()
    {
        toggleReference.action.started -= Toggle;
    }



    private void Toggle(InputAction.CallbackContext context)
    {
        bool isActive = !gameObject.activeSelf;
        gameObject.SetActive(isActive);
        menuPanel.SetActive(false);
    }

    public void LevelOne()
    {
        taskText.text = "Task: Changing bearing of an robot arm";
    }

    public void LevelTwo()
    {
        taskText.text = "Push the emergency switch of the robot";
        infoText.text = "As safety measure, power must be shut down before approaching robot";
    }

    public void LevelThree()
    {
        taskText.text = "Remove the bolts of bearing cover plate";
        infoText.text = "Wrench can be found from the tool box";
    }

}
