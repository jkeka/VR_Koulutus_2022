using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script controls an arrow that guides the player by pointing towards the current, 
// target object.
// The arrow is only visible when the target is outside the camera's view.
public class GuidanceArrow : MonoBehaviour
{
    // Reference to the GameManager to get the current game stage
    public GameManager gameManager;
    
    // The current target that the arrow should point at
    public Transform target;

    // Renderer component of the target, used to check if it is visible on screen
    Renderer m_RendererTarget;

    // The arrow GameObject that visually points to the target
    public GameObject arrow;

    // List of all target objects the arrow can point to
    public List<GameObject> targetsList;

    // Index of the current target in the targets list
    private int targetNumber;

    // Speed at which the arrow rotates to face the target
    public float speed = 5.0f;

    // Update called in every frame
    void Update()
    {
        // Update target index based on the current stage (assuming stageInt starts at 1)
        targetNumber = gameManager.stageInt - 1;

        // Get the current target transform from the list
        target = targetsList[targetNumber].transform;

        // Get the Renderer component of the target to check visibility
        m_RendererTarget = targetsList[targetNumber].GetComponent<Renderer>();

        // Show arrow only if the target is not visible on screen
        if (m_RendererTarget.isVisible == true)
        {
            arrow.GetComponent<MeshRenderer>().enabled = false;
        } else
            arrow.GetComponent<MeshRenderer>().enabled = true;

        // Smoothly rotate the arrow towards the target
        Vector3 targetDirection = target.position - transform.position;
        float singleStep = speed * Time.deltaTime;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);
    }
}
