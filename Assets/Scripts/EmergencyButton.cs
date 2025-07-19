using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Handles interaction logic for an emergency button
// Highlights on player proximity, reacts to input, and advances game stage

public class EmergencyButton : MonoBehaviour
{
    // Material shown when the player is near (highlighted)
    public Material M_Int_Hover; 
    
    // Default red material when idle or after being pressed
    public Material M_Red; 

    // Reference to the GameManager to control game state and feedback
    public GameManager gameManager;

    // Input System reference for player interaction input
    public InputActionReference toggleReference = null;

    // Tracks whether the player is within interaction range
    bool isOnPerimeter = false;

    // Subscribes to the input action on Awake
    void Awake()
    {
        toggleReference.action.started += Toggle;
    }

    // Unsubscribes from the input action to avoid memory leaks
    private void OnDestroy()
    {
        toggleReference.action.started -= Toggle;

    }
    
    // Triggered when player enters the interaction area
    // Highlights the button if game is in correct stage
    private void OnTriggerEnter(Collider other) //Toiminnallisuus, kun pelaaja menee sis��n 
    {  
        if (other.tag == "GameController" && gameManager.stageInt == 1)
        {
            gameObject.GetComponent<Renderer>().material = M_Int_Hover;
            isOnPerimeter = true;
        }
    }

    // Triggered when player exits the interaction area
    // Resets button appearance and disables interaction flag
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "GameController")
        {
            gameObject.GetComponent<Renderer>().material = M_Red;
            isOnPerimeter = false;
        }
    }
    
    // Triggered by input system when player presses the interaction key
    // If in range, triggers the button's press logic
    private void Toggle(InputAction.CallbackContext context)
    {
        if (isOnPerimeter == true)
        {
            bool isActive = !gameObject.activeSelf;
            ButtonPush();
        }
    }

    // Handles visual and logical behavior when button is pressed:
    // - Moves button down
    // - Disables outline and collider
    // - Updates materials and stage
    public void ButtonPush()
    {
        Vector3 downPos = new Vector3(transform.localPosition.x, 0.6971f, 2.7406f);
        gameObject.transform.localPosition = downPos;
        gameObject.GetComponent<Outline>().enabled = false;
        gameObject.GetComponent<Renderer>().material = M_Red;
        gameObject.GetComponent<BoxCollider>().enabled = false;
        isOnPerimeter = true;
        gameManager.PlayGranted();
        gameManager.stageInt = 2;
        Debug.Log("Button pushed");
        Destroy(this);
    }
}
