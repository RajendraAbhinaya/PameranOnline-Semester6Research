using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CarStand : MonoBehaviour
{
    public TMP_Text carName;
    public GameObject car;
    public GameObject center;
    // Start is called before the first frame update
    void Start()
    {
        GameObject spawnedCar = Instantiate(car, center.transform.position, Quaternion.identity);
        spawnedCar.transform.SetParent(center.transform);
    }

    // Update is called once per frame
    void Update()
    {
        carName.text = car.name;
    }
}
