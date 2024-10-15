using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class GuardNPC : MonoBehaviour
{
    public GameObject canvas;
    public XRSimpleInteractable simpleInteractible;
    public GameObject questionsPanel;
    public GameObject answersPanel;
    public RectTransform Content;
    private RectTransform questionsTransform;
    private RectTransform answersTransform;
    // Start is called before the first frame update
    void Start()
    {
        questionsTransform = questionsPanel.GetComponent<RectTransform>();
        answersTransform = answersPanel.GetComponent<RectTransform>();
    }

    public void SwitchPanels(){
        if(questionsPanel.activeSelf == true){
            questionsPanel.SetActive(false);
            answersPanel.SetActive(true);
            Content.sizeDelta = new Vector2(0, answersTransform.sizeDelta.y);
        }
        else{
            questionsPanel.SetActive(true);
            answersPanel.SetActive(false);
            Content.sizeDelta = new Vector2(0, questionsTransform.sizeDelta.y);
        }
    }

    public void ActivateCanvas(bool activate){
        canvas.SetActive(activate);
        simpleInteractible.enabled = !activate;
    }
}
