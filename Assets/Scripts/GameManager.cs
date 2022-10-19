using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public int stageInt;
    public static float totalTime;

    public InfoPanel infoPanel;

    public List<GameObject> interactableList;

    void Awake()
    {
        stageInt = 1;
    }

    void Update()
    {
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
        }

    }

}
