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
    const string gasUrl = "https://script.google.com/macros/s/AKfycbxzaYwRmU4v-icQZF8wvunbuOiS_-0aVur3IsBk00TZwxwbvkfl28BLmI9yaCjE87a0/exec";

    private string inputPrompt = "What are the newest toyota releases in indonesia";
    private GeminiInputField geminiInputfield;
    List<GeminiContent> _chatHistory = new();

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(SendDataToGas());
    }

    private IEnumerator SendDataToGas()
    {
        Debug.Log("Running chat request.");
        geminiInputfield.SetResponseText("Running chat request.");
        
        WWWForm form = new WWWForm();
        form.AddField("parameter", inputPrompt);
        UnityWebRequest www = UnityWebRequest.Post(gasUrl, form);

        yield return www.SendWebRequest();
        string response = "";

        if(www. result == UnityWebRequest.Result.Success){
            response = www.downloadHandler.text;
        }
        else{
            response = "There was an error";
        }
        Debug.Log(response);
        geminiInputfield.SetResponseText(response);
    }

    public void EnterPrompt(string prompt, GeminiInputField inputField)
    {
        inputPrompt = prompt;
        geminiInputfield = inputField;
        //RunChatRequest();
        StartCoroutine(SendDataToGas());
    }

    private async void RunChatRequest()
    {
        Debug.Log("Running chat request.");
        geminiInputfield.SetResponseText("Running chat request.");
    
        GeminiChatResponse response = await GeminiManager.Instance.Request<GeminiChatResponse>(
            new GeminiChatRequest(GeminiModel.Gemini1_5Pro, true)
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
