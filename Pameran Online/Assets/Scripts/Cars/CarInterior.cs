using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class CarInterior : MonoBehaviour
{
    public InputActionProperty[] exitButtons;
    public Transform cameraPosition;
    public GameObject[] interiorAnchors;

    private bool hasEntered = false;
    private bool exitPressed = false;
    private bool canExit = false;
    private GameObject player;
    private Vector3 lastPosition;
    private CapsuleCollider capsuleCollider;
    public MeshRenderer meshRenderer;
    private GameObject settings;
    private GameObject playerCamera;
    private GameObject cameraOffset;
    private GameObject leftHand;
    private GameObject rightHand;
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
        cameraOffset = GameObject.Find("Camera Offset");
        playerCamera = cameraOffset.transform.GetChild(0).gameObject;
        leftHand = cameraOffset.transform.GetChild(1).gameObject;
        rightHand = cameraOffset.transform.GetChild(2).gameObject;
        capsuleCollider = this.GetComponent<CapsuleCollider>();
        //meshRenderer = this.GetComponent<MeshRenderer>();
        continuousMove = player.GetComponent<ActionBasedContinuousMoveProvider>();
        teleportation = player.GetComponent<TeleportationProvider>();
        keyboard = player.GetComponent<KeyboardControls>();
        trackedPoseDriver = playerCamera.GetComponent<TrackedPoseDriver>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach(InputActionProperty button in exitButtons){
            exitPressed = exitPressed || button.action.WasPressedThisFrame();
        }
        if(hasEntered){
            meshRenderer.enabled = false;
            //settings.SetActive(false);

            if(exitPressed && canExit){
                capsuleCollider.enabled = true;
                player.transform.position = lastPosition;
                hasEntered = false;
                canExit = false;
                ToggleHands(true);
                Invoke("ActivateAnchors", 1f);

                if(cameraOffset.transform.localPosition.y < 0.1f){
                    trackedPoseDriver.trackingType = TrackedPoseDriver.TrackingType.RotationAndPosition;
                }
                else{
                    cameraOffset.transform.localPosition = new Vector3(0f, 1.5f, 0f);
                }

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
        capsuleCollider.enabled = false;
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

        Debug.Log(cameraOffset.transform.localPosition.y);
        if(cameraOffset.transform.localPosition.y < 0.1f){
            playerCamera.transform.position = cameraPosition.position;
            trackedPoseDriver.trackingType = TrackedPoseDriver.TrackingType.RotationOnly;
        }
        else{
            cameraOffset.transform.position = cameraPosition.position;
        }
        ToggleHands(false);
        DeactivateAnchors();
        Invoke("ExitCooldown", 1f);
    }

    void ExitCooldown(){
        canExit = true;
    }

    void ToggleHands(bool toggle){
        leftHand.SetActive(toggle);
        rightHand.SetActive(toggle);
    }

    void ActivateAnchors(){
        foreach(GameObject anchor in interiorAnchors){
            anchor.SetActive(true);
        }
    }

    void DeactivateAnchors(){
        foreach(GameObject anchor in interiorAnchors){
            anchor.SetActive(false);
        }
    }
}
