using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GeminiInputField : MonoBehaviour
{
    public TMP_Text response;
    public Gemini gemini;

    public void OnEnter(string input)
    {
        gemini.EnterPrompt(input, this);
    }

    public void SetResponseText(string geminiResponse)
    {
        response.text = geminiResponse;
    }
}
