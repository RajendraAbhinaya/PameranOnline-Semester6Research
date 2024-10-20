using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GeminiInputField : MonoBehaviour
{
    public TMP_Text response;
    public Gemini gemini;
    public ContentSizeFitter contentSizer;

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
}
