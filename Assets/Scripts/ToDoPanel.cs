using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class ToDoPanel : MonoBehaviour
{
    public GameObject infoPanel;

    public GameManager gameManager;

    public List<GameObject> doneImagesList;

    public bool isMenuActive;

    public InputActionReference toggleReference = null;

    

    private void Awake()
    {
        gameObject.SetActive(false);

        isMenuActive = false;
        toggleReference.action.started += Toggle;

        foreach (GameObject images in doneImagesList)
        {
            images.SetActive(false);
        }

    }

    private void Update()
    {
        if (gameManager.stageInt == 2)
        {
            doneImagesList[gameManager.stageInt].SetActive(true);
        }

        if (gameManager.stageInt == 3)
        {
            doneImagesList[gameManager.stageInt].SetActive(true);
        }

        if (gameManager.stageInt == 4)
        {
            doneImagesList[gameManager.stageInt].SetActive(true);
        }

        if (gameManager.stageInt == 5)
        {
            doneImagesList[gameManager.stageInt].SetActive(true);
        }

        if (gameManager.stageInt == 6)
        {
            doneImagesList[gameManager.stageInt].SetActive(true);
        }

        if (gameManager.stageInt == 7)
        {
            doneImagesList[gameManager.stageInt].SetActive(true);
        }

        if (gameManager.stageInt == 8)
        {
            doneImagesList[gameManager.stageInt].SetActive(true);
        }

        if (gameManager.stageInt == 9)
        {
            doneImagesList[gameManager.stageInt].SetActive(true);
        }
    }

    private void OnDestroy()
    {
        toggleReference.action.started -= Toggle;
    }



    private void Toggle(InputAction.CallbackContext context)
    {
        bool isActive = !gameObject.activeSelf;
        gameObject.SetActive(isActive);

        infoPanel.SetActive(false);

    }
    
}
