using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;

public class ExpoSelector : MonoBehaviour
{
    public TMP_Text expoName;
    public TMP_Text errorMessage;
    public GameObject canvas;
    public PokeButton pokeButton;
    public SlidingDoor leftDoor;
    public SlidingDoor rightDoor;
    private GameObject expo;
    private GameObject preview = null;

    /*
    void OnTriggerEnter(Collider col){
        expo = col.gameObject;
        if(expo.tag == "Expo Select"){
            expoName.text = expo.name;
            canvas.SetActive(true);
            if(expoName.text == "Car Expo"){
                pokeButton.ActivateButton(true);
            }
            else{
                pokeButton.ActivateButton(false);
            }
        }
    }

    void OnTriggerExit(Collider col){
        if(expo.tag == "Expo Select"){
            expo = null;
            expoName.text = "";
            errorMessage.text = "";
            canvas.SetActive(false);
            pokeButton.ActivateButton(false);
        }
    }
    */

    void FixedUpdate(){
        transform.Rotate(new Vector3(0f, 0.3f, 0f));
    }

    public void SelectExpo(GameObject expo){
        if(expo.tag == "Expo Select"){
            errorMessage.text = "";

            Destroy(preview);
            preview = Instantiate(expo, transform.position, Quaternion.identity);
            preview.transform.parent = transform;
            Destroy(preview.GetComponent<XRSimpleInteractable>());

            expoName.text = expo.name;
            canvas.SetActive(true);
            if(expoName.text == "Car Expo"){
                pokeButton.ActivateButton(true);
            }
            else{
                pokeButton.ActivateButton(false);
                leftDoor.Close();
                rightDoor.Close();
            }
        }
    }

    public void ButtonSelect(){
        if(expoName.text != "Car Expo"){
            errorMessage.text = "Expo WIP";
        }
    }
}
