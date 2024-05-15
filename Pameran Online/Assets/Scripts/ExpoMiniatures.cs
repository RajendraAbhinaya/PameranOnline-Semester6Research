using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpoMiniatures : MonoBehaviour
{
    public AudioClip collisionAudio;
    private AudioSource audioSource;
    private Rigidbody rigidBody;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
        rigidBody = this.GetComponent<Rigidbody>();
        Invoke("SetVolume", 2f);
    }

    void SetVolume(){
        audioSource.volume = 1f;
    }

    void OnCollisionEnter(Collision col){
        float velocity = Mathf.Clamp(Vector3.Magnitude(rigidBody.velocity) / 5f, 0.1f, 1f);
        audioSource.PlayOneShot(collisionAudio, velocity);
    }
}
