using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EmergencyButton : MonoBehaviour
{
    public Material M_Int_Hover; 
    public Material M_Red; 

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
        
        if (other.tag == "GameController" && gameManager.stageInt == 1)
        {
            gameObject.GetComponent<Renderer>().material = M_Int_Hover;
            isOnPerimeter = true;


        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        
        if (other.tag == "GameController")
        {

            gameObject.GetComponent<Renderer>().material = M_Red;
            isOnPerimeter = false;
        }
        

    }

    private void Toggle(InputAction.CallbackContext context)
    {
        if (isOnPerimeter == true)
        {
            bool isActive = !gameObject.activeSelf;
            ButtonPush();
        }

    }

        public void ButtonPush()
    {
        Vector3 downPos = new Vector3(transform.localPosition.x, 0.6971f, 2.7406f);
        gameObject.transform.localPosition = downPos;
        gameObject.GetComponent<Outline>().enabled = false;
        gameObject.GetComponent<Renderer>().material = M_Red;
        gameObject.GetComponent<BoxCollider>().enabled = false;
        isOnPerimeter = true;
        gameManager.PlayGranted();
        gameManager.stageInt = 2;
        Debug.Log("Button pushed");
        Destroy(this);

    }
}
