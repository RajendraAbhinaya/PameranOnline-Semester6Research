using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CarSelector : MonoBehaviour
{
    [System.Serializable]
    public struct Country{
        public string countryName;
        public List<Car> carList;
    }

    public TMP_Dropdown dropdown;
    public List<Country> countryList = new List<Country>();
    public List<CarStand> carStands = new List<CarStand>();
    public GameObject prevButton;
    public GameObject nextButton;
    public TMP_Text countryText;
    public TMP_Text hallText;

    private int carStandsLength;
    private Country currCountry;
    private int currCountryCarListLength;
    private int currHall = 0;
    private int hallCount;

    // Start is called before the first frame update
    void Start()
    {
        //Initialise starting values. currCountry set to first country in countryList
        carStandsLength = carStands.Count;
        currCountry = countryList[0];
        currCountryCarListLength = currCountry.carList.Count;
        hallCount = Mathf.CeilToInt((float)currCountry.carList.Count / (float)carStandsLength);
        countryText.text = currCountry.countryName;

        //Set prev button off as it is the first page. Set next button off if there is only one page
        prevButton.SetActive(false);
        if(hallCount <= 1){
            nextButton.SetActive(false);
        }
        else{
            nextButton.SetActive(true);
        }

        SetStands();
    }

    //Select Country using the dropdown
    public void SelectCountry(){
        //Set variable values according to the country chosen from the dropdown
        currCountry = countryList[dropdown.value];
        currCountryCarListLength = currCountry.carList.Count;
        currHall = 0;
        countryText.text = currCountry.countryName;
        hallText.text = "Hall " + (currHall+1);
        hallCount = Mathf.CeilToInt((float)currCountry.carList.Count / (float)carStandsLength);

        //Set prev button off as it is the first page. Set next button off if there is only one page
        prevButton.SetActive(false);
        if(hallCount <= 1){
            nextButton.SetActive(false);
        }
        else{
            nextButton.SetActive(true);
        }
        SetStands();
    }

    //Sets the stands in the scene with the scriptable objects in the selected country's carList
    public void SetStands(){
        //Sets the offset for car selection based on which page is chosen
        int offset = carStandsLength*currHall;
        for(int i = 0; i < carStandsLength; i++){
            //Erases all existing cars from the stands and then replaces them if there is a car available in the list
            carStands[i].DestroyCar();
            if(currCountryCarListLength > i + offset){
                carStands[i].SetCar(currCountry.carList[i+offset]);
            }
        }
    }

    //Used to go back to a previous page
    public void Prev(){
        nextButton.SetActive(true);
        currHall--;
        hallText.text = "Hall " + (currHall+1);
        SetStands();
        if(currHall == 0){
            prevButton.SetActive(false);
        }
    }

    //Used to go to the next page
    public void Next(){
        prevButton.SetActive(true);
        currHall++;
        hallText.text = "Hall " + (currHall+1);
        SetStands();
        if(currHall == hallCount-1){
            nextButton.SetActive(false);
        }
    }
}
