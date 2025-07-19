using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

// Manages the information panel UI, displaying current task and info based on game stage
// Also handles toggling the panel visibility with input actions
public class InfoPanel : MonoBehaviour
{
    // Reference to the central GameManager for game state info
    public GameManager gameManager;

    // UI Text showing the current stage integer
    public TMPro.TMP_Text stageIntText;

    // Panel displaying task instructions
    public GameObject toDoPanel;

    // UI Text fields for task description and additional info
    public TMPro.TMP_Text taskText;    
    public TMPro.TMP_Text infoText;

    // Input action reference for toggling this panel's visibility
    public InputActionReference toggleReference = null;

    // Called once when the script instance is loaded
    // Sets panel active and subscribes to toggle input event
    private void Awake()
    {
        gameObject.SetActive(true);
        toggleReference.action.started += Toggle;
    }

    // Called when the script instance is destroyed
    // Unsubscribes from toggle input event to prevent memory leaks
    private void OnDestroy()
    {
        toggleReference.action.started -= Toggle;
    }

    // Called every frame
    // Updates the stage integer display to match the current game state
    private void Update()
    {
        stageIntText.text = gameManager.stageInt.ToString();
    }

    // Toggles the visibility of the info panel
    private void Toggle(InputAction.CallbackContext context)
    {
        bool isActive = !gameObject.activeSelf;
        gameObject.SetActive(isActive);
    }

    // Next methods to set task and info text based on current level/stage
    public void LevelOne()
    {
        taskText.text = "Push down the emergency switch of the robot";
        infoText.text = "Power must be shut down before entering the robot cell";
    }

    public void LevelTwo()
    {
        taskText.text = "Open the robot cell door";
        infoText.text = "";
    }

    public void LevelThree()
    {
        taskText.text = "Remove the bolts of bearing cover plate with the impact wrench";
        infoText.text = "The impact wrench can be found from the table next to the robot";
    }

    public void LevelFour()
    {
        taskText.text = "Remove the covering plate";
        infoText.text = "";
    }

    public void LevelFive()
    {
        taskText.text = "Remove the old bearing with the bearing extractor";
        infoText.text = "Extractor can be found from the table next to robot";
    }

    public void LevelSix()
    {
        taskText.text = "Insert the new bearing on to the axle";
        infoText.text = "New bearing can be found from the table next to robot";
    }

    public void LevelSeven()
    {
        taskText.text = "Insert the covering plate back to it's position";
        infoText.text = "";
    }

    public void LevelEight()
    {
        taskText.text = "Tighten the bolts with the impact wrench";
        infoText.text = "";
    }

    public void LevelNine()
    {
        taskText.text = "Task accomplished, well done!";
        infoText.text = "Select restart or feedback to proceed";
    }
}
