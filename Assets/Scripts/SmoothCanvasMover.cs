using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCanvasMover : MonoBehaviour
{
    public Transform target;
    private float smoothTime = 1.0f;
    Vector3 velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = target.TransformPoint(new Vector3(0, 5, -10));

        // Smoothly move the camera towards that target position
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

        transform.eulerAngles = target.eulerAngles;

    }
}
