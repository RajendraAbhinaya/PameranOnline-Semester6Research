using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialArea : MonoBehaviour
{
    public GameObject tutorialCanvas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider col){
        if(col.gameObject.tag == "Player"){
            tutorialCanvas.SetActive(true);
        }
    }

    void OnTriggerExit(Collider col){
        if(col.gameObject.tag == "Player"){
            tutorialCanvas.SetActive(false);
        }
    }

    public void DestroyTutorial(){
        tutorialCanvas.SetActive(false);
        Destroy(this.gameObject, 3f);
    }
}
