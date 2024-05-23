using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public Waypoint currWaypoint;
    public float speed;
    
    private Transform body;
    private Animator animator;
    private CharacterController characterController;
    private bool isWalking = true;
    private float minDistance = 1f;
    private Quaternion targetRotation;
    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();
        characterController = this.GetComponent<CharacterController>();
        body = animator.GetBoneTransform(HumanBodyBones.Chest);
        targetRotation = Quaternion.LookRotation(currWaypoint.position - transform.position);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(isWalking){
            Walk();
        }
        else if(currWaypoint.pointOfInterest != null){
            targetRotation = Quaternion.LookRotation(currWaypoint.pointOfInterest.transform.position - transform.position);
        }
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 0.1f);
    }

    void Walk(){
        Vector3 destination = currWaypoint.position;
        characterController.Move((destination - transform.position - Vector3.up).normalized * speed / 10f);

        float distance = (destination - transform.position).magnitude;
        if(distance < minDistance){
            isWalking = false;
            animator.SetBool("Walk", false);
            Invoke("NewDestination", Random.Range(2f, 5f));
        }
    }

    void NewDestination(){
        currWaypoint = currWaypoint.NextWaypoint();
        targetRotation = Quaternion.LookRotation(currWaypoint.position - transform.position);
        minDistance = Random.Range(0.2f, 2f);
        isWalking = true;
        animator.SetBool("Walk", true);
    }

    /*
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Expo Object") {
            targetLook = other.transform;
            Debug.Log("Enter");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Expo Object") {
            targetLook = null;
            Debug.Log("Exit");
        }
    }
    */
}
