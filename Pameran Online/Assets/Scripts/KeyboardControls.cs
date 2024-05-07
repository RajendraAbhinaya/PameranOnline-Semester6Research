using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardControls : MonoBehaviour
{
    public GameObject cameraOffset;
    private CharacterController characterController;
    private float speed = 0.1f;
    private float rotationSpeed = 3f;
    private float xRotation = 0f;
    private float yRotation = 0f;
    // Start is called before the first frame update
    void Start()
    {
        characterController = this.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.W)){
            characterController.Move((transform.forward - Vector3.up) * speed);
        }

        if(Input.GetKey(KeyCode.S)){
            characterController.Move((-transform.forward - Vector3.up) * speed);
        }

        if(Input.GetKey(KeyCode.A)){
            characterController.Move((-transform.right - Vector3.up) * speed);
        }

        if(Input.GetKey(KeyCode.D)){
            characterController.Move((transform.right - Vector3.up) * speed);
        }

        if(Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0){
            xRotation += Input.GetAxis("Mouse Y") * rotationSpeed;
            xRotation = Mathf.Clamp(xRotation, -30f, 30f);
            yRotation += Input.GetAxis("Mouse X") * rotationSpeed;
            cameraOffset.transform.localRotation = Quaternion.Euler(-xRotation, 0f, 0f);

            transform.rotation = Quaternion.Euler(0f, yRotation, 0f);
        }
    }
}
