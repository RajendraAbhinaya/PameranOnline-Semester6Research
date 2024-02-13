using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class ActivateRays : MonoBehaviour
{
    public GameObject leftTeleportation;
    public GameObject rightTeleportation;

    public InputActionProperty leftTeleportationAction;
    public InputActionProperty rightTeleportationAction;

    public InputActionProperty leftTeleportationCancel;
    public InputActionProperty rightTeleportationCancel;

    public GameObject leftRay;
    public GameObject rightRay;

    public XRDirectInteractor leftDirectGrab;
    public XRDirectInteractor rightDirectGrab;

    private XRRayInteractor leftRayInteractor;
    private XRRayInteractor rightRayInteractor;

    void Start()
    {
        leftRayInteractor = leftRay.GetComponent<XRRayInteractor>();
        rightRayInteractor = rightRay.GetComponent<XRRayInteractor>();
    }

    // Update is called once per frame
    void Update()
    {
        //Teleportation Ray
        bool isLeftRayHovering = leftRayInteractor.TryGetHitInfo(out Vector3 leftPos, out Vector3 leftNormal, out int leftNumber, out bool leftValid);
        leftTeleportation.SetActive(!isLeftRayHovering && leftTeleportationCancel.action.ReadValue<float>() == 0 && leftTeleportationAction.action.ReadValue<float>() > 0.1f);

        bool isRightRayHovering = rightRayInteractor.TryGetHitInfo(out Vector3 rightPos, out Vector3 rightNormal, out int rightNumber, out bool rightValid);
        rightTeleportation.SetActive(!isRightRayHovering && rightTeleportationCancel.action.ReadValue<float>() == 0 && rightTeleportationAction.action.ReadValue<float>() > 0.1f);

        //Ray interactor
        leftRay.SetActive(leftDirectGrab.interactablesSelected.Count == 0 && leftTeleportation.activeSelf == false);
        rightRay.SetActive(rightDirectGrab.interactablesSelected.Count == 0 && rightTeleportation.activeSelf == false);
    }
}
