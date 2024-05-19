using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Props : MonoBehaviour
{
    public AudioClip collisionAudio;

    private AudioSource audioSource;
    private Rigidbody rigidBody;

    private Vector3 startingPosition;
    private Quaternion startingRotation;
    private bool isBeingHeld = false;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
        rigidBody = this.GetComponent<Rigidbody>();
        Invoke("SetVolume", 2f);

        startingPosition = transform.position;
        startingRotation = transform.rotation;
        Invoke("Reset", 60f);
    }

    void SetVolume(){
        audioSource.volume = 1f;
    }

    void OnCollisionEnter(Collision col){
        float velocity = Mathf.Clamp(Vector3.Magnitude(rigidBody.velocity) / 5f, 0.1f, 1f);
        audioSource.PlayOneShot(collisionAudio, velocity);
    }

    public void Reset(){
        if(!isBeingHeld){
            Instantiate(this.gameObject, startingPosition, startingRotation);
            Destroy(this.gameObject);
        }
        else{
            Invoke("Reset", 60f);
        }
    }

    public void SetHeld(bool held){
        isBeingHeld = held;
    }
}
