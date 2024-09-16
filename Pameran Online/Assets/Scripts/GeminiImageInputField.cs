using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GeminiImageInputField : MonoBehaviour
{
    public RawImage response;
    public Gemini gemini;
    // Start is called before the first frame update
    public void OnEnter(string input)
    {
        gemini.EnterImagePrompt(input, response);
    }

    public void SetResponseText(Texture2D geminiResponse)
    {
        response.texture = geminiResponse;
    }
}
