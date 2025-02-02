using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarInteraction : MonoBehaviour
{
    public AudioClip openAudio;
    public AudioClip closeAudio;
    private Animator animator;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();
        audioSource = this.GetComponent<AudioSource>();
    }

    public void ToggleOpen(){
        if(animator.GetBool("isOpen") == true){
            animator.SetBool("isOpen", false);
            audioSource.PlayOneShot(closeAudio);
        }
        else{
            animator.SetBool("isOpen", true);
            audioSource.PlayOneShot(openAudio);
        }
    }
}
