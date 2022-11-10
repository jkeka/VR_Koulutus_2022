using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerZone : MonoBehaviour
{

    public GameManager gameManager;

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
            //gameObject.GetComponent<Renderer>().material = M_Int_Hover;

            GameManager.wasCautious = false;
            gameManager.PlayWarning();
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
