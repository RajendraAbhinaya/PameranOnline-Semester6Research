using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontDoor : MonoBehaviour
{
    public SlidingDoor leftDoor;
    public SlidingDoor rightDoor;
    
    void OnTriggerStay(Collider col){
        leftDoor.Open(true);
        rightDoor.Open(true);
    }

    void OnTriggerExit(Collider col){
        leftDoor.Close();
        rightDoor.Close();
    }
}
