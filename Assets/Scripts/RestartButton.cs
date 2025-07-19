using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

// This script handles the interaction logic for a Restart button in the scene
// When the player enters the trigger area and activates the input, the game restarts
public class RestartButton : MonoBehaviour
{
    // Materials to show visual feedback when hovered or idle
    public Material M_Int_Hover;
    public Material M_Base;

    // Reference to Game Manager
    public GameManager gameManager;

    // Input action used to trigger the button press
    public InputActionReference toggleReference = null;

    // Flag to check if player is within the button's trigger area
    bool isOnPerimeter = false;

    // Called in the beginning
    void Awake()
    {
        // Subscribe to input action when the script is initialized
        toggleReference.action.started += Toggle;
    }

    private void OnDestroy()
    {
        // Unsubscribe to avoid memory leaks or input issues
        toggleReference.action.started -= Toggle;
    }


    // If the GameController enters the trigger area, change material and allow interaction
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "GameController")
        {
            gameObject.GetComponent<Renderer>().material = M_Int_Hover;
            isOnPerimeter = true;
        }
    }

    // If the GameController leaves the trigger area, reset material and disable interaction
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "GameController")
        {
            gameObject.GetComponent<Renderer>().material = M_Base;
            isOnPerimeter = false;
        }
    }

    // If player is within the button area and activates input, trigger the button function
    private void Toggle(InputAction.CallbackContext context)
    {
        if (isOnPerimeter == true)
        {
            bool isActive = !gameObject.activeSelf;
            RestartButtonPush();
        }
    }

    // Play confirmation sound and reload the main game scene
    public void RestartButtonPush()
    {
        gameManager.PlayGranted();
        SceneManager.LoadScene("GameScene");
    }
}
