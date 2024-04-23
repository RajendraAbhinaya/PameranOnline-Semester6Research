using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontDoor : MonoBehaviour
{
    public SlidingDoor leftDoor;
    public SlidingDoor rightDoor;
    private AudioSource audioSource;

    void Start(){
        audioSource = this.GetComponent<AudioSource>();
    }
    
    void OnTriggerEnter(Collider col){
        leftDoor.Open(true);
        rightDoor.Open(true);
        audioSource.Play();
    }

    void OnTriggerExit(Collider col){
        leftDoor.Close();
        rightDoor.Close();
        audioSource.Play();
    }
}
