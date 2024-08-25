using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Uralstech.UGemini;
using Uralstech.UGemini.Chat;
using Uralstech.UGemini.Models;
using System.Threading.Tasks;

public class Gemini : MonoBehaviour
{
    const string gasUrl = "https://script.google.com/macros/s/AKfycbzZt39wdJ8fP7wUk4pb7qW3XoA25XQm3c_AnCMwVNWWq4gtvWg0QQAD-TbZSY0eO-kC/exec";

    private string inputPrompt;
    private GeminiInputField geminiInputfield;
    List<GeminiContent> _chatHistory = new();

    // Start is called before the first frame update
    void Start()
    {

    }
 
    async Task<string> OnChat(string text)
    {
        _chatHistory.Add(GeminiContent.GetContent(text, GeminiRole.User));
        GeminiChatResponse response = await GeminiManager.Instance.Request<GeminiChatResponse>(
            new GeminiChatRequest(GeminiModel.Gemini1_5Flash)
            {
                Contents = _chatHistory.ToArray(),
            }
        );
        
        _chatHistory.Add(response.Candidates[0].Content);
        Debug.Log(response.Parts[0].Text);
        return response.Parts[0].Text;
    }

    public void EnterPrompt(string prompt, GeminiInputField inputField)
    {
        inputPrompt = prompt;
        geminiInputfield = inputField;
        RunChatRequest();
    }

    private async void RunChatRequest()
    {
        Debug.Log("Running chat request.");
        geminiInputfield.SetResponseText("Running chat request.");
    
        GeminiChatResponse response = await GeminiManager.Instance.Request<GeminiChatResponse>(
            new GeminiChatRequest(GeminiModel.Gemini1_5Pro)
            {
                Contents = new GeminiContent[]
                {
                    GeminiContent.GetContent(inputPrompt)
                },
            }
        );
        
        Debug.Log($"Gemini's response: {response.Parts[^1].Text}");
        geminiInputfield.SetResponseText(response.Parts[^1].Text);
    }
}
