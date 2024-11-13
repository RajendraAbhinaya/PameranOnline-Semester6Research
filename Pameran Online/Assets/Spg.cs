using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Spg : GeminiInputField
{
    public GameObject canvas;
    public List<TMP_Text> texts = new List<TMP_Text>();
    public TextToSpeech ttsScript;
    private int textIndex = 0;
    private List<GameObject> customers = new List<GameObject>();

    // Update is called once per frame
    void Update()
    {
        if(customers.Count > 0){
            Quaternion direction = Quaternion.LookRotation(customers[0].transform.position - transform.position);
            transform.rotation = Quaternion.Lerp(transform.rotation, direction, 4 * Time.deltaTime);
        }
        Vector3 distance = player.transform.position - transform.position;
        if(distance.magnitude > 10f){
            canvas.SetActive(false);
        }
    }

    public void SetTextIndex(int index){
        textIndex = index;
    }

    public void ToggleCanvas(){
        canvas.SetActive(!canvas.activeSelf);
    }

    public void CallTTS(){
        if(canvas.activeSelf){
            ttsScript.StartTTS(response.text);
        }
        else{
            ttsScript.StartTTS(texts[textIndex].text);
        }
    }

    public void OnEnter(string input)
    {
        gemini.EnterInputFieldPrompt("You are a car salesperson who only asnwers questions regarding the following car: " + texts[0].text + " Question: " + input, this);
    }

    void OnTriggerEnter(Collider col){
        if(col.gameObject.tag == "Player"){
            customers.Insert(0, col.gameObject);
        }
        else{
            customers.Add(col.gameObject);
        }
    }

    void OnTriggerExit(Collider col){
        customers.Remove(col.gameObject);
    }
}
