using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CoverRemovable : MonoBehaviour
{
    
    public Material M_Int_Hover;
    public Material M_GreyMetal;

    public GameManager gameManager;


    public Rigidbody rb;


    private void Awake()
    {
        Physics.IgnoreLayerCollision(7, 16, true);
    }

    private void Update()
    {
        if (gameManager.stageInt == 4)
        {
            gameObject.GetComponent<Outline>().enabled = true;
            Physics.IgnoreLayerCollision(7, 16, false);

        }

        if (gameManager.stageInt == 7)
        {
            gameObject.GetComponent<Outline>().enabled = true;

        }
    }



    private void OnTriggerEnter(Collider other) //Toiminnallisuus, kun pelaaja menee sis��n 
    {

        if (other.tag == "GameController" && gameManager.stageInt == 4)
        {
            gameObject.GetComponent<Renderer>().material = M_Int_Hover;


        }

    }
    private void OnTriggerExit(Collider other)
    {

        if (other.tag == "GameController")
        {

            gameObject.GetComponent<Renderer>().material = M_GreyMetal;
        }


    }



    public void CoverGrabbed()
    {
        gameObject.GetComponent<Outline>().enabled = false;
        Debug.Log("Cover grapped");


    }

    public void CoverAbandoned()
    {

        if (gameManager.stageInt == 4)
        {
            gameManager.stageInt = 5;
            gameManager.PlayGranted();
            Debug.Log("Stage int to 5, cover dropped");
            rb.useGravity = true;
            gameObject.GetComponent<Outline>().enabled = false;
        }

        if (gameManager.stageInt == 7)
        {
            Debug.Log("Stage int to 8, cover set back");
            gameManager.stageInt = 8;
            gameManager.PlayGranted();

        }

    }



}
