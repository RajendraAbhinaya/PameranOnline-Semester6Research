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
    public GameObject center;
    public GameObject socket;
    public List<Brand> brandList = new List<Brand>();

    private List<string> brandNames = new List<string>();
    private List<Car> currCarList = new List<Car>();
    private Brand currBrand;
    //private MiniatureSelector miniatureSelectorScript;

    // Start is called before the first frame update
    void Start()
    {
        //miniatureSelectorScript = socket.GetComponent<MiniatureSelector>();
        for(int i=0; i<brandList.Count; i++){
            brandNames.Add(brandList[i].brandName);
        }
        brandNames.Sort();

        brandDropdown.ClearOptions();
        brandDropdown.AddOptions(brandNames);

        string currBrandName = brandNames[0];
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

/*
    public void SelectCar(){
        //miniatureSelectorScript.DestroyMiniature();
        //spawnMiniature = Time.time + 0.5f;
        Instantiate(currCarList[carDropdown.value].miniature, socket.transform.position, new Quaternion(0, 180, 0, 0));
        Debug.Log("AAAAAAA");
    }

    public void CheckCar(){
        if(carDropdown.value != prevCar){
            SelectCar();
            prevCar = carDropdown.value;
        }
    }
    */

    public void SelectButton(){
        carStand.DestroyCar();
        carStand.SetCar(currCarList[carDropdown.value]);
        //SelectCar();
        center.transform.Rotate(0, 180, 0);
    }
}
