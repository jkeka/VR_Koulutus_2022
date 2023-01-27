using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CoverBoltsAttach : MonoBehaviour
{

    public Material M_Int_Hover;
    public Material M_Floor;

    public GameManager gameManager;


    public InputActionReference toggleReference = null;

    bool isOnPerimeter = false;

    void Awake()
    {

        toggleReference.action.started += Toggle;

        gameObject.GetComponent<Renderer>().material = M_Floor;

    }


    private void OnDestroy()
    {

        toggleReference.action.started -= Toggle;

    }



    private void OnTriggerEnter(Collider other) //Toiminnallisuus, kun pelaaja menee sis��n 
    {

        if (other.tag == "Wrench" && gameManager.stageInt == 8)
        {
            gameManager.allBoltsFloorColor = false;
            gameObject.GetComponent<Renderer>().material = M_Int_Hover;
            isOnPerimeter = true;
        }

    }
    private void OnTriggerExit(Collider other)
    {

        if (other.tag == "Wrench" && gameManager.stageInt == 8)
        {

            gameObject.GetComponent<Renderer>().material = M_Floor;
            isOnPerimeter = false;
        }


    }

    private void Toggle(InputAction.CallbackContext context)
    {
        if (isOnPerimeter == true && gameManager.stageInt == 8)
        {
            bool isActive = !gameObject.activeSelf;
            AttachBolt();
            Debug.Log("Bolt clicked with wrench");
        }

    }

    public void AttachBolt()
    {
        gameManager.allBoltsLoose = false;
        Destroy(GetComponent<Outline>());

        gameObject.GetComponent<Renderer>().material = M_Floor;
        gameManager.PlayGranted();
        gameManager.attachedBolts++;
        Debug.Log("Bolt attached");

        Destroy(this);


    }

}
