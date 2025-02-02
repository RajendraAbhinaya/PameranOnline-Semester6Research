using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Credits : MonoBehaviour
{
    public GameObject[] panels;
    public GameObject prevButton;
    public GameObject nextButton;
    private int panelAmount;
    private int currPanel = 0;
    // Start is called before the first frame update
    void Start()
    {
        panelAmount = panels.Length;
    }

    //Used to go back to a previous panel
    public void Prev(){
        nextButton.SetActive(true);
        panels[currPanel].SetActive(false);
        currPanel--;
        panels[currPanel].SetActive(true);
        if(currPanel == 0){
            prevButton.SetActive(false);
        }
    }

    //Used to go to the next page
    public void Next(){
        prevButton.SetActive(true);
        panels[currPanel].SetActive(false);
        currPanel++;
        panels[currPanel].SetActive(true);
        if(currPanel == panelAmount-1){
            nextButton.SetActive(false);
        }
    }

    public void OpenLink(string url){
        Application.OpenURL(url);
    }
}
