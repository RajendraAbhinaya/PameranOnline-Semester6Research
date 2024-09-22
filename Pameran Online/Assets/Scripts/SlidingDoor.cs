using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingDoor : MonoBehaviour
{
    private Vector3 initialPosition;
    public Vector3 openPosition;
    public PokeButton pokeButton;
    public AudioClip openAudio;
    public AudioClip closeAudio;
    private bool isOpen = false;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
        initialPosition = transform.position;
    }

    void Update(){
        float distance = (transform.position - openPosition).magnitude;
        if(isOpen){
            transform.position = Vector3.Lerp(transform.position, openPosition, Time.deltaTime * 5);
        }
        else{
            transform.position = Vector3.Lerp(transform.position, initialPosition, Time.deltaTime * 5);
        }
    }

    public void Open(bool noButton){
        if(pokeButton.GetActiveStatus() || noButton){
            if(!isOpen){
                audioSource.PlayOneShot(closeAudio);
            }
            isOpen = true;
        }
    }

    public void Close(){
        if(isOpen){
            audioSource.PlayOneShot(closeAudio);
        }
        isOpen = false;
    }
}
