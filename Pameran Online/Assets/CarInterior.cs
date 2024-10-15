using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem.XR;

public class CarInterior : MonoBehaviour
{
    public InputActionProperty[] exitButtons;
    public Transform cameraPosition;
    private bool hasEntered = false;
    private bool exitPressed = false;
    private bool canExit = false;
    private GameObject player;
    private Vector3 lastPosition;
    private CapsuleCollider collider;
    private GameObject settings;
    private GameObject playerCamera;
    private TrackedPoseDriver trackedPoseDriver;

    private ActionBasedContinuousMoveProvider continuousMove;
    private TeleportationProvider teleportation;
    private KeyboardControls keyboard;
    private bool wasTeleportationEnabled;
    private bool wasKeyboardEnabled;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("XR Origin (XR Rig)");
        settings = GameObject.Find("Settings Menu");
        playerCamera = GameObject.Find("Camera Offset");
        collider = this.GetComponent<CapsuleCollider>();
        continuousMove = player.GetComponent<ActionBasedContinuousMoveProvider>();
        teleportation = player.GetComponent<TeleportationProvider>();
        keyboard = player.GetComponent<KeyboardControls>();
        trackedPoseDriver = playerCamera.transform.GetChild(0).GetComponent<TrackedPoseDriver>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach(InputActionProperty button in exitButtons){
            exitPressed = exitPressed || button.action.WasPressedThisFrame();
        }
        if(hasEntered){
            settings.SetActive(false);

            if(exitPressed && canExit){
                collider.enabled = true;
                player.transform.position = lastPosition;
                playerCamera.transform.localPosition = new Vector3(0f, 1.5f, 0f);
                hasEntered = false;
                canExit = false;

                if(wasTeleportationEnabled){
                    teleportation.enabled = true;
                }
                if(wasKeyboardEnabled){
                    keyboard.enabled = true;
                }
                continuousMove.enabled = true;
                trackedPoseDriver.trackingType = TrackedPoseDriver.TrackingType.RotationAndPosition;
            }
        }
        exitPressed = false;
    }

    public void EnterInterior(){
        collider.enabled = false;
        lastPosition = player.transform.position;
        player.transform.position = transform.parent.position;
        hasEntered = true;

        if(teleportation.enabled){
            wasTeleportationEnabled = true;
            teleportation.enabled = false;
        }
        else{
            wasTeleportationEnabled = false;
        }

        if(keyboard.enabled){
            wasKeyboardEnabled = true;
            //keyboard.enabled = false;
        }
        else{
            wasKeyboardEnabled = false;
        }

        continuousMove.enabled = false;

        playerCamera.transform.position = cameraPosition.position;
        playerCamera.transform.localRotation = Quaternion.Euler(cameraPosition.forward);
        trackedPoseDriver.trackingType = TrackedPoseDriver.TrackingType.RotationOnly;

        Invoke("ExitCooldown", 1f);
    }

    void ExitCooldown(){
        canExit = true;
    }
}
