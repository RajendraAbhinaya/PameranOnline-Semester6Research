using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class MainMenu : MonoBehaviour
{
    public ActionBasedContinuousMoveProvider continuousMove;
    public ActionBasedContinuousTurnProvider continuousTurn;
    public TeleportationProvider teleportationProvider;
    public ActionBasedControllerManager controllerManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateXROrigin(){
        continuousMove.enabled = true;
        continuousTurn.enabled = true;
        teleportationProvider.enabled = true;
        controllerManager.enabled = true;
        Destroy(this.gameObject);
    }
}
