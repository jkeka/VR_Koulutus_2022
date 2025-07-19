using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Main game controller that manages the flow, state, and references for the gameplay
// Acts as central controller of the game
public class GameManager : MonoBehaviour
{
    // Tracks elapsed gameplay time
    private float timer;

    // Static reference to total end time
    public static float endTime;

    // Thickness of outlines used for object highlighting
    public float outlineWidth;

    // Audio clips used for various events
    public AudioClip granted;
    public AudioClip accomplished;
    public AudioClip warning;

    // AudioSource used to play feedback sounds
    AudioSource audioSource;

    // Current gameplay stage (used to drive progression logic)
    public int stageInt;

    // Number of bolts opened and attached (used to track task progress)
    public int openedBolts;
    public int attachedBolts;

    // Flags to track different game conditions
    public bool allBoltsLoose;
    public bool allBoltsFloorColor;
    public bool timerOn;
    public bool coverOnSocket;

    // Tracks whether the player followed safety/caution protocol
    public static bool wasCautious;

    // Reference to the in-world informational display panel
    public InfoPanel infoPanel;

    // Reference to the task checklist panel script
    public ToDoPanel toDoPanelScript;

    // UI elements for different panels and menus
    public GameObject toDoPanel;
    public GameObject winPanel;
    public GameObject infoPanel_Panel;

    // Controllers for menus and direct input (likely VR controller representations)
    public GameObject menuControllerLeft;
    public GameObject menuControllerRight;
    public GameObject directControllerLeft;
    public GameObject directControllerRight;
    public GameObject teleController;

    // List of all bolt GameObjects (used for checking completion status)
    public List<GameObject> boltsList;

    // Tools and objects used in the simulation/task
    public GameObject wrench;
    public GameObject coverRemovable;
    public GameObject coverRemovableDummy;
    public GameObject bearingExtractor;
    public GameObject bearingNew;
    public GameObject bearingNewSocket;
    public GameObject bearingBroken;
    public GameObject coverRemovableSocket;

    // Screens shown in different menu states
    public GameObject menuScreen;
    public GameObject menuScreenBasics;
    public GameObject menuScreenFeedback;

    // Reference positions used for snapping or validation
    public Transform bearingSocketTransform;
    public Transform coverRemovableTransform;

    // Rigidbody references for physical interactions or constraints
    public Rigidbody bearingNewRB;
    public Rigidbody coverRemovableRB;

    // Materials used for socket and floor visuals
    public Material M_Socket;
    public Material M_Floor;

    // Awake to initialize variables or states before the application starts.
    void Awake()
    {
        // Initialize timers and game stage
        timer = 0;
        endTime = 0;
        stageInt = 1;

        // Reset bolt counters
        openedBolts = 0;
        attachedBolts = 0;

        // Disable both menu controllers at start
        menuControllerLeft.SetActive(false);
        menuControllerRight.SetActive(false);

        // Hide certain objects and placeholders at the beginning of the game
        bearingNewSocket.SetActive(false);
        coverRemovableSocket.SetActive(false);
        coverRemovableDummy.SetActive(false);

        // Hide win panel and feedback screen initially
        winPanel.SetActive(false);
        menuScreenFeedback.SetActive(false);
        
        // Show the task/to-do panel at start
        toDoPanel.SetActive(true);

        // Get reference to the attached AudioSource component for playing sound effects
        audioSource = GetComponent<AudioSource>();

        // Set default gameplay flags
        allBoltsLoose = true;    // Assume all bolts are loosened (can be used for validation)
        allBoltsFloorColor = true;    // Used for floor material state validation
        wasCautious = true;    // Assume safety rules were followed (updated later based on behavior)

        timerOn = true;    // Start the gameplay timer

        coverOnSocket = false;    // The removable cover is not yet placed on the socket
    }

