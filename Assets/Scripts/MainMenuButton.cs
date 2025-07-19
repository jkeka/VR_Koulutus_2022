using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

// Handles the interaction logic for the "Main Menu" button
// When the player hovers over and presses the input action near the button, it triggers a scene change
public class MainMenuButton : MonoBehaviour
{
    // Materials to indicate interaction feedback (hover and base states)
    public Material M_Int_Hover;
    public Material M_Base;
    
    // Reference to the GameManager for stage tracking and audio playback
    public GameManager gameManager;

    // Input action reference for toggling the button
    public InputActionReference toggleReference = null;

    // Tracks if the player is within the interaction range
    bool isOnPerimeter = false;

    // Subscribe to the input action when the object is initialized
    void Awake()
    {
        toggleReference.action.started += Toggle;
    }

    // Unsubscribe from the input action to avoid memory leaks
    private void OnDestroy()
    {
        toggleReference.action.started -= Toggle;
    }

    // Detect when the player enters the button's collider
    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "GameController")
        {
            gameObject.GetComponent<Renderer>().material = M_Int_Hover;
            isOnPerimeter = true;
        }
    }

    // Detect when the player exits the button's collider
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "GameController")
        {
            gameObject.GetComponent<Renderer>().material = M_Base;
            isOnPerimeter = false;
        }
    }

    // Called when the input action is triggered
    private void Toggle(InputAction.CallbackContext context)
    {
        if (isOnPerimeter == true)
        {
            bool isActive = !gameObject.activeSelf;
            MainMenuButtonPush();
        }
    }

    // Logic that runs when the button is pressed
    public void MainMenuButtonPush()
    {
        // Play a sound or visual confirmation from the GameManager
        gameManager.PlayGranted();
        
        // Load the appropriate scene based on progress
        SceneManager.LoadScene("MenuScene");

        // If task is completed, go to feedback instead
        if (gameManager.stageInt == 9)
        {
            SceneManager.LoadScene("FeedbackScene");
        }
    }
}
