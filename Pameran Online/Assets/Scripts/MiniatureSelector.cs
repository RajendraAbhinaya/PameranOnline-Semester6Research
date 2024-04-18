using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MiniatureSelector : MonoBehaviour
{
    public TMP_Dropdown brandDropdown;
    public TMP_Dropdown carDropdown;
    public CarSearch carSearchScript;
    private GameObject miniature;
    private string carBrand;
    private string carName;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider col){
        if(col.gameObject.tag == "Miniature"){
            Destroy(miniature);
            miniature = col.gameObject;
            carBrand = miniature.GetComponent<CarMiniature>().car.brand;
            carName = miniature.GetComponent<CarMiniature>().car.name;

            for(int i=0; i<brandDropdown.options.Count; i++){
                if(carBrand == brandDropdown.options[i].text){
                    brandDropdown.value = i;
                    carSearchScript.SelectBrand();
                    break;
                }
            }

            for(int i=0; i<carDropdown.options.Count; i++){
                if(carName == carDropdown.options[i].text){
                    carDropdown.value = i;
                    break;
                }
            }
        }
    }

    void OnTriggerExit(Collider col){
        if(col.gameObject.tag == "Miniature"){
            miniature = null;
            col.gameObject.GetComponent<CarMiniature>().SetDespawnTimer();
        }
    }
}
