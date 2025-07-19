using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// Handles interaction logic for cover bolts in the game
/// Allows player to open bolts using a wrench during a specific game stage

public class CoverBolts : MonoBehaviour
{
    // Material shown when bolt is interactable (player is close with correct tool)
    public Material M_Int_Hover;

    // Default material when not highlighted
    public Material M_Floor;

    // Reference to the GameManager controlling game state and progression
    public GameManager gameManager;

    // Input action reference for the interaction button
    public InputActionReference toggleReference = null;
    
    // Indicates whether the player is in range to interact
    bool isOnPerimeter = false;

    // Subscribes to the input action when the object awakens
    void Awake()
    {
        toggleReference.action.started += Toggle;
    }

    // Cleans up the event subscription to avoid memory leaks
    private void OnDestroy()
    {
        toggleReference.action.started -= Toggle;
    }

    // Triggered when a collider (wrench collider) enters the interaction area
    // If the player has the correct tool and game is in correct stage, highlight the bolt
    private void OnTriggerEnter(Collider other) 
    {
        if (other.tag == "Wrench" && gameManager.stageInt == 3)
        {
            gameObject.GetComponent<Renderer>().material = M_Int_Hover;
            isOnPerimeter = true;
        }
    }

    // Triggered when the player/tool exits the interaction area
    // Resets the bolt's appearance
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Wrench")
        {
            gameObject.GetComponent<Renderer>().material = M_Floor;
            isOnPerimeter = false;
        }
    }

    // Called when the interaction input is triggered
    // If the player is in range, opens the bolt
    private void Toggle(InputAction.CallbackContext context)
    {
        if (isOnPerimeter == true)
        {
            bool isActive = !gameObject.activeSelf;
            OpenBolt();
            Debug.Log("Bolt clicked with wrench");
        }

    }

    // Handles the logic for opening a bolt:
    // - Plays feedback sound/animation
    // - Increments openedBolts -counter
    // - Disables this bolt
    public void OpenBolt()
    {
        gameManager.PlayGranted();
        gameManager.openedBolts++;
        Debug.Log("Bolt opened");
        Destroy(this);
        gameObject.SetActive(false);
    }
}
