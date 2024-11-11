using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using Uralstech.UGemini;
using Uralstech.UGemini.Chat;
using Uralstech.UGemini.Models;
using System.Threading.Tasks;
using TMPro;

public class Gemini : MonoBehaviour
{
    public struct TMP_Prompt{
        public string prompt;
        public TMP_Text tmpText;

        public TMP_Prompt(string prompt, TMP_Text tmpText){
            this.prompt = prompt;
            this.tmpText = tmpText;
        }
    }

    const string gasUrl = "https://script.google.com/macros/s/AKfycbxzaYwRmU4v-icQZF8wvunbuOiS_-0aVur3IsBk00TZwxwbvkfl28BLmI9yaCjE87a0/exec";
    const string imageRequestPrompt = "Provide a url for an image of the following using the first image from google images and make sure the url is valid: ";

    public Texture2D loadingImage;
    public Texture2D errorImage;
    private string inputPrompt;
    private RawImage responseImage;
    private List<TMP_Prompt> prompts = new List<TMP_Prompt>();
    private Coroutine tmpCoroutine;

    void Start()
    {
        tmpCoroutine = StartCoroutine(SendDataToGasTMP());
    }

    private IEnumerator SendDataToGasGeminiInputField(GeminiInputField geminiInputField)
    {
        Debug.Log("Running chat request.");
        geminiInputField.SetResponseText("Running chat request.");
        
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
        geminiInputField.SetResponseText(response);
    }

    private IEnumerator SendDataToGasTMP()
    {
        while(true){
            if(prompts.Count == 0){
                yield return new WaitForSeconds(1f);
            }
            else{
                Debug.Log("Running chat request.");
                
                WWWForm form = new WWWForm();
                form.AddField("parameter", prompts[0].prompt);
                UnityWebRequest www = UnityWebRequest.Post(gasUrl, form);

                yield return www.SendWebRequest();
                string response = "";

                if(www.result == UnityWebRequest.Result.Success){
                    response = www.downloadHandler.text;
                    response.Replace("*", "");
                }
                else{
                    response = "There was an error";
                }

                Debug.Log(response);
                
                prompts[0].tmpText.text = response;
                prompts.RemoveAt(0);
            }
        }
        
    }

    public void EnterInputFieldPrompt(string prompt, GeminiInputField inputField)
    {
        inputPrompt = prompt;
        StartCoroutine(SendDataToGasGeminiInputField(inputField));
    }

    public void EnterTextPrompt(string prompt, TMP_Text text)
    {
        prompts.Add(new TMP_Prompt(prompt, text));
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

    public void ClearPrompts(){
        StopCoroutine(tmpCoroutine);
        prompts.Clear();
        tmpCoroutine = StartCoroutine(SendDataToGasTMP());
    }

}
