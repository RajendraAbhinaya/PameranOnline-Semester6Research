using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GeminiInputField : MonoBehaviour
{
    public TMP_Text response;
    public ContentSizeFitter contentSizer;
    protected Gemini gemini;
    protected GameObject player;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        gemini = GameObject.FindWithTag("GeminiAPI").GetComponent<Gemini>();
    }

    public void OnEnter(string input)
    {
        gemini.EnterInputFieldPrompt(input, this);
    }

    public void SetResponseText(string geminiResponse)
    {
        response.text = geminiResponse;
        Invoke("EnableContentSizer", 0.1f);
    }

    void EnableContentSizer()
    {
        contentSizer.enabled = false;
        contentSizer.enabled = true;
    }

    public void ToggleKeyboard(bool toggle){
        player.GetComponent<KeyboardControls>().enabled = toggle;
    }
}
