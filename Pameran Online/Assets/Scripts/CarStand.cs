using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;

public class CarStand : MonoBehaviour
{
    public GameObject canvas;
    public GameObject spg;
    public ContentSizeFitter descriptionContentSizer;
    public ContentSizeFitter featuresContentSizer;
    public TMP_Text carName;
    public TMP_Text description;
    public TMP_Text features;
    public TMP_Text length;
    public TMP_Text width;
    public TMP_Text height;
    public TMP_Text price;
    public GameObject center;
    public GameObject[] panels;
    public GameObject prevButton;
    public GameObject nextButton;
    public bool activateSalesperson;

    private int panelAmount;
    private int currPanel = 0;
    private GameObject spawnedCar;
    private XRBaseInteractable interactable;
    private XRRayInteractor interactor;
    private Transform startingHandPosition;
    private Quaternion prevStandRotation;
    private float startingHandRotation = 0f;
    private bool isRotating = false;
    private Quaternion startingRotation;
    private bool reset = false;
    private Gemini gemini;
    private Spg spgScript;
    private CanvasGroup canvasGroup;
    private Coroutine fadeCoroutine;
    private Car car;

    // Start is called before the first frame update
    void Awake()
    {
        interactable = GetComponent<XRBaseInteractable>();
        interactable.selectEntered.AddListener(Spin);
        interactable.selectExited.AddListener(Stop);

        startingRotation = transform.rotation;
        gemini = GameObject.FindWithTag("GeminiAPI").GetComponent<Gemini>();
        canvasGroup = canvas.GetComponent<CanvasGroup>();
        spgScript = spg.GetComponent<Spg>();
        StartCoroutine(ResetContentSizer());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(isRotating){
            //Rotates the stand based on the y rotation of the controller.
            float handRotation = startingHandPosition.localRotation.y - startingHandRotation;
            transform.Rotate(new Vector3(0, handRotation, 0));
        }

         //Reset the stand's rotation.
        if(reset){
            transform.rotation = Quaternion.Lerp(transform.rotation, startingRotation, 0.2f);
            if(Mathf.Abs(transform.rotation.y - startingRotation.y) < 0.01f){
                reset = false;
            }
        }
    }

    //Rotates the stand based on the y rotation of the controller. Called when the select button is pressed
    public void Spin(BaseInteractionEventArgs hover){
        if(hover.interactorObject is XRRayInteractor){
            interactor = (XRRayInteractor)hover.interactorObject;

            startingHandPosition = interactor.gameObject.transform.parent.transform;
            startingHandRotation = interactor.gameObject.transform.parent.transform.localRotation.y;;
            isRotating = true;
        }
    }

    //Stops the stand from rotating. Called when the select button is released
    public void Stop(BaseInteractionEventArgs hover){
        if(hover.interactorObject is XRRayInteractor){
            isRotating = false;
            prevStandRotation = transform.rotation;
        }
    }

    //Reset the stand's rotation. Called when the activate button is pressed
    public void Reset(){
        reset = true;
        isRotating = false;
        prevStandRotation = startingRotation;
    }

    //Sets the stand using the values from the scriptable object passed through the function
    public void SetCar(Car carObject){
        car = carObject;
        spawnedCar = Instantiate(car.prefab, center.transform.position, Quaternion.identity);
        spawnedCar.transform.SetParent(center.transform);
        spg.SetActive(activateSalesperson);

        carName.text = car.name;
        /*
        description.text = car.description;
        features.text = car.features;
        */
        length.text = "Length: " + car.length;
        width.text = "Width: " + car.width;
        height.text = "Height: " + car.height;
        price.text = "Price: Rp. " + car.price;
        

        gemini.EnterTextPrompt("Give a description excluding features of the following car: " + car.name, description);
        gemini.EnterTextPrompt("List out four key features of the following car: " + car.name, features);
        /*
        gemini.EnterTextPrompt("Give the length formatted 'Length: mm' of the following car in millimeters: " + car.name, length);
        gemini.EnterTextPrompt("Give the width formatted 'Width: mm' of the following car in millimeters: " + car.name, width);
        gemini.EnterTextPrompt("Give the height formatted 'Height: mm' of the following car in millimeters: " + car.name, height);
        gemini.EnterTextPrompt("Give the price formatted 'Price: Rp. ' of the following car in Indonesian Rupiah" + car.name, price);
        */

        canvas.SetActive(true);
        fadeCoroutine = StartCoroutine(FadeIn());
        currPanel = 0;
        spgScript.SetTextIndex(currPanel);
        prevButton.SetActive(false);
        nextButton.SetActive(true);
        panelAmount = panels.Length;
        panels[currPanel].SetActive(true);
    }

    //Used to go back to a previous panel
    public void Prev(){
        nextButton.SetActive(true);
        panels[currPanel].SetActive(false);
        currPanel--;
        spgScript.SetTextIndex(currPanel);
        panels[currPanel].SetActive(true);
        if(currPanel == 0){
            prevButton.SetActive(false);
        }

        DefaultText();
    }

    //Used to go to the next page
    public void Next(){
        prevButton.SetActive(true);
        panels[currPanel].SetActive(false);
        currPanel++;
        spgScript.SetTextIndex(currPanel);
        panels[currPanel].SetActive(true);
        if(currPanel == panelAmount-1){
            nextButton.SetActive(false);
        }

        DefaultText();
    }

    //Remove the current car and reset the stand's rotation
    public void DestroyCar(){
        transform.rotation = startingRotation;
        Destroy(spawnedCar);
        car = null;
        spg.SetActive(false);
        canvas.SetActive(false);
        panels[currPanel].SetActive(false);
    }

    public void ToggleCanvas(){
        if(fadeCoroutine != null){
            StopCoroutine(fadeCoroutine);
        }
        if(!canvas.activeSelf && car != null){
            fadeCoroutine = StartCoroutine(FadeIn());
        }
        else{
            fadeCoroutine = StartCoroutine(FadeOut());
        }
    }

    public IEnumerator FadeIn(){
        canvas.SetActive(true);
        while(canvasGroup.alpha < 1f){
            canvasGroup.alpha += 0.1f;
            yield return new WaitForSeconds(0.01f);
        }
    }

    public IEnumerator FadeOut(){
        while(canvasGroup.alpha > 0f){
            canvasGroup.alpha -= 0.1f;
            yield return new WaitForSeconds(0.01f);
        }
        canvas.SetActive(false);
    }

    IEnumerator ResetContentSizer(){
        while(true){
            descriptionContentSizer.enabled = false;
            descriptionContentSizer.enabled = true;
            featuresContentSizer.enabled = false;
            featuresContentSizer.enabled = true;
            yield return new WaitForSeconds(1f);
        }
    }

    void DefaultText(){
        if(description.text == "There was an error"){
            description.text = car.description;
        }

        if(features.text == "There was an error"){
            features.text = car.features;
        }
    }
}
