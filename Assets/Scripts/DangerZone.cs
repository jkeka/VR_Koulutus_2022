using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Handles player interaction with a danger zone
// Activates visual warnings and updates game state

public class DangerZone : MonoBehaviour
{
    // Reference to the GameManager for accessing UI and game state
    public GameManager gameManager;
    
    // UI panel shown when player enters the danger zone
    public GameObject warningPanel;
    
    // Directional arrow pointing toward danger or objective
    public GameObject warningArrow;

    // Initializes the warning elements as inactive (false) at game start.
    private void Awake()
    {
        warningArrow.SetActive(false);
        warningPanel.SetActive(false);
    }

    // Ensures that the to-do panel is hidden while warning is active.
    private void Update()
    {
        if (warningPanel.activeSelf)
        {
            gameManager.toDoPanel.SetActive(false);
        }
    }

    // Called when the player enters the danger zone
    // Activates warning UI and sound, and updates caution state
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            warningArrow.SetActive(true);
            warningPanel.SetActive(true);

            GameManager.wasCautious = false;
            gameManager.PlayWarning();

            gameManager.infoPanel_Panel.SetActive(false);
            gameManager.toDoPanel.SetActive(false);
        }
    }

    // Called when the player exits the danger zone
    // Deactivates warning UI elements
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            warningArrow.SetActive(false);
            warningPanel.SetActive(false);
        }
    }
}
