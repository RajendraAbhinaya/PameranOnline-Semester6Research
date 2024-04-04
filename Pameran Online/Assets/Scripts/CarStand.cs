using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;

public class CarStand : MonoBehaviour
{
    //public Car car;
    public GameObject canvas;
    public TMP_Text carName;
    public TMP_Text description;
    public TMP_Text length;
    public TMP_Text width;
    public TMP_Text height;
    public TMP_Text price;
    public GameObject center;
    public GameObject[] panels;
    public GameObject prevButton;
    public GameObject nextButton;

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
    // Start is called before the first frame update
    void Awake()
    {
        interactable = GetComponent<XRBaseInteractable>();
        interactable.selectEntered.AddListener(Follow);
        interactable.selectExited.AddListener(Stop);

        startingRotation = transform.rotation;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(isRotating){
            float handRotation = startingHandPosition.localRotation.y - startingHandRotation;
            transform.Rotate(new Vector3(0, -handRotation, 0));
        }

        if(reset){
            transform.rotation = Quaternion.Lerp(transform.rotation, startingRotation, 0.2f);
            if(Mathf.Abs(transform.rotation.y - startingRotation.y) < 0.01f){
                reset = false;
            }
        }
    }

    public void Follow(BaseInteractionEventArgs hover){
        if(hover.interactorObject is XRRayInteractor){
            interactor = (XRRayInteractor)hover.interactorObject;

            startingHandPosition = interactor.gameObject.transform.parent.transform;
            startingHandRotation = interactor.gameObject.transform.parent.transform.localRotation.y;;
            isRotating = true;
        }
    }

    public void Stop(BaseInteractionEventArgs hover){
        if(hover.interactorObject is XRRayInteractor){
            isRotating = false;
            prevStandRotation = transform.rotation;
        }
    }

    public void Reset(){
        reset = true;
        isRotating = false;
        prevStandRotation = startingRotation;
    }

    public void SetCar(Car car){
        spawnedCar = Instantiate(car.prefab, center.transform.position, Quaternion.identity);
        spawnedCar.transform.SetParent(center.transform);

        carName.text = car.name;
        description.text = car.description;
        length.text = "Length: " + car.length;
        width.text = "Width: " + car.width;
        height.text = "Height: " + car.height;
        price.text = "Price: Rp. " + car.price;

        canvas.SetActive(true);
        currPanel = 0;
        panelAmount = panels.Length;
        panels[currPanel].SetActive(true);
    }

    public void Prev(){
        nextButton.SetActive(true);
        panels[currPanel].SetActive(false);
        currPanel--;
        panels[currPanel].SetActive(true);
        if(currPanel == 0){
            prevButton.SetActive(false);
        }
    }

    public void Next(){
        prevButton.SetActive(true);
        panels[currPanel].SetActive(false);
        currPanel++;
        panels[currPanel].SetActive(true);
        if(currPanel == panelAmount-1){
            nextButton.SetActive(false);
        }
    }

    public void DestroyCar(){
        transform.rotation = startingRotation;
        Destroy(spawnedCar);
        canvas.SetActive(false);
    }
}
