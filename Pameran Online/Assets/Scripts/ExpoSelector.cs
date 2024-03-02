using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ExpoSelector : MonoBehaviour
{
    public TMP_Text expoName;
    public TMP_Text errorMessage;
    private GameObject expo;

    void OnTriggerEnter(Collider col){
        expo = col.gameObject;
        if(expo.tag == "Expo Select"){
            expoName.text = expo.name;
        }
    }

    void OnTriggerExit(Collider col){
        if(expo.tag == "Expo Select"){
            expo = null;
            expoName.text = "";
            errorMessage.text = "";
        }
    }

    public void ButtonSelect(){
        if(expoName.text == "Car Expo"){
            SceneManager.LoadScene("Car Expo");
        }
        else{
            errorMessage.text = "Expo WIP";
        }
    }
}
