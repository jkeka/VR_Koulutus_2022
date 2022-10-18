using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Teleportation : MonoBehaviour
{

    public GameObject RightHandTele;
    public GameObject RightHandAction;


    public InputActionReference toggleTeleportReference = null;


    void Awake()
    {
        //RightHandTele.SetActive(false);
        //RightHandAction.SetActive(true);

    }


    void Update()
    {
        Vector2 value = toggleTeleportReference.action.ReadValue<Vector2>();
        print("arvo: " + value);
    }
}
