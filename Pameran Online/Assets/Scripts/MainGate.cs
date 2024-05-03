using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGate : MonoBehaviour
{
    public SlidingDoor leftDoor;
    public SlidingDoor rightDoor;
    public BoxCollider mainGateCollider;
    public BoxCollider expoHallCollider;
    private bool isEntering = true;
    
    void OnTriggerEnter(Collider col){
        if(col.gameObject.tag == "Player"){
            if(isEntering){
                expoHallCollider.enabled = false;
                mainGateCollider.enabled = true;
                isEntering = false;
            }
            else{
                expoHallCollider.enabled = true;
                mainGateCollider.enabled = false;
                isEntering = true;
                leftDoor.Close();
                rightDoor.Close();
            }
        }
    }
}
