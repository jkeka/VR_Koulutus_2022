using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{

    private float timer;
    public static float endTime;


    public int stageInt;
    public int openedBolts;

    public static float totalTime;

    //public Canvas xrCanvas;

    public InfoPanel infoPanel;

    public GameObject menuPanel;
    public GameObject menuControllerLeft;
    public GameObject menuControllerRight;

    public GameObject directControllerLeft;
    public GameObject directControllerRight;
    public GameObject teleController;

    public List<GameObject> boltsList;
    public GameObject wrench;
    public GameObject coverRemovable;
    public GameObject bearingExtractor;
    public GameObject bearingNew;
    public GameObject bearingBroken;

    void Awake()
    {
        timer = 0;
        endTime = 0;

        stageInt = 1;
        openedBolts = 0;

        menuControllerLeft.SetActive(false);
        menuControllerRight.SetActive(false);
    }

    void Update()
    {
        //MenuPanel
        //Debug.Log(Time.timeScale);

        if (menuPanel.activeSelf)
        {
            Time.timeScale = 0;
            menuControllerRight.SetActive(true);
            menuControllerLeft.SetActive(true);
            directControllerRight.SetActive(false);
            directControllerLeft.SetActive(false);
            teleController.SetActive(false);
            //xrCanvas.renderMode = RenderMode.WorldSpace;
        }
        else
        {
            Time.timeScale = 1;
            menuControllerRight.SetActive(false);
            menuControllerLeft.SetActive(false);
            directControllerRight.SetActive(true);
            directControllerLeft.SetActive(true);
            teleController.SetActive(true);
            //xrCanvas.renderMode = RenderMode.ScreenSpaceCamera;

        }

        //Game progression


        timer = timer + Time.deltaTime;

        if (stageInt == 1)
        {
            infoPanel.LevelOne();
        }

        if (stageInt == 2)
        {
            infoPanel.LevelTwo();
        }

        if(stageInt == 3)
        {
            infoPanel.LevelThree();
            /*
            for (int i = 0; i < boltsList.Count; i++)
            {
                boltsList[i].GetComponent<Outline>().enabled = true;
            }
            */

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
            wrench.GetComponent<Outline>().enabled = false;
            //infoPanel.LevelFour();
        }

        if (stageInt == 5)
        {
            bearingExtractor.GetComponent<Outline>().enabled = true;
            bearingBroken.GetComponent<Outline>().enabled = true;
            //infoPanel.LevelFive();
        }

    }

}
