using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SafetyDoor : MonoBehaviour
{
    public GameManager gameManager;

    //public GameObject doorFrame;
    public GameObject stopper;

    public Material M_Int_Hover; 
    public Material M_RobotOrange;


    private Vector3 direction = new Vector3(0f, -180f, 0f);

    public InputActionReference toggleReference = null;

    bool isOnPerimeter = false;
    bool doorOpened = false;

    void Awake()
    {
        toggleReference.action.started += Toggle;
    }

    private void OnDestroy()
    {
        toggleReference.action.started -= Toggle;

    }

    private void Update()
    {
        if (gameManager.stageInt == 2)
        {
            gameObject.GetComponent<Outline>().enabled = true;
        }


        if (doorOpened)
        {
            Quaternion targetRotation = Quaternion.Euler(direction);
            gameObject.transform.rotation = Quaternion.Lerp(this.transform.rotation, targetRotation, Time.deltaTime * 1.5f);
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
            /*
            foreach (Collider c in GetComponents<Collider>())
            {
                c.enabled = false;
            }
            */

        }

    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "GameController" && gameManager.stageInt == 2)
        {
            gameObject.GetComponent<Renderer>().material = M_Int_Hover;
            isOnPerimeter = true;
            //Debug.Log("Safety door entered");
        }

        if (other.tag == "DoorStopper")
        {
            foreach (Collider c in GetComponents<Collider>())
            {
                c.enabled = false;
            }
            //Destroy(this);
        }

    }
    private void OnTriggerExit(Collider other)
    {
        
        if (other.tag == "GameController")
        {
            gameObject.GetComponent<Renderer>().material = M_RobotOrange;
            isOnPerimeter = false;

        }
        

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

        doorOpened = true;
        //isOnPerimeter = true;
        gameObject.GetComponent<Outline>().enabled = false;
        gameObject.GetComponent<Renderer>().material = M_RobotOrange;
        gameManager.PlayGranted();
        gameManager.stageInt = 3;
        Debug.Log("Safety door opened");
    }
}
