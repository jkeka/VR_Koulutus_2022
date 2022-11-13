using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class TutorialManager : MonoBehaviour
{
    public AudioClip granted;
    public AudioClip accomplished;
    AudioSource audioSource;

    public List<GameObject> boltsList;

    public GameObject bolts;
    public GameObject tutMotorDummy;
    public GameObject tutMotorSocket;
    public GameObject tutMotor;
    public GameObject impactWrench;
    public GameObject screen;

    public GameObject leftStick;
    public GameObject rightStick;



    public int tutStageInt;
    public int attachedBolts;

    //public InfoPanel infoPanel;

    public ToDoPanel toDoPanelScript;

   // public GameObject toDoPanel;
    public GameObject infoPanel_Panel;



    public TMPro.TMP_Text taskText;
    public TMPro.TMP_Text infoText;



    void Awake()
    {
        tutStageInt = 0;
        bolts.SetActive(false);
        tutMotorDummy.SetActive(false);
        tutMotorSocket.SetActive(false);

        infoPanel_Panel.SetActive(true);

        audioSource = GetComponent<AudioSource>();


    }

    void Update()
    {
        if (tutStageInt == 0)
        {
            leftStick.SetActive(true);

        }

        if (tutStageInt == 1)
        {
            leftStick.SetActive(false);
            rightStick.SetActive(true);

            infoText.text = "Press the left trigger forward and press grip to teleport";
            toDoPanelScript.doneImagesList[tutStageInt].SetActive(true);


        }

        if (tutStageInt == 2)
        {
            rightStick.SetActive(false);
            tutMotor.GetComponent<Outline>().enabled = true;

            infoText.text = "Pick up the motor by pressing grip";
            toDoPanelScript.doneImagesList[tutStageInt].SetActive(true);


        }

        if (tutStageInt == 3)
        {
            tutMotor.GetComponent<Outline>().enabled = false;
            tutMotorSocket.GetComponent<Outline>().enabled = true;

            infoText.text = "Set the motor to it's base";
            toDoPanelScript.doneImagesList[tutStageInt].SetActive(true);

        }

        if (tutStageInt == 4)
        {
            tutMotorSocket.SetActive(false);
            tutMotorDummy.SetActive(true);
            tutMotorSocket.GetComponent<Outline>().enabled = false;
            impactWrench.GetComponent<Outline>().enabled = true;
            infoText.text = "Pick up the impact wrench";
            toDoPanelScript.doneImagesList[tutStageInt].SetActive(true);

        }


        if (tutStageInt == 5)
        {
            bolts.SetActive(true);
            foreach (GameObject bolt in boltsList)
            {
                bolt.GetComponent<Outline>().enabled = true;
            }
            infoText.text = "Tighten the bolts with impact wrench pressing the trigger";
            toDoPanelScript.doneImagesList[tutStageInt].SetActive(true);


        }

        if (tutStageInt == 6)
        {

            impactWrench.GetComponent<Outline>().enabled = false;
            infoText.text = "Tutorial complete! Restart or exit by using the screen";
            screen.GetComponent<Outline>().enabled = false;
            toDoPanelScript.doneImagesList[tutStageInt].SetActive(true);


        }


        if (attachedBolts >= boltsList.Count)
        {
            tutStageInt = 6;

        }

    }

    public void TutStageIntTo1()
    {
        tutStageInt = 1;
    }

    public void PlayGranted()
    {
        audioSource.PlayOneShot(granted, 1f);
    }

    public void PlayAccomplished()
    {
        audioSource.PlayOneShot(accomplished, 1f);
    }
}
