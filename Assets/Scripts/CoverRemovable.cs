using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Handles the behavior of a removable cover in the game
// Includes visual feedback, interaction triggers, and stage transitions

public class CoverRemovable : MonoBehaviour
{
    // Material for when the cover is highlighted
    public Material M_Int_Hover;

    // Default metal material
    public Material M_GreyMetal;

    // Reference to the GameManager controlling global state
    public GameManager gameManager;

    // Rigidbody for enabling/disabling gravity
    public Rigidbody rb;

    // Called on Awake. Sets up layer collision rules
    private void Awake()
    {
        // Initially ignore collisions between layers 7 and 16
        Physics.IgnoreLayerCollision(7, 16, true);
    }

    // Updates visual feedback and physics rules based on game state
    private void Update()
    {
        if (gameManager.stageInt == 4)
        {
            // Enable outline and allow collisions for pickup
            gameObject.GetComponent<Outline>().enabled = true;
            Physics.IgnoreLayerCollision(7, 16, false);

        }
        if (gameManager.stageInt == 7)
        {
            // Re-enable outline for placing the cover back
            gameObject.GetComponent<Outline>().enabled = true;
        }
    }

    // Triggered when another collider enters the cover’s trigger zone
    // Used for detecting when the cover is placed near the correct socket
    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "CoverRemovableSocket")
        {
            gameManager.coverOnSocket = true;
        }
    }

    // Triggered when another collider exits the cover’s trigger zone
    // Resets placement state when leaving the socket area
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "CoverRemovableSocket")
        {
            gameManager.coverOnSocket = false;
        }
    }

    // Called when the player grabs the cover
    // Disables visual outline
    public void CoverGrabbed()
    {
        gameObject.GetComponent<Outline>().enabled = false;
        Debug.Log("Cover grapped");
    }
    
    // Called when the player releases the cover
    // Advances game stage based on current state and socket placement
    public void CoverAbandoned()
    {
        if (gameManager.stageInt == 4)
        {
            gameManager.stageInt = 5;
            gameManager.PlayGranted();
            Debug.Log("Stage int to 5, cover dropped");
            rb.useGravity = true;
            gameObject.GetComponent<Outline>().enabled = false;
        }

        if (gameManager.stageInt == 7 && gameManager.coverOnSocket == true)
        {
            Debug.Log("Stage int to 8, cover set back");
            gameManager.stageInt = 8;
            gameManager.PlayGranted();
        }
    }
}
