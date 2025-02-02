using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class TutorialPanel : MonoBehaviour
{
    [System.Serializable]
    public struct Tutorial{
        public GameObject panel;
        public InputActionProperty joystickInput;
        public Vector2 threshholdValue;
    }

    public CanvasGroup canvasGroup;
    public float fadeDuration;
    public List<Tutorial> tutorialPanels = new List<Tutorial>();
    private int index = 0;
    private int tutorialCount;
    private Coroutine fadeCoroutine = null;
    private bool canTransition = true;
    // Start is called before the first frame update
    void Start()
    {
        tutorialCount = tutorialPanels.Count;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 joystickDirection = tutorialPanels[index].joystickInput.action.ReadValue<Vector2>();
        float dotProduct = Vector2.Dot(joystickDirection.normalized, tutorialPanels[index].threshholdValue.normalized);
        if((dotProduct > 0.9f || Input.GetKeyDown(KeyCode.F)) && canTransition){
            canTransition = false;
            fadeCoroutine = StartCoroutine(NextPanel());
        }
    }

    public IEnumerator NextPanel(){
        while(canvasGroup.alpha > 0f){
            canvasGroup.alpha -= 0.01f * (1/fadeDuration);
            yield return new WaitForSeconds(0.01f);
        }

        tutorialPanels[index].panel.SetActive(false);
        if(index < tutorialCount-1){
            index++;
            tutorialPanels[index].panel.SetActive(true);
            while(canvasGroup.alpha < 1f){
                canvasGroup.alpha += 0.01f * (1/fadeDuration);
                yield return new WaitForSeconds(0.01f);
            }
            canTransition = true;
        }
        else{
            Destroy(this.gameObject, 5f);
        }
        StopCoroutine(fadeCoroutine);
    }
}  