    void Update()
    {
        // Always keep the info panel visible during gameplay
        infoPanel_Panel.SetActive(true);

        // Track gameplay time if the timer is active
        if (timerOn == true)
        {
            timer = timer + Time.deltaTime;
        }

        // Starting stage 1
        if (stageInt == 1)
        {
            infoPanel.LevelOne();    // Show instructions for stage 1
        }
        
        // Stage 2: player reaches a key area (proximity or interaction)
        if (stageInt == 2)
        {
            infoPanel.LevelTwo();    // Show stage 2 info
            toDoPanelScript.doneImagesList[stageInt].SetActive(true);    // Mark checklist as completed
        }

        // Stage 3: Loosen all bolts
        if (stageInt == 3)
        {
            infoPanel.LevelThree();
            toDoPanelScript.doneImagesList[stageInt].SetActive(true);

            // Enable outlines on all bolts to indicate interactivity
            foreach (GameObject bolt in boltsList)
            {
                bolt.GetComponent<Outline>().enabled = true;
            }

            // Highlight the wrench
            wrench.GetComponent<Outline>().enabled = true;

            // Check if all bolts have been opened
            if (openedBolts >= boltsList.Count)
            {
                stageInt = 4;    // Advance to next stage
            }
        }

        // Stage 4: All bolts removed
        if (stageInt == 4)
        {
            toDoPanelScript.doneImagesList[stageInt].SetActive(true);

            // Hide wrench outline (task complete)
            wrench.GetComponent<Outline>().enabled = false;
            infoPanel.LevelFour();
        }

        // Stage 5: Extract broken bearing
        if (stageInt == 5)
        {
            toDoPanelScript.doneImagesList[stageInt].SetActive(true);
            
            // Highlight the extractor tool and broken bearing
            bearingExtractor.GetComponent<Outline>().enabled = true;
            bearingBroken.GetComponent<Outline>().enabled = true;
            
            infoPanel.LevelFive();
        }
        
        // Stage 6: Insert new bearing
        if (stageInt == 6)
        {
            toDoPanelScript.doneImagesList[stageInt].SetActive(true);
            
            // Hide old tool outline and show new bearing + socket
            bearingExtractor.GetComponent<Outline>().enabled = false;
            bearingNew.GetComponent<Outline>().enabled = true;
            bearingNewSocket.SetActive(true);
            
            infoPanel.LevelSix();
        }
        
        // Stage 7: Bearing placed correctly
        if (stageInt == 7)
        {
            toDoPanelScript.doneImagesList[stageInt].SetActive(true);

            // Snap bearing to correct position and disable interactions
            bearingNew.GetComponent<Outline>().enabled = false;
            bearingNew.transform.position = bearingSocketTransform.position;
            bearingNew.transform.rotation = bearingSocketTransform.rotation;

            bearingNewRB.useGravity = false;
            bearingNewRB.isKinematic = true;
            bearingNew.GetComponent<Collider>().enabled = false;

            // Swap bearing socket to cover socket
            bearingNewSocket.SetActive(false);
            coverRemovableSocket.SetActive(true);

            infoPanel.LevelSeven();
        }

        // Stage 8: Place the cover and reattach bolts
        if (stageInt == 8)
        {
            toDoPanelScript.doneImagesList[stageInt].SetActive(true);

            // Hide cover outline and show wrench again
            coverRemovable.GetComponent<Outline>().enabled = false;
            wrench.GetComponent<Outline>().enabled = true;

            // Snap cover into correct position and replace with dummy
            coverRemovable.transform.position = coverRemovableTransform.position;
            coverRemovable.transform.rotation = coverRemovableTransform.rotation;
            coverRemovable.SetActive(false);
            coverRemovableDummy.SetActive(true);

            // Cleanup physical constraints and socket
            coverRemovableSocket.SetActive(false);
            coverRemovable.GetComponent<Collider>().enabled = false;

            // Reactivate all bolts for reattaching
            foreach (GameObject bolt in boltsList)
            {
                bolt.SetActive(true);
            }
            
            // If all bolts are marked as floor-colored, update their material
            if (allBoltsFloorColor == true)
            {
                foreach (GameObject bolt in boltsList)
                {
                    bolt.GetComponent<Renderer>().material = M_Floor;
                }
            }

            // If bolts are loose, show outlines to indicate they can be reattached
            if (allBoltsLoose == true)
            {
                foreach (GameObject bolt in boltsList)
                {
                    bolt.GetComponent<Outline>().enabled = true;
                }
            }

            // Once all bolts are reattached, complete the task
            if (attachedBolts >= boltsList.Count)
            {
                stageInt = 9;
                PlayAccomplished();    // Play feedback sound

                // Ensure final UI panels are shown
                infoPanel_Panel.SetActive(true);
                toDoPanel.SetActive(true);
            }
            infoPanel.LevelEight();
        }
        
        // Stage 9: Task complete – show menu and end
        if (stageInt == 9)
        {
            toDoPanelScript.doneImagesList[stageInt].SetActive(true);
            
            // Hide tool outlines
            wrench.GetComponent<Outline>().enabled = false;
            infoPanel.LevelNine();

            // Highlight menu screen and display feedback
            menuScreen.GetComponent<Outline>().enabled = true;
            menuScreenBasics.SetActive(false);
            menuScreenFeedback.SetActive(true);

            // Stop the timer and record final time
            timerOn = false;
            endTime = timer;
        }
    }

    // Sets the current game stage to 7
    public void StageInt7()
    {
        stageInt = 7;
    }

    // Sets the current game stage to 8
    public void StageInt8()
    {
        stageInt = 8;
    }

    // Plays the "granted" sound effect (for successful input or permission)
    public void PlayGranted()
    {
        audioSource.PlayOneShot(granted, 1f);
    }
    
    // Plays the "accomplished" sound effect (for completing a task)
    public void PlayAccomplished()
    {
        audioSource.PlayOneShot(accomplished, 1f);
    }
    
    // Plays the "warning" sound effect (for incorrect action or caution required)
    public void PlayWarning()
    {
        audioSource.PlayOneShot(warning, 1f);
    }
}
