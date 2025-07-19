using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// Manages the feedback display at the end of the game
// Shows total time and whether the player acted cautiously
public class FeedbackManager : MonoBehaviour
{
    // Reference to the TMP UI text displaying total time
    public TMPro.TMP_Text timeText;

    // Feedback indicators (X = careless, L = cautious)
    public GameObject X;
    public GameObject L;

    // Start is called before the first frame update
    void Start()
    {
        // Displays total time and sets it to string with one decimal precision
        timeText.text = "TOTAL TIME: " + GameManager.endTime.ToString("F1") + " s";
    }

    // Update is called once per frame
    // Shows feedback based on whether player was cautious
    void Update()
    {
        if (GameManager.wasCautious == true)
        {
            L.SetActive(true);    // Player was careful
        }
        else
        {
            X.SetActive(true);    // Player was not careful
        }
    }
}
