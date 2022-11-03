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
        }

    }

    public void OpenBolt()
    {
        //Vector3 downPos = new Vector3(transform.localPosition.x, 0.025f, transform.localPosition.z);
        //gameObject.transform.localPosition = downPos;
        //gameObject.GetComponent<Outline>().enabled = false;
        //gameObject.GetComponent<Renderer>().material = M_Floor;
        //gameObject.GetComponent<BoxCollider>().enabled = false;
        //isOnPerimeter = true;
        playAudioGranted.PlayGranted();
        gameManager.openedBolts++;
        gameObject.SetActive(false);

        //gameManager.stageInt = 5;

    }
    
}
