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
        [HideInInspector]
        public int pages;

        public void SetPages(int pageCount){
            pages = pageCount;
        }
    }

    public TMP_Dropdown dropdown;
    public List<Brand> brandList = new List<Brand>();
    public List<CarStand> carStands = new List<CarStand>();
    public GameObject prevButton;
    public GameObject nextButton;

    private int carStandsLength;
    private Brand currBrand;
    private int currBrandCarListLength;
    private int currPage = 0;
    // Start is called before the first frame update
    void Start()
    {
        carStandsLength = carStands.Count;

        for(int i = 0; i < brandList.Count; i++){
            brandList[i].SetPages((int)Mathf.Ceil((float)brandList[i].carList.Count / (float)carStandsLength));
        }

        currBrand = brandList[0];
        currBrandCarListLength = currBrand.carList.Count;

        prevButton.SetActive(false);
        if(currBrand.pages == 0){
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
        prevButton.SetActive(false);
        if(currBrand.pages == 0){
            nextButton.SetActive(false);
        }
        else{
            nextButton.SetActive(true);
        }
    }

    public void SetStands(){
        int offset = carStandsLength*currPage;
        for(int i = 0; i < carStandsLength; i++){
            if(currBrand.carList.Count > i + offset){
                carStands[i].SetCar(currBrand.carList[i+offset]);
            }
        }
    }

    public void Prev(){
        nextButton.SetActive(true);
        currPage--;
        if(currPage == 0){
            prevButton.SetActive(false);
        }
    }

    public void Next(){
        prevButton.SetActive(true);
        currPage++;
        if(currPage == currBrand.pages-1){
            nextButton.SetActive(false);
        }
    }
}
