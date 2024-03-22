using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Car", menuName = "Car")]
public class Car : ScriptableObject
{
    public GameObject prefab;
    public new string name;
    public string description;
    public string length;
    public string width;
    public string height;
    public string price;
}
