using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using Uralstech.UGemini;
using Uralstech.UGemini.Chat;
using Uralstech.UGemini.Models;
using System.Threading.Tasks;

public class Gemini : MonoBehaviour
{
    const string gasUrl = "https://script.google.com/macros/s/AKfycbxzaYwRmU4v-icQZF8wvunbuOiS_-0aVur3IsBk00TZwxwbvkfl28BLmI9yaCjE87a0/exec";

    private string inputPrompt;
    private GeminiInputField geminiInputfield;
    private RawImage responseImage;

    // Start is called before the first frame update
    void Start()
    {

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
        StartCoroutine(SendDataToGas());
    }

    private IEnumerator SendImageDataToGas()
    {
        Debug.Log("Running chat request.");
        
        WWWForm form = new WWWForm();
        form.AddField("parameter", inputPrompt);
        UnityWebRequest www = UnityWebRequest.Post(gasUrl, form);

        yield return www.SendWebRequest();
        Texture2D texture = new Texture2D(256, 256);

        if(www. result == UnityWebRequest.Result.Success){
            texture.LoadImage(www.downloadHandler.data);
        }
        else{
            Debug.Log("There was an error");
        }

        responseImage.texture = texture;
    }

    public void EnterImagePrompt(string prompt, RawImage image)
    {
        inputPrompt = prompt;
        responseImage = image;
        StartCoroutine(SendImageDataToGas());
    }

}
