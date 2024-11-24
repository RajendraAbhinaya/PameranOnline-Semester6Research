using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Exit : MonoBehaviour
{
    public GameObject canvas;
    public float fadeDuration;
    private CanvasGroup canvasGroup;
    private Coroutine fadeCoroutine;
    // Start is called before the first frame update
    void Start()
    {
        canvasGroup = canvas.GetComponent<CanvasGroup>();
        fadeCoroutine = StartCoroutine(FadeOut());
    }

    public void ExitGame(){
        Application.Quit();
    }

    void OnTriggerEnter(Collider col){
        if(col.gameObject.tag =="Player"){
            StopCoroutine(fadeCoroutine);
            fadeCoroutine = StartCoroutine(FadeIn());
        }
    }

    void OnTriggerExit(Collider col){
        if(col.gameObject.tag =="Player"){
            StopCoroutine(fadeCoroutine);
            fadeCoroutine = StartCoroutine(FadeOut());
        }
    }

    public IEnumerator FadeIn(){
        canvas.SetActive(true);
        while(canvasGroup.alpha < 1f){
            canvasGroup.alpha += 0.01f * (1/fadeDuration);
            yield return new WaitForSeconds(0.01f);
        }
    }

    public IEnumerator FadeOut(){
        while(canvasGroup.alpha > 0f){
            canvasGroup.alpha -= 0.01f * (1/fadeDuration);
            yield return new WaitForSeconds(0.01f);
        }
        canvas.SetActive(false);
    }
}
