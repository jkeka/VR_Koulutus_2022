using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Handles the movement and interaction logic of the safety door
public class SafetyDoor : MonoBehaviour
{
    // Reference to Game Manager
    public GameManager gameManager;

    // Reference to door stopping game object
    public GameObject stopper;

    // Door materials depending of states of door (hovered and default)
    public Material M_Int_Hover; 
    public Material M_RobotOrange;

    // Target rotation for when the door opens
    private Vector3 direction = new Vector3(90f, -90f, 90f);

    // Input action reference for detecting interaction (button press)
    public InputActionReference toggleReference = null;

    // Whether the player is in the interaction area
    bool isOnPerimeter = false;

    // Whether the door has already been opened
    bool doorOpened = false;

    // Subscribes to input action event when object awakens
    void Awake()
    {
        toggleReference.action.started += Toggle;
    }

    // Unsubscribes from input event when object is destroyed
    private void OnDestroy()
    {
        toggleReference.action.started -= Toggle;
    }

    // Called every frame
    private void Update()
    {
        // Enables the outline effect when the game reaches stage 2
        if (gameManager.stageInt == 2)
        {
            gameObject.GetComponent<Outline>().enabled = true;
        }

        // If the door should be opening, smoothly rotate it to the target orientation
        if (doorOpened)
        {
            Quaternion targetRotation = Quaternion.Euler(direction);
            gameObject.transform.rotation = Quaternion.Lerp(this.transform.rotation, targetRotation, Time.deltaTime * 1.5f);
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    // Called when another collider enters this door's trigger collider
    private void OnTriggerEnter(Collider other)
    {
        // If the player enters the interaction area during stage 2, 
        // enable hover material and set interaction flag
        if (other.tag == "GameController" && gameManager.stageInt == 2)
        {
            gameObject.GetComponent<Renderer>().material = M_Int_Hover;
            isOnPerimeter = true;
        }
        
        // If the door touches the stopper object, disable its colliders
        if (other.tag == "DoorStopper")
        {
            foreach (Collider c in GetComponents<Collider>())
            {
                c.enabled = false;
            }
        }
    }
    
    // Called when another collider exits this door's trigger collider
    private void OnTriggerExit(Collider other)
    {
        // If the player leaves the interaction area, revert to default material and reset flag
        if (other.tag == "GameController")
        {
            gameObject.GetComponent<Renderer>().material = M_RobotOrange;
            isOnPerimeter = false;
        }   
    }

    // Triggered when the input action occurs
    private void Toggle(InputAction.CallbackContext context)
    {
        // Only allow interaction if the player is near the door
        if (isOnPerimeter == true)
        {
            bool isActive = !gameObject.activeSelf;
            OpenDoor();
        }

    }
    
        // Opens the door by setting state flags and triggering game progression
        public void OpenDoor()
    {
        doorOpened = true;
        
        // Disable outline and set default material
        gameObject.GetComponent<Outline>().enabled = false;
        gameObject.GetComponent<Renderer>().material = M_RobotOrange;

        // Inform GameManager and progress to next stage
        gameManager.PlayGranted();
        gameManager.stageInt = 3;
    }
}
