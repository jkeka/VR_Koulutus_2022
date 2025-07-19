using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Handles player interaction with a door button
// Changes material on proximity and triggers stage progression when pressed

public class DoorButton : MonoBehaviour
{
    // Material shown when the button is highlighted (player nearby)
    public Material M_Int_Hover; 

    // Default button material (blue)
    public Material M_Blue; 

    // Reference to the GameManager for stage control
    public GameManager gameManager;

    // Separate component for playing audio feedback (granted sound)
    public PlayAudioGranted playAudioGranted;

    // Input action reference for interaction key/button
    public InputActionReference toggleReference = null;

    // Whether the player is within interaction range
    bool isOnPerimeter = false;

    // Subscribes to input event when the object awakens
    void Awake()
    {
        toggleReference.action.started += Toggle;
    }
    
    // Unsubscribes from input event to avoid memory leaks
    private void OnDestroy()
    {
        toggleReference.action.started -= Toggle;
    }

    // Called when player enters interaction area
    // If at correct game stage, highlights the button
    private void OnTriggerEnter(Collider other) //Toiminnallisuus, kun pelaaja menee sis��n 
    {
        if (other.tag == "GameController" && gameManager.stageInt == 1)
        {
            gameObject.GetComponent<Renderer>().material = M_Int_Hover;
            isOnPerimeter = true;
        }
    }
    
    // Called when player exits interaction area
    // Resets material and disables interaction
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "GameController")
        {
            gameObject.GetComponent<Renderer>().material = M_Blue;
            isOnPerimeter = false;
        }
    }

    // Called when player presses the interaction input
    // If within range, triggers button press logic
    private void Toggle(InputAction.CallbackContext context)
    {
        if (isOnPerimeter == true)
        {
            bool isActive = !gameObject.activeSelf;
            ButtonPush();
        }
    }

    // Handles visual and game state changes when the button is pressed:
    // - Lowers the button
    // - Disables outline
    // - Resets material
    // - Advances game stage
    // - Plays audio
    public void ButtonPush()
    {
        Vector3 downPos = new Vector3(transform.localPosition.x, 0.6971f, 2.7406f);
        gameObject.transform.localPosition = downPos;
        gameObject.GetComponent<Outline>().enabled = false;
        gameObject.GetComponent<Renderer>().material = M_Blue;
        Destroy (this);
        gameManager.PlayGranted();
        gameManager.stageInt = 2;
    }
}
