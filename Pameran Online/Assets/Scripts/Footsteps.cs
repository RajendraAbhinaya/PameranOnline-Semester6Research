using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Footsteps : MonoBehaviour
{
    public InputActionProperty leftHandMove;
    public AudioClip footstep;
    public float footstepInterval;
    private AudioSource audioSource;
    private float nextFootstep = 0f;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || leftHandMove.action?.ReadValue<Vector2>() != Vector2.zero){
            if(Time.time > nextFootstep){
                audioSource.PlayOneShot(footstep, 1f);
                nextFootstep = Time.time + footstepInterval;
            }
        }
    }
}