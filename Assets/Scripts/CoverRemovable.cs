using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CoverRemovable : MonoBehaviour
{
    
    //public Material M_Int_Hover;
    //public Material M_GreyMetal;

    public GameManager gameManager;

    public PlayAudioGranted playAudioGranted;


    private void Update()
    {
        if (gameManager.stageInt == 4)
        {
            gameObject.GetComponent<Outline>().enabled = true;
        }
    }

    /*
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

        if (other.tag == "GameController" && gameManager.stageInt == 4)
        {
            gameObject.GetComponent<Renderer>().material = M_Int_Hover;
            isOnPerimeter = true;


        }

    }
    private void OnTriggerExit(Collider other)
    {

        if (other.tag == "GameController")
        {

            gameObject.GetComponent<Renderer>().material = M_GreyMetal;
            isOnPerimeter = false;
        }


    }

    private void Toggle(InputAction.CallbackContext context)
    {
        if (isOnPerimeter == true)
        {
            bool isActive = !gameObject.activeSelf;
            RemoveCover();
        }

    }

    public void RemoveCover()
    {

        gameObject.GetComponent<Outline>().enabled = false;
        gameObject.GetComponent<Renderer>().material = M_GreyMetal;
        isOnPerimeter = true;
        //Destroy (this);
        gameObject.GetComponent<Rigidbody>().useGravity = true;
        playAudioGranted.PlayGranted();
        gameManager.stageInt = 5;

    }
    */

    public void CoverGrabbed()
    {
        gameObject.GetComponent<Outline>().enabled = false;

    }

    public void CoverAbandoned()
    {
        gameManager.stageInt = 5;
        playAudioGranted.PlayGranted();

    }

}
