using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingDoor : MonoBehaviour
{
    private Vector3 initialPosition;
    public Vector3 openPosition;
    public PokeButton pokeButton;
    private bool isOpen = false;
    // Start is called before the first frame update
    void Start()
    {
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

    public void Open(){
        if(pokeButton.GetActiveStatus()){
            isOpen = true;
        }
    }

    public void Close(){
        isOpen = false;
    }
}
