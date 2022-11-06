using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CoverBolts : MonoBehaviour
{
    
    public Material M_Int_Hover;
    public Material M_Floor;

    public GameManager gameManager;

    public PlayAudioGranted playAudioGranted;

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

        if (other.tag == "Wrench" && gameManager.stageInt == 3)
        {
            gameObject.GetComponent<Renderer>().material = M_Int_Hover;
            isOnPerimeter = true;
        }

    }
    private void OnTriggerExit(Collider other)
    {

        if (other.tag == "Wrench")
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
            OpenBolt();
            Debug.Log("Bolt clicked with wrench");
        }

    }

    public void OpenBolt()
    {

        playAudioGranted.PlayGranted();
        gameManager.openedBolts++;
        Debug.Log("Bolt opened");
        Destroy(this);
        gameObject.SetActive(false);


        //gameManager.stageInt = 5;

    }

}
