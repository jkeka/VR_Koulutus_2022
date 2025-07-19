using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// Tutorial controller that manages the flow, state, and references for the gameplay
// Acts as central controller of the tutorial scene
public class TutorialManager : MonoBehaviour
{
    // Audio clips for feedback
    public AudioClip granted;
    public AudioClip accomplished;

    // AudioSource used to play sound effects
    AudioSource audioSource;

    // List of bolts used in the tutorial for tracking
    public List<GameObject> boltsList;

    // GameObjects involved in the motor and tool setup
    public GameObject bolts;
    public GameObject tutMotorDummy;
    public GameObject tutMotorStartDummy;
    public GameObject tutMotorSocket;
    public GameObject tutMotor;
    public GameObject impactWrench;
    public GameObject impactWrenchDummy;
    public GameObject screen;
    public GameObject arrow;

    // VR controller GameObjects
    public GameObject rightController;
    public GameObject teleController;

    // Turn provider (VR snap/continuous turning system)
    public GameObject turnProvider;

    // Virtual representations of thumbsticks
    public GameObject leftStick;
    public GameObject rightStick;

    // Transforms for setting/resetting positions
    public Transform startingPosition;        // Where the player is moved to
    public Transform playerStartingPosition;  // Predefined spawn position for the player
    public Transform tutMotorStartPosition;   // Starting location of the tutorial motor

    // Flags for tutorial progress
    public bool isMotorLifted;    // Whether the motor has been lifted
    public bool accomplishedPlayed;    // Whether the accomplishment sound has already been played

    // Current tutorial stage index
    public int tutStageInt;

    // Number of bolts reattached
    public int attachedBolts;

    // Reference to To-Do panel script for checklist UI
    public ToDoPanel toDoPanelScript;

    // UI Panels
    public GameObject toDoPanel;
    public GameObject infoPanel_Panel;

    // On-screen text fields for tasks and information
    public TMPro.TMP_Text taskText;
    public TMPro.TMP_Text infoText;

    // Called when the script instance is loaded
    void Awake()
    {
        // Move player to starting position
        startingPosition.position = playerStartingPosition.position;

        // Reset tutorial stage
        tutStageInt = 0;

        // Deactivate motor and bolt-related objects until needed        
        bolts.SetActive(false);
        tutMotorDummy.SetActive(false);
        tutMotorSocket.SetActive(false);
        
        // Show the task checklist panel
        toDoPanel.SetActive(true);

        // Get reference to this object's AudioSource for sound playback
        audioSource = GetComponent<AudioSource>();

        // Hide the directional arrow at start
        arrow.SetActive(false);

        // Disable controller models until tutorial requires them
        rightController.SetActive(false);
        teleController.SetActive(false);

        // Disable player turning system initially
        turnProvider.SetActive(false);

        // Reset tutorial condition flags
        isMotorLifted = false;
        accomplishedPlayed = false;
    }

