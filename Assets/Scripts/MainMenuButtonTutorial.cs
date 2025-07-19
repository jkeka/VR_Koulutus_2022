using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

// Handles interaction with the Main Menu button in the tutorial scene
// When the player hovers over the button and presses the assigned input key, 
// the game transitions to the Menu scene

public class MainMenuButtonTutorial : MonoBehaviour
{
    // Materials for visual feedback (hover and base)
    public Material M_Int_Hover;
    public Material M_Base;

    // Reference to the TutorialManager for audio or state management
    public TutorialManager tutorialManager;

    // Input action used to trigger the button
    public InputActionReference toggleReference = null;

    // Boolean to track if the player is within the interaction zone
    bool isOnPerimeter = false;

    // Subscribe to the input action when the object is initialized
    void Awake()
    {
        toggleReference.action.started += Toggle;
    }

    // Unsubscribe from the input action when the object is destroyed
    private void OnDestroy()
    {
        toggleReference.action.started -= Toggle;
    }

    // Triggered when a collider enters the button's collider
    private void OnTriggerEnter(Collider other) //Toiminnallisuus, kun pelaaja menee sis��n 
    {
        if (other.tag == "GameController")
        {
            // Change material to indicate hover state
            gameObject.GetComponent<Renderer>().material = M_Int_Hover;
            isOnPerimeter = true;
        }
    }

    // Triggered when a collider exits the button's collider
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "GameController")
        {
            // Revert material to base state
            gameObject.GetComponent<Renderer>().material = M_Base;
            isOnPerimeter = false;
        }
    }

    // Called when the assigned input action is triggered
    private void Toggle(InputAction.CallbackContext context)
    {
        if (isOnPerimeter == true)
        {
            bool isActive = !gameObject.activeSelf;
            MainMenuButtonPush();
        }
    }

    // Handles the main menu button action
    public void MainMenuButtonPush()
    {
        // Play success sound and visual feedback
        tutorialManager.PlayGranted();

        // Load the main menu scene
        SceneManager.LoadScene("MenuScene");
    }
}
