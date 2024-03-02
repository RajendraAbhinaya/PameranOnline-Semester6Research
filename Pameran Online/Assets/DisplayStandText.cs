using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayStandText : MonoBehaviour
{
    public TMP_Text expoName;
    public GameObject canvas;
    private GameObject expo;

    void OnTriggerEnter(Collider col){
        expo = col.gameObject;
        if(expo.tag == "Expo Select"){
            expoName.text = expo.name;
            canvas.SetActive(true);
        }
    }

    void OnTriggerExit(Collider col){
        if(expo.tag == "Expo Select"){
            expo = null;
            expoName.text = "";
            canvas.SetActive(false);
        }
    }
}
