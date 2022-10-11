using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InfoPanel : MonoBehaviour
{

    public GameObject menuPanel;

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
}
