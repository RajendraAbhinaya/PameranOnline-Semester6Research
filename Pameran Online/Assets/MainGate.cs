using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGate : MonoBehaviour
{
    public SlidingDoor leftDoor;
    public SlidingDoor rightDoor;
    
    void OnTriggerEnter(Collider col){
        leftDoor.Close();
        rightDoor.Close();
    }
}
