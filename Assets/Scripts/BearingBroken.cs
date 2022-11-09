using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BearingBroken : MonoBehaviour
{
    
    public Material M_Int_Hover;
    public Material M_Floor;

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

        if (other.tag == "BearingExtractor" && gameManager.stageInt == 5)
        {
            gameObject.GetComponent<Renderer>().material = M_Int_Hover;
            isOnPerimeter = true;
        }

    }
    private void OnTriggerExit(Collider other)
    {

        if (other.tag == "BearingExtractor")
        {

            gameObject.GetComponent<Renderer>().material = M_Floor;
            isOnPerimeter = false;
        }


    }

    private void Toggle(InputAction.CallbackContext context)
    {
        if (isOnPerimeter == true)
        {
            bool isActive = !gameObject.activeSelf;
            ExtractBearing();
            Debug.Log("Bearing clicked with extractor");
        }

    }

    public void ExtractBearing()
    {

        gameManager.PlayGranted();
        Debug.Log("Bearing extracted");
        gameObject.SetActive(false);
        gameManager.stageInt = 6;
        Destroy(this);

    }

}
