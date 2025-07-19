using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Controls an on-screen arrow that guides the player toward a tutorial objective
// The arrow becomes visible only when the current target is off-screen
public class GuidanceArrowTutorial : MonoBehaviour
{
    // Reference to the TutorialManager to track the current tutorial stage    
    public TutorialManager tutorialManager;

    // Current target Transform the arrow should point at
    public Transform target;

    // Renderer of the current target, used to check screen visibility
    Renderer m_RendererTarget;

    // The arrow GameObject that visually points to the target
    public GameObject arrow;

    // List of all potential targets in the tutorial
    public List<GameObject> targetsList;

    // Index of the current target in the targetsList
    private int targetNumber;

    // Speed at which the arrow rotates to face the target
    public float speed = 5.0f;

    // Uodate is called every frame
    void Update()
    {
        // Determine the current target index based on tutorial progress (starts at 1)        
        targetNumber = tutorialManager.tutStageInt - 1;

        // Update the target transform and get its renderer
        target = targetsList[targetNumber].transform;
        m_RendererTarget = targetsList[targetNumber].GetComponent<Renderer>();

        // Show the arrow only if the target is not currently visible on screen
        if (m_RendererTarget.isVisible == true)
        {
            arrow.GetComponent<MeshRenderer>().enabled = false;
        } else
            arrow.GetComponent<MeshRenderer>().enabled = true;

        // Smoothly rotate the arrow to point toward the target
        Vector3 targetDirection = target.position - transform.position;
        float singleStep = speed * Time.deltaTime;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);
    }
}
