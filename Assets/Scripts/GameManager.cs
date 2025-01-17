using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{

    private float timer;
    public static float endTime;

    public float outlineWidth;

    public AudioClip granted;
    public AudioClip accomplished;
    public AudioClip warning;
    AudioSource audioSource;

    public int stageInt;
    public int openedBolts;
    public int attachedBolts;


    public bool allBoltsLoose;
    public bool allBoltsFloorColor;
    public bool timerOn;
    public bool coverOnSocket;
    public static bool wasCautious;

    //public Canvas xrCanvas;

    public InfoPanel infoPanel;

    public ToDoPanel toDoPanelScript;

    public GameObject toDoPanel;
    public GameObject winPanel;
    public GameObject infoPanel_Panel;
    public GameObject menuControllerLeft;
    public GameObject menuControllerRight;

    public GameObject directControllerLeft;
    public GameObject directControllerRight;
    public GameObject teleController;

    public List<GameObject> boltsList;
    public GameObject wrench;
    public GameObject coverRemovable;
    public GameObject coverRemovableDummy;
    public GameObject bearingExtractor;
    public GameObject bearingNew;
    public GameObject bearingNewSocket;
    public GameObject bearingBroken;
    public GameObject coverRemovableSocket;

    public GameObject menuScreen;

    public GameObject menuScreenBasics;
    public GameObject menuScreenFeedback;

    public Transform bearingSocketTransform;
    public Transform coverRemovableTransform;

    public Rigidbody bearingNewRB;
    public Rigidbody coverRemovableRB;

    public Material M_Socket;
    public Material M_Floor;

    void Awake()
    {
        timer = 0;
        endTime = 0;

        stageInt = 1;
        openedBolts = 0;
        attachedBolts = 0;

        menuControllerLeft.SetActive(false);
        menuControllerRight.SetActive(false);

        bearingNewSocket.SetActive(false);
        coverRemovableSocket.SetActive(false);
        coverRemovableDummy.SetActive(false);

        winPanel.SetActive(false);
        menuScreenFeedback.SetActive(false);

        toDoPanel.SetActive(true);


        audioSource = GetComponent<AudioSource>();

        allBoltsLoose = true;
        allBoltsFloorColor = true;
        wasCautious = true;

        timerOn = true;

        coverOnSocket = false;

    }

    void Update()
    {

        infoPanel_Panel.SetActive(true);
        //Game progression

        if (timerOn == true)
        {
            timer = timer + Time.deltaTime;

        }

        if (stageInt == 1)
        {
            infoPanel.LevelOne();
        }

        if (stageInt == 2)
        {
            infoPanel.LevelTwo();
            toDoPanelScript.doneImagesList[stageInt].SetActive(true);

        }

        if (stageInt == 3)
        {
            infoPanel.LevelThree();
            toDoPanelScript.doneImagesList[stageInt].SetActive(true);

            foreach (GameObject bolt in boltsList)
            {
                bolt.GetComponent<Outline>().enabled = true;
            }

            wrench.GetComponent<Outline>().enabled = true;

            if (openedBolts >= boltsList.Count)
            {
                stageInt = 4;
            }
        }

        if (stageInt == 4)
        {
            toDoPanelScript.doneImagesList[stageInt].SetActive(true);

            wrench.GetComponent<Outline>().enabled = false;
            infoPanel.LevelFour();
        }

        if (stageInt == 5)
        {
            toDoPanelScript.doneImagesList[stageInt].SetActive(true);

            bearingExtractor.GetComponent<Outline>().enabled = true;
            bearingBroken.GetComponent<Outline>().enabled = true;
            infoPanel.LevelFive();
        }

        if (stageInt == 6)
        {
            toDoPanelScript.doneImagesList[stageInt].SetActive(true);

            bearingExtractor.GetComponent<Outline>().enabled = false;
            bearingNew.GetComponent<Outline>().enabled = true;
            bearingNewSocket.SetActive(true);
            infoPanel.LevelSix();
        }

        if (stageInt == 7)
        {
            toDoPanelScript.doneImagesList[stageInt].SetActive(true);

            bearingNew.GetComponent<Outline>().enabled = false;
            bearingNew.transform.position = bearingSocketTransform.position;
            bearingNew.transform.rotation = bearingSocketTransform.rotation;

            bearingNewRB.useGravity = false;
            bearingNewRB.isKinematic = true;

            bearingNew.GetComponent<Collider>().enabled = false;
            bearingNewSocket.SetActive(false);
            coverRemovableSocket.SetActive(true);

            infoPanel.LevelSeven();
        }

        if (stageInt == 8)
        {
            toDoPanelScript.doneImagesList[stageInt].SetActive(true);

            coverRemovable.GetComponent<Outline>().enabled = false;
            wrench.GetComponent<Outline>().enabled = true;


            coverRemovable.transform.position = coverRemovableTransform.position;
            coverRemovable.transform.rotation = coverRemovableTransform.rotation;
            //coverRemovableRB.useGravity = false;
            //coverRemovableRB.constraints = RigidbodyConstraints.FreezePosition;
            //coverRemovableRB.isKinematic = true;

            coverRemovable.SetActive(false);
            coverRemovableDummy.SetActive(true);

            coverRemovableSocket.SetActive(false);
            coverRemovable.GetComponent<Collider>().enabled = false;


            foreach (GameObject bolt in boltsList)
            {
                bolt.SetActive(true);
            }

            if (allBoltsFloorColor == true)
            {
                foreach (GameObject bolt in boltsList)
                {
                    bolt.GetComponent<Renderer>().material = M_Floor;
                }
            }


            if (allBoltsLoose == true)
            {
                foreach (GameObject bolt in boltsList)
                {
                    bolt.GetComponent<Outline>().enabled = true;
                }
            }


            if (attachedBolts >= boltsList.Count)
            {
                stageInt = 9;
                PlayAccomplished();
                infoPanel_Panel.SetActive(true);
                toDoPanel.SetActive(true);
            }
            infoPanel.LevelEight();
        }

        if (stageInt == 9)
        {
            toDoPanelScript.doneImagesList[stageInt].SetActive(true);

            wrench.GetComponent<Outline>().enabled = false;
            infoPanel.LevelNine();

            menuScreen.GetComponent<Outline>().enabled = true;
            menuScreenBasics.SetActive(false);
            menuScreenFeedback.SetActive(true);


            timerOn = false;
            endTime = timer;
        }


    }

    public void StageInt7()
    {
        stageInt = 7;
    }

    public void StageInt8()
    {
        stageInt = 8;
    }


    public void PlayGranted()
    {
        audioSource.PlayOneShot(granted, 1f);
    }

    public void PlayAccomplished()
    {
        audioSource.PlayOneShot(accomplished, 1f);
    }

    public void PlayWarning()
    {
        audioSource.PlayOneShot(warning, 1f);
    }
    /*
    public void CoverOnSocketToTrue()
    {
        coverOnSocket = true;
    }

    public void CoverOnSocketToFalse()
    {
        coverOnSocket = false;

    }
    */
}
