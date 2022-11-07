using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class FeedbackManager : MonoBehaviour
{

    public TMPro.TMP_Text timeText;

    // Start is called before the first frame update
    void Start()
    {
        timeText.text = "Time: " + GameManager.endTime;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
