using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

public class TextToSpeech : MonoBehaviour
{
    private AudioSource audioSource;
    private List<string> inputTexts;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
        //StartCoroutine(GetTTS());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartTTS(string input){
        StopAllCoroutines();
        if(audioSource.isPlaying){
            audioSource.Stop();
            return;
        }
        inputTexts = new List<string>();
        foreach(string text in input.Split(new string[]{".", "*", "\n"}, StringSplitOptions.RemoveEmptyEntries)){
            inputTexts.Add(text);
        }
        StartCoroutine(GetTTS());
    }

    public IEnumerator GetTTS()
    {             
        foreach(string text in inputTexts){
            string url = "https://translate.google.com/translate_tts?ie=UTF-8&client=tw-ob&tl=en&q=" + text;
            UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(url, AudioType.MPEG);
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success) {
                AudioClip clip = DownloadHandlerAudioClip.GetContent(www);
                audioSource.clip = clip;
                audioSource.Play();
                yield return new WaitForSeconds(clip.length);
            }
            else {
                Debug.Log("TTS Error");
            }
        }
    }
}
