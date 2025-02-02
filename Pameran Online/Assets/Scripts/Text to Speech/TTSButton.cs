using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TTSButton : MonoBehaviour
{
    public TMP_Text ttsText;
    public TextToSpeech ttsScript;

    public void TTS(){
        ttsScript.StartTTS(ttsText.text.Replace("\n", "").Replace("\r", ""));
    }
}
