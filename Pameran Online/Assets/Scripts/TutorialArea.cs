using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialArea : MonoBehaviour
{
    public GameObject tutorialCanvas;
    public CanvasGroup canvasGroup;
    public float fadeDuration;
    public bool facePlayer;
    private Coroutine fadeCoroutine;
    private GameObject player;

    void Start(){
        fadeCoroutine = StartCoroutine(FadeOut(false));
        player = GameObject.FindWithTag("Player");
    }

    void Update(){
        if(facePlayer){
            Vector3 diff = new Vector3(player.transform.position.x - tutorialCanvas.transform.position.x, 0, player.transform.position.z - tutorialCanvas.transform.position.z);
            Quaternion direction = Quaternion.LookRotation(diff);
            tutorialCanvas.transform.rotation = Quaternion.Lerp(tutorialCanvas.transform.rotation, direction, 4 * Time.deltaTime);
        }
    }

    void OnTriggerEnter(Collider col){
        if(col.gameObject == player){
            StopCoroutine(fadeCoroutine);
            fadeCoroutine = StartCoroutine(FadeIn());
        }
    }

    void OnTriggerExit(Collider col){
        if(col.gameObject == player){
            StopCoroutine(fadeCoroutine);
            fadeCoroutine = StartCoroutine(FadeOut(false));
        }
    }

    public void DestroyTutorial(){
        StopCoroutine(fadeCoroutine);
        fadeCoroutine = StartCoroutine(FadeOut(true));
    }

    public IEnumerator FadeIn(){
        tutorialCanvas.SetActive(true);
        while(canvasGroup.alpha < 1f){
            canvasGroup.alpha += 0.01f * (1/fadeDuration);
            yield return new WaitForSeconds(0.01f);
        }
    }

    public IEnumerator FadeOut(bool destroy){
        while(canvasGroup.alpha > 0f){
            canvasGroup.alpha -= 0.01f * (1/fadeDuration);
            yield return new WaitForSeconds(0.01f);
        }
        tutorialCanvas.SetActive(false);
        if(destroy){
            Destroy(this.gameObject);
        }
    }
}
