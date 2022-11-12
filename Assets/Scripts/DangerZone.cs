using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerZone : MonoBehaviour
{

    public GameManager gameManager;

    public GameObject warningPanel;
    public GameObject warningArrow;

    private void Awake()
    {
        warningArrow.SetActive(false);
        warningPanel.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {

            warningArrow.SetActive(true);
            warningPanel.SetActive(true);

            GameManager.wasCautious = false;
            gameManager.PlayWarning();
        }

    }
    private void OnTriggerExit(Collider other)
    {

        if (other.tag == "Player")
        {
            warningArrow.SetActive(false);
            warningPanel.SetActive(false);
        }


    }
}
