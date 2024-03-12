using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CarStand : MonoBehaviour
{
    public TMP_Text carName;
    public TMP_Text dimensions;
    public TMP_Text price;
    public Car car;
    public GameObject center;
    public GameObject[] panels;
    public GameObject prevButton;
    public GameObject nextButton;

    private int panelAmount;
    private int currPanel = 0;
    // Start is called before the first frame update
    void Start()
    {
        GameObject spawnedCar = Instantiate(car.prefab, center.transform.position, Quaternion.identity);
        spawnedCar.transform.SetParent(center.transform);

        carName.text = car.name;
        price.text = car.price.ToString();

        panelAmount = panels.Length;
        panels[currPanel].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
