using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

// Handles restart button behavior in the tutorial scene
public class RestartButtonTutorial : MonoBehaviour
{
    // Material shown when player hovers over the button
    public Material M_Int_Hover;

    // Default material for the button    
    public Material M_Base;

    // Reference to the tutorial manager to trigger audio or game logic
    public TutorialManager tutorialManager;

    // Input action reference for detecting interaction input (e.g., trigger press)
    public InputActionReference toggleReference = null;

    // Flag indicating whether the player is within the button's interaction area
    bool isOnPerimeter = false;

    // Subscribe to input action when the object awakens
    void Awake()
    {
        toggleReference.action.started += Toggle;
    }

    // Unsubscribe from input action when the object is destroyed
    private void OnDestroy()
    {
        toggleReference.action.started -= Toggle;
    }

    // Triggered when another collider enters this object's trigger collider
    private void OnTriggerEnter(Collider other) //Toiminnallisuus, kun pelaaja menee sis��n 
    {
        // If the player enters the area, show the hover material and allow interaction
        if (other.tag == "GameController")
        {
            gameObject.GetComponent<Renderer>().material = M_Int_Hover;
            isOnPerimeter = true;
        }
    }

    // Triggered when another collider exits this object's trigger collider
    private void OnTriggerExit(Collider other)
    {
        // If the player leaves the area, revert to base material and disable interaction
        if (other.tag == "GameController")
        {
            gameObject.GetComponent<Renderer>().material = M_Base;
            isOnPerimeter = false;
        }
    }

    // Called when the input action occurs
    private void Toggle(InputAction.CallbackContext context)
    {
        // Only restart if the player is in the interaction zone
        if (isOnPerimeter == true)
        {
            bool isActive = !gameObject.activeSelf;
            RestartButtonPush();
        }
    }

    // Restart the tutorial by playing feedback and reloading the scene
    public void RestartButtonPush()
    {
        tutorialManager.PlayGranted();    // Play audio feedback
        SceneManager.LoadScene("TutorialScene");    // Reload current scene
    }
}
