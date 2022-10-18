using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SafetyDoor : MonoBehaviour
{
    public Material M_Int_Hover; 
    public Material M_RobotOrange; 

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
        /*
        if (other.tag == "Kiintoavain" && gameManager.stageInt == 4)
        {
            gameObject.GetComponent<Renderer>().material = green;
            isOnPerimeter = true;


        }
        */
            gameObject.GetComponent<Renderer>().material = M_Int_Hover;
            isOnPerimeter = true;

    }
        private void OnTriggerExit(Collider other)
    {
        /*
        if (other.tag == "Kiintoavain")
        {
            gameObject.GetComponent<Renderer>().material = basic;
            isOnPerimeter = false;

        }
        */
            gameObject.GetComponent<Renderer>().material = M_RobotOrange;
            isOnPerimeter = false;
    }

        private void Toggle(InputAction.CallbackContext context)
    {
        if (isOnPerimeter == true)
        {
            bool isActive = !gameObject.activeSelf;
            OpenDoor();
        }

    }

        public void OpenDoor()
    {
        Vector3 openPos = new Vector3(transform.localRotation.x, -180, transform.localRotation.z);
        gameObject.transform.localPosition = openPos;
        gameObject.GetComponent<Outline>().enabled = false;
        gameObject.GetComponent<Renderer>().material = M_RobotOrange;
        Destroy (this);

    }
}
