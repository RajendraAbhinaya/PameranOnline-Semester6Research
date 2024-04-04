using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CarSelector : MonoBehaviour
{
    [System.Serializable]
    public struct Brand{
        public string brandName;
        public List<Car> carList;
    }

    public TMP_Dropdown dropdown;
    public List<Brand> brandList = new List<Brand>();
    public List<CarStand> carStands = new List<CarStand>();
    public GameObject prevButton;
    public GameObject nextButton;
    public TMP_Text brandText;
    public TMP_Text hallText;

    private int carStandsLength;
    private Brand currBrand;
    private int currBrandCarListLength;
    private int currPage = 0;
    private int pageCount;
    // Start is called before the first frame update
    void Start()
    {
        carStandsLength = carStands.Count;

        currBrand = brandList[0];
        currBrandCarListLength = currBrand.carList.Count;
        pageCount = Mathf.CeilToInt((float)currBrand.carList.Count / (float)carStandsLength);
        brandText.text = currBrand.brandName;

        prevButton.SetActive(false);
        if(pageCount <= 1){
            nextButton.SetActive(false);
        }
        else{
            nextButton.SetActive(true);
        }

        SetStands();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectBrand(){
        Debug.Log(dropdown.value);
        currBrand = brandList[dropdown.value];
        currBrandCarListLength = currBrand.carList.Count;
        currPage = 0;
        brandText.text = currBrand.brandName;
        hallText.text = "Hall " + (currPage+1);
        pageCount = Mathf.CeilToInt((float)currBrand.carList.Count / (float)carStandsLength);
        prevButton.SetActive(false);
        if(pageCount <= 1){
            nextButton.SetActive(false);
        }
        else{
            nextButton.SetActive(true);
        }
        SetStands();
    }

    public void SetStands(){
        int offset = carStandsLength*currPage;
        for(int i = 0; i < carStandsLength; i++){
            carStands[i].DestroyCar();
            if(currBrandCarListLength > i + offset){
                carStands[i].SetCar(currBrand.carList[i+offset]);
            }
        }
    }

    public void Prev(){
        nextButton.SetActive(true);
        currPage--;
        hallText.text = "Hall " + (currPage+1);
        SetStands();
        if(currPage == 0){
            prevButton.SetActive(false);
        }
    }

    public void Next(){
        prevButton.SetActive(true);
        currPage++;
        hallText.text = "Hall " + (currPage+1);
        SetStands();
        if(currPage == pageCount-1){
            nextButton.SetActive(false);
        }
    }
}
