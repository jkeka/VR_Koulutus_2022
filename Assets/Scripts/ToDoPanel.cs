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
