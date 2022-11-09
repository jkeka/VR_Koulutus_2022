using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class MenuPanel : MonoBehaviour
{
    public GameObject infoPanel;


    public bool isMenuActive;

    public InputActionReference toggleReference = null;

    /*

    private void Awake()
    {
        gameObject.SetActive(false);
        isMenuActive = false;
        toggleReference.action.started += Toggle;

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
    */
}
