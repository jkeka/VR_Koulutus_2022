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
    public GameObject tutMotorStartDummy;
    public GameObject tutMotorSocket;
    public GameObject tutMotor;
    public GameObject impactWrench;
    public GameObject impactWrenchDummy;
    public GameObject screen;
    public GameObject arrow;

    public GameObject rightController;
    public GameObject teleController;

    public GameObject turnProvider;

    public GameObject leftStick;
    public GameObject rightStick;

    public Transform startingPosition;
    public Transform playerStartingPosition;
    public Transform tutMotorStartPosition;

    public bool isMotorLifted;
    public bool accomplishedPlayed;

    public int tutStageInt;
    public int attachedBolts;

    //public InfoPanel infoPanel;

    public ToDoPanel toDoPanelScript;

    public GameObject toDoPanel;
    public GameObject infoPanel_Panel;



    public TMPro.TMP_Text taskText;
    public TMPro.TMP_Text infoText;



    void Awake()
    {
        startingPosition.position = playerStartingPosition.position;
        tutStageInt = 0;
        bolts.SetActive(false);
        tutMotorDummy.SetActive(false);
        tutMotorSocket.SetActive(false);
        //tutMotor.SetActive(true);

        toDoPanel.SetActive(true);
        
        audioSource = GetComponent<AudioSource>();
        arrow.SetActive(false);

        rightController.SetActive(false);
        teleController.SetActive(false);

        turnProvider.SetActive(false);

        isMotorLifted = false;
        accomplishedPlayed = false;

    }

    void Update()
    {
        infoPanel_Panel.SetActive(true);

        //Debug.Log("playerStartingPosition: " + playerStartingPosition.position.ToString());

        if (tutStageInt == 0)
        {
            if (startingPosition.position.x != playerStartingPosition.position.x || startingPosition.position.z != playerStartingPosition.position.z)
            {
                tutStageInt = 1;
                PlayGranted();
            }
        }

        if (tutMotor.transform.position.y > tutMotorStartPosition.position.y && tutStageInt == 2)
        {
            TutStageIntTo3();
        }

        if (tutStageInt == 0)
        {
            leftStick.SetActive(true);
            rightController.SetActive(false);
            teleController.SetActive(false);
        }

        if (tutStageInt == 1)
        {
            turnProvider.SetActive(true);

            leftStick.SetActive(false);
            rightStick.SetActive(true);
            rightController.SetActive(true);
            teleController.SetActive(true);

            infoText.text = "Press the right thumbstick forward and press the grip to teleport";
            toDoPanelScript.doneImagesList[tutStageInt].SetActive(true);


        }

        if (tutStageInt == 2)
        {
            rightStick.SetActive(false);
            tutMotorStartDummy.SetActive(false);
            tutMotor.SetActive(true);

            tutMotor.GetComponent<Outline>().enabled = true;
            arrow.SetActive(true);
            infoText.text = "Pick up the motor by pressing grip";
            toDoPanelScript.doneImagesList[tutStageInt].SetActive(true);
            tutMotor.SetActive(true);

        }

        if (tutStageInt == 3)
        {
            //tutMotorSocket.SetActive(true);

            tutMotorSocket.GetComponent<Outline>().enabled = true;

            infoText.text = "Set the motor to it's base";
            toDoPanelScript.doneImagesList[tutStageInt].SetActive(true);

        }

        if (tutStageInt == 4)
        {
            tutMotorSocket.SetActive(false);
            tutMotorDummy.SetActive(true);
            tutMotor.GetComponent<Outline>().enabled = false;

            tutMotorSocket.GetComponent<Outline>().enabled = false;
            impactWrenchDummy.SetActive(false);
            impactWrench.SetActive(true);
            impactWrench.GetComponent<Outline>().enabled = true;
            infoText.text = "Pick up the impact wrench";
            toDoPanelScript.doneImagesList[tutStageInt].SetActive(true);

        }


        if (tutStageInt == 5)
        {
            bolts.SetActive(true);

            infoText.text = "Tighten the bolts with impact wrench pressing the trigger";
            toDoPanelScript.doneImagesList[tutStageInt].SetActive(true);


        }

        if (tutStageInt == 6)
        {

            impactWrench.GetComponent<Outline>().enabled = false;
            infoText.text = "Tutorial complete! Restart or exit by using the screen";
            screen.GetComponent<Outline>().enabled = true;
            toDoPanelScript.doneImagesList[tutStageInt].SetActive(true);
            PlayAccomplished();

        }


        if (attachedBolts >= boltsList.Count)
        {
            tutStageInt = 6;

        }

    }

    public void TutStageIntTo2()
    {
        tutStageInt = 2;
    }

    public void TutStageIntTo3()
    {

            tutStageInt = 3;

        


    }

    public void TutStageIntTo4()
    {
        tutStageInt = 4;
    }

    public void TutStageIntTo5()
    {
        tutStageInt = 5;
    }


    public void PlayGranted()
    {
        audioSource.PlayOneShot(granted, 1f);
    }

    public void PlayGrantedTele()
    {
        if (tutStageInt == 1)
        {
            audioSource.PlayOneShot(granted, 1f);

        }
    }

    public void PlayAccomplished()
    {
        if (accomplishedPlayed == false)
        {
            audioSource.PlayOneShot(accomplished, 1f);
            accomplishedPlayed = true;
        }
    }

 
}
