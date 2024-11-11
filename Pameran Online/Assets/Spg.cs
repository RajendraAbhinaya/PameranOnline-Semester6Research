using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Spg : MonoBehaviour
{
    public TMP_Text[] ttsText;
    public TextToSpeech ttsScript;
    private int textIndex = 0;
    private List<Transform> visitors = new List<Transform>();

    public void TTS(){
        ttsScript.StartTTS(ttsText[textIndex].text.Replace("\n", "").Replace("\r", ""));
    }

    public void SetTextIndex(int index){
        textIndex = index;
    }

    void OnTriggerEnter(Collider col){
        if(col.gameObject.tag == "Player"){
            visitors.Insert(0, col.transform);
        }
        else if(col.gameObject.tag == "NPC"){
            visitors.Add(col.transform);
        }
    }

    void OnTriggerExit(Collider col){
        visitors.Remove(col.transform);
    }

    void Update(){
        if(visitors.Count > 0){
            Quaternion direction = Quaternion.LookRotation(visitors[0].position - transform.position);
            transform.rotation = Quaternion.Lerp(transform.rotation, direction, 0.5f);
        }
    }
}
