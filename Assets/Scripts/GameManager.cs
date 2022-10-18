using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public int levelInt;
    public static float totalTime;

    public InfoPanel infoPanel;

    public List<GameObject> interactableList;

    void Awake()
    {
        levelInt = 1;
        infoPanel.LevelOne();
    }

    void Update()
    {
        if(levelInt == 2)
        {
            infoPanel.LevelTwo();
        }

        if(levelInt == 3)
        {
            infoPanel.LevelThree();
        }

    }

}
