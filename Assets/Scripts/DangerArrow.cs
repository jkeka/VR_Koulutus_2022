using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerArrow : MonoBehaviour
{

    public Transform target;

    Renderer m_RendererTarget;

    public float speed = 5.0f;

    void Update()
    {

        //Rotation
        Vector3 targetDirection = target.position - transform.position;
        float singleStep = speed * Time.deltaTime;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);

    }
}
