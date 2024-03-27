using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarInteraction : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleOpen(){
        if(animator.GetBool("isOpen") == true){
            animator.SetBool("isOpen", false);
        }
        else{
            animator.SetBool("isOpen", true);
        }
    }
}
