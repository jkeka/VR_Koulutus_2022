using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCanvasMover : MonoBehaviour
{


    private void Update()
    {
        transform.position = new Vector3(transform.position.x, 3.36f, transform.position.z);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
    }


}
