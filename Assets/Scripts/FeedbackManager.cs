using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class FeedbackManager : MonoBehaviour
{

    public TMPro.TMP_Text timeText;

    public GameObject X;
    public GameObject L;

    // Start is called before the first frame update
    void Start()
    {
        timeText.text = "TOTAL TIME: " + GameManager.endTime.ToString("F1") + " s";

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.wasCautious == true)
        {
            L.SetActive(true);
        }
        else
        {
            X.SetActive(true);
        }
    }
}
