using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class ActivateTeleportationRay : MonoBehaviour
{
    public GameObject leftTeleportation;
    public GameObject rightTeleportation;

    public InputActionProperty leftAction;
    public InputActionProperty rightAction;

    // Update is called once per frame
    void Update()
    {
        leftTeleportation.SetActive(leftAction.action.ReadValue<float>() > 0.1f);
        rightTeleportation.SetActive(rightAction.action.ReadValue<float>() > 0.1f);
    }
}
