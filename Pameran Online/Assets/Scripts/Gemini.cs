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
    const string imageRequestPrompt = "Provide a url for an image of the following using the first image from google images and make sure the url is valid: ";

    public Texture2D loadingImage;
    public Texture2D errorImage;
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

        if(www.result == UnityWebRequest.Result.Success){
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
        responseImage.texture = loadingImage;
        
        WWWForm form = new WWWForm();
        form.AddField("parameter", inputPrompt);
        UnityWebRequest www = UnityWebRequest.Post(gasUrl, form);

        yield return www.SendWebRequest();
        
        string response = "";
        if(www.result == UnityWebRequest.Result.Success){
            response = www.downloadHandler.text;
            StartCoroutine(GetTexture(response));
        }
        else{
            responseImage.texture = errorImage;
        }
    }

    private IEnumerator GetTexture(string url){
        Debug.Log("Running image request: " + url);
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
        yield return request.SendWebRequest();

        if(request.isNetworkError || request.isHttpError){
            responseImage.texture = errorImage;
            StartCoroutine(SendImageDataToGas());
        }
        else{
            responseImage.texture = ((DownloadHandlerTexture) request.downloadHandler).texture;
        }
    }

    public void EnterImagePrompt(string prompt, RawImage image)
    {
        inputPrompt = imageRequestPrompt + prompt;
        responseImage = image;
        StartCoroutine(SendImageDataToGas());
    }

}
