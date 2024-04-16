using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CarSearch : MonoBehaviour
{
    [System.Serializable]
    public struct Brand{
        public string brandName;
        public List<Car> carList;
    }

    public TMP_Dropdown brandDropdown;
    public TMP_Dropdown carDropdown;
    public CarStand carStand;
    public List<Brand> brandList = new List<Brand>();

    private List<string> brandNames = new List<string>();
    private List<Car> currCarList = new List<Car>();
    private Brand currBrand;

    // Start is called before the first frame update
    void Start()
    {
        for(int i=0; i<brandList.Count; i++){
            brandNames.Add(brandList[i].brandName);
        }
        brandNames.Sort();

        brandDropdown.ClearOptions();
        brandDropdown.AddOptions(brandNames);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectBrand(){
        string currBrandName = brandNames[brandDropdown.value];
        for(int i=0; i<brandList.Count; i++){
            if(currBrandName == brandList[i].brandName){
                currBrand = brandList[i];
                break;
            }
        }

        currCarList = currBrand.carList;
        List<string> carNames = new List<string>();
        for(int i=0; i<currCarList.Count; i++){
            carNames.Add(currCarList[i].name);
        }
        carDropdown.ClearOptions();
        carDropdown.AddOptions(carNames);
    }

    public void SelectCar(){
        
    }
}
