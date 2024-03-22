using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ExpoSelector : MonoBehaviour
{
    public TMP_Text expoName;
    public TMP_Text errorMessage;
    public GameObject canvas;
    public PokeButton pokeButton;
    private GameObject expo;

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

    public void ButtonSelect(){
        if(expoName.text != "Car Expo"){
            errorMessage.text = "Expo WIP";
        }
    }
}
