using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlinePulse : MonoBehaviour
{

    public float outlineWidth;


    private void Start()
    {
        
    }

    private void Update()
    {
        outlineWidth = Mathf.Lerp(3f, 7f, 1 * Time.deltaTime);
        Debug.Log(outlineWidth);
    }



}
