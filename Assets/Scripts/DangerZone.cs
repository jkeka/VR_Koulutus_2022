using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
            //gameObject.GetComponent<Renderer>().material = M_Int_Hover;

            GameManager.wasCautious = false;
            Debug.Log("DANGER");
        }

    }
    private void OnTriggerExit(Collider other)
    {

        if (other.tag == "Player")
        {

            //gameObject.GetComponent<Renderer>().material = M_Blue;
        }


    }
}
