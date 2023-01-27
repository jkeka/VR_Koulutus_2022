using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;


public class RestartButton : MonoBehaviour
{
    public Material M_Int_Hover;
    public Material M_Base;

    public GameManager gameManager;


    public InputActionReference toggleReference = null;

    bool isOnPerimeter = false;

    void Awake()
    {
        toggleReference.action.started += Toggle;
    }

    private void OnDestroy()
    {
        toggleReference.action.started -= Toggle;

    }



    private void OnTriggerEnter(Collider other) //Toiminnallisuus, kun pelaaja menee sis��n 
    {

        if (other.tag == "GameController")
        {
            gameObject.GetComponent<Renderer>().material = M_Int_Hover;
            isOnPerimeter = true;


        }

    }
    private void OnTriggerExit(Collider other)
    {

        if (other.tag == "GameController")
        {

            gameObject.GetComponent<Renderer>().material = M_Base;
            isOnPerimeter = false;
        }


    }

    private void Toggle(InputAction.CallbackContext context)
    {
        if (isOnPerimeter == true)
        {
            bool isActive = !gameObject.activeSelf;
            RestartButtonPush();
        }

    }

    public void RestartButtonPush()
    {
        gameManager.PlayGranted();
        SceneManager.LoadScene("GameScene");

    }

}
