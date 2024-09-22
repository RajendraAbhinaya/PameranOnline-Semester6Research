using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    public GameObject exitPanel;
    // Start is called before the first frame update
    void Start()
    {
        exitPanel.SetActive(false);
    }

    public void ActivateExitPanel(bool activate){
        exitPanel.SetActive(activate);
    }

    public void ExitGame(){
        Application.Quit();
    }

    void OnTriggerEnter(Collider col){
        ActivateExitPanel(true);
    }

    void OnTriggerExit(Collider col){
        ActivateExitPanel(false);
    }
}
