using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem.XR;

public class CarTeleportationAnchor : MonoBehaviour
{
    private GameObject player;
    private GameObject camera;
    private CharacterController characterController;
    private CharacterControllerDriver characterControllerDriver;
    private TrackedPoseDriver trackedPoseDriver;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("XR Origin (XR Rig)");
        camera = GameObject.Find("Main Camera");
        characterController = player.GetComponent<CharacterController>();
        characterControllerDriver = player.GetComponent<CharacterControllerDriver>();
        trackedPoseDriver = camera.GetComponent<TrackedPoseDriver>();
    }

    public void OnTriggerStay(Collider col){
        //characterController.height = 0.5f;
        characterController.enabled = false;
        //characterControllerDriver.maxHeight = 0.5f;
        camera.transform.position = new Vector3(camera.transform.position.x, 0.4f, camera.transform.position.z);
        trackedPoseDriver.trackingType = TrackedPoseDriver.TrackingType.RotationOnly;
    }

    public void OnTriggerExit(Collider col){
        characterController.enabled = true;
        //characterControllerDriver.maxHeight = 100f;
        trackedPoseDriver.trackingType = TrackedPoseDriver.TrackingType.RotationAndPosition;
    }
}
