using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PokeButton : MonoBehaviour
{
    public GameObject visualObject;
    public Material active;
    public Material inactive;
    public Transform visualTarget;
    public Vector3 localAxis;
    public float resetSpeed = 5f;
    public float followAngleThreshold = 45f;

    private bool isActive = false;
    private Vector3 offset;
    private Transform pokeAttachTransform;
    private XRBaseInteractable interactable;
    private bool isFollowing;
    private bool freeze = false;
    private Vector3 initialPosition;
    // Start is called before the first frame update
    void Start()
    {
        initialPosition = visualTarget.localPosition;
        interactable = GetComponent<XRBaseInteractable>();
        interactable.hoverEntered.AddListener(Follow);
        interactable.hoverExited.AddListener(Reset);
        interactable.selectEntered.AddListener(Freeze);
        ActivateButton(isActive);
    }

    public void Follow(BaseInteractionEventArgs hover){
        if(hover.interactorObject is XRPokeInteractor){
            XRPokeInteractor interactor = (XRPokeInteractor)hover.interactorObject;

            pokeAttachTransform = interactor.attachTransform;
            offset = visualTarget.position - pokeAttachTransform.position;

            float pokeAngle = Vector3.Angle(offset, visualTarget.TransformDirection(localAxis));

            if(pokeAngle < followAngleThreshold){
                isFollowing = true;
                freeze = false;
            }
        }
    }

    public void Reset(BaseInteractionEventArgs hover){
        if(hover.interactorObject is XRPokeInteractor){
            isFollowing = false;
            freeze = false;
        }
    }

    public void Freeze(BaseInteractionEventArgs hover){
        if(hover.interactorObject is XRPokeInteractor){
            freeze = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(freeze){
            return;
        }
        if(isFollowing){
            Vector3 localTargetPosition = visualTarget.InverseTransformPoint(pokeAttachTransform.position + offset);
            Vector3 constrainedLocalTargetPosition = Vector3.Project(localTargetPosition, localAxis);
            visualTarget.position = visualTarget.TransformPoint(constrainedLocalTargetPosition);
        }
        else{
            visualTarget.localPosition = Vector3.Lerp(visualTarget.localPosition, initialPosition, Time.deltaTime * resetSpeed);
        }
    }

    public void ActivateButton(bool activate){
        isActive = activate;
        if(activate){
            visualObject.GetComponent<MeshRenderer>().material = active;
        }
        else{
            visualObject.GetComponent<MeshRenderer>().material = inactive;
        }
    }

    public bool GetActiveStatus(){
        return isActive;
    }
}
