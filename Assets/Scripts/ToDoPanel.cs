using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// This script manages the To-Do Panel UI in the game.
public class ToDoPanel : MonoBehaviour
{
    // Reference to the info panel UI element
    public GameObject infoPanel;

    // Reference to the GameManager script
    public GameManager gameManager;

    // List of GameObjects representing task completion indicators
    public List<GameObject> doneImagesList;

    // Boolean to track whether the to-do menu is active
    public bool isMenuActive;

    // Input action reference to toggle this panel (e.g., controller button)
    public InputActionReference toggleReference = null;

    // Called in beginning
    private void Awake()
    {
        gameObject.SetActive(false);    // Hide the to-do panel when the scene starts

        isMenuActive = false;    // Initialize the menu active status

        // Subscribe the Toggle function to the input event
        toggleReference.action.started += Toggle;

        // Disable all done image indicators at the start
        foreach (GameObject images in doneImagesList)
        {
            images.SetActive(false);
        }
    }

    // Unsubscribe the input event when the object is destroyed to prevent memory leaks
    private void OnDestroy()
    {
        toggleReference.action.started -= Toggle;
    }

    // Called when the toggle input is activated
    private void Toggle(InputAction.CallbackContext context)
    {
        // Toggle the panel's active state
        bool isActive = !gameObject.activeSelf;    
        gameObject.SetActive(isActive);
    }
}
