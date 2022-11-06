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


    public InfoPanel infoPanel;

    public List<GameObject> boltsList;
    public GameObject wrench;
    public GameObject coverRemovable;

    void Awake()
    {
        timer = 0;
        endTime = 0;

        stageInt = 1;
        openedBolts = 0;
    }

    void Update()
    {

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
            //coverRemovable.GetComponent<BoxCollider>().enabled = true;
            //infoPanel.LevelFour();
        }

    }

}