    void Update()
    {

        // Ensure the info panel is always visible during the tutorial
        infoPanel_Panel.SetActive(true);

        // Stage 0: Initial setup check - waits for player to be moved to starting position
        if (tutStageInt == 0)
        {
            // Check if startingPosition has changed to match playerStartingPosition on X or Z axis
            if (startingPosition.position.x != playerStartingPosition.position.x || startingPosition.position.z != playerStartingPosition.position.z)
            {
                // Move to tutorial stage 1 when player is correctly positioned
                tutStageInt = 1;

                // Play a sound indicating progression (granted)
                PlayGranted();
            }
        }

        // Stage 2 to 3 transition: Detect if the motor is lifted (Y-position higher than start)
        if (tutMotor.transform.position.y > tutMotorStartPosition.position.y && tutStageInt == 2)
        {
            TutStageIntTo3();    // Move tutorial to stage 3
        }

        // Stage 0: Show left thumbstick UI, hide right controller and teleport controller
        if (tutStageInt == 0)
        {
            leftStick.SetActive(true);
            rightController.SetActive(false);
            teleController.SetActive(false);
        }

        // Stage 1: Enable player turning, show right controller and right stick UI, 
        // and teleport controller
        if (tutStageInt == 1)
        {
            turnProvider.SetActive(true);

            leftStick.SetActive(false);
            rightStick.SetActive(true);
            rightController.SetActive(true);
            teleController.SetActive(true);
            
            // Update instructional text for teleporting using the right thumbstick and grip
            infoText.text = "Press the right thumbstick forward, aim floor and press the grip to teleport";
            
            // Mark this stage's task as done in the ToDo panel UI
            toDoPanelScript.doneImagesList[tutStageInt].SetActive(true);
        }

        // Stage 2: Motor pickup stage
        if (tutStageInt == 2)
        {
            rightStick.SetActive(false);
            tutMotorStartDummy.SetActive(false);

            // Show the actual motor object to be picked up
            tutMotor.SetActive(true);

            // Highlight motor with outline to indicate interactivity
            tutMotor.GetComponent<Outline>().enabled = true;

            // Show directional arrow pointing to motor
            arrow.SetActive(true);

            // Update instructions for picking up the motor
            infoText.text = "Pick up the motor by taking the controller near and pressing the grip";
            
            // Mark this stage's task as done in the ToDo panel UI
            toDoPanelScript.doneImagesList[tutStageInt].SetActive(true);
        }

        // Stage 3: Position the motor on its socket/base
        if (tutStageInt == 3)
        {
            // Highlight motor socket/base for placing motor
            tutMotorSocket.GetComponent<Outline>().enabled = true;
            
            // Instruction to set the motor to its base
            infoText.text = "Set the motor to it's base";

            // Mark this stage's task as done in the ToDo panel UI
            toDoPanelScript.doneImagesList[tutStageInt].SetActive(true);
        }

        // Stage 4: Switch focus from motor to impact wrench tool
        if (tutStageInt == 4)
        {
            // Hide the motor socket highlight and show dummy motor (non-interactive)
            tutMotorSocket.SetActive(false);
            tutMotorDummy.SetActive(true);

            // Disable motor outline highlight
            tutMotor.GetComponent<Outline>().enabled = false;

            // Also disable motor socket outline
            tutMotorSocket.GetComponent<Outline>().enabled = false;

            // Swap impact wrench dummy with actual impact wrench and enable its outline
            impactWrenchDummy.SetActive(false);
            impactWrench.SetActive(true);
            impactWrench.GetComponent<Outline>().enabled = true;

            // Instruction to pick up the impact wrench
            infoText.text = "Pick up the impact wrench";

            // Mark this stage's task as done in the ToDo panel UI
            toDoPanelScript.doneImagesList[tutStageInt].SetActive(true);
        }

        // Stage 5: Bolt tightening stage
        if (tutStageInt == 5)
        {
            // Show bolts to be tightened
            bolts.SetActive(true);

            // Instruction to tighten bolts using impact wrench trigger
            infoText.text = "Tighten the bolts with impact wrench pressing the trigger";

            // Mark this stage's task as done in the ToDo panel UI
            toDoPanelScript.doneImagesList[tutStageInt].SetActive(true);
        }

        // Stage 6: Tutorial completion stage
        if (tutStageInt == 6)
        {
            // Disable impact wrench highlight
            impactWrench.GetComponent<Outline>().enabled = false;

            // Display completion message
            infoText.text = "Tutorial complete! Restart or exit by using the screen";

            // Highlight the screen UI for next action
            screen.GetComponent<Outline>().enabled = true;
            
            // Mark this stage's task as done in the ToDo panel UI
            toDoPanelScript.doneImagesList[tutStageInt].SetActive(true);

            // Play accomplished sound once when tutorial finishes
            PlayAccomplished();
        }

        // Check if all bolts are attached to move tutorial to completion (stage 6)
        if (attachedBolts >= boltsList.Count)
        {
            tutStageInt = 6;
        }
    }

    // Advances tutorial stage from 1 to 2, only if currently at stage 1
    public void TutStageIntTo2()
    {
        if (tutStageInt == 1)
        {
            tutStageInt = 2;
        }
    }

    // Advances tutorial stage from 2 to 3, only if currently at stage 2
    public void TutStageIntTo3()
    {
        if (tutStageInt == 2)
        {
            tutStageInt = 3;
        }
    }

    // Advances tutorial stage from 3 to 4, only if currently at stage 3
    public void TutStageIntTo4()
    {
        if (tutStageInt == 3)
        {
            tutStageInt = 4;
        }
    }

    // Advances tutorial stage from 4 to 5, only if currently at stage 4
    public void TutStageIntTo5()
    {
        if (tutStageInt == 4)
        {
            tutStageInt = 5;
        }
    }

    // Plays the 'granted' audio clip once at full volume
    public void PlayGranted()
    {
        audioSource.PlayOneShot(granted, 1f);
    }

    // Plays the 'granted' audio clip only if the tutorial stage is 1
    public void PlayGrantedTele()
    {
        if (tutStageInt == 1)
        {
            audioSource.PlayOneShot(granted, 1f);

        }
    }

    // Plays the 'accomplished' audio clip once, only if it hasn't been played before
    public void PlayAccomplished()
    {
        if (accomplishedPlayed == false)
        {
            audioSource.PlayOneShot(accomplished, 1f);
            accomplishedPlayed = true;
        }
    }
}
