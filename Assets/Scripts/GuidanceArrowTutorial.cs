using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidanceArrowTutorial : MonoBehaviour
{

    public TutorialManager tutorialManager;

    public Transform target;

    Renderer m_RendererTarget;

    public GameObject arrow;

    public List<GameObject> targetsList;

    private int targetNumber;

    public float speed = 5.0f;


    void Update()
    {

        //Target
        targetNumber = tutorialManager.tutStageInt - 1;
        target = targetsList[targetNumber].transform;
        m_RendererTarget = targetsList[targetNumber].GetComponent<Renderer>();

        if (m_RendererTarget.isVisible == true)
        {
            arrow.GetComponent<MeshRenderer>().enabled = false;
        } else
            arrow.GetComponent<MeshRenderer>().enabled = true;

        //Rotation
        Vector3 targetDirection = target.position - transform.position;
        float singleStep = speed * Time.deltaTime;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);

    }
}
