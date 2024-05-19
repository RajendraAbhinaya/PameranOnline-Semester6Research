using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Car", menuName = "Car")]
public class Car : ScriptableObject
{
    public GameObject prefab;
    public GameObject miniature;
    public new string name;
    public string brand;
    [TextArea(5,10)]
    public string description;
    [TextArea(5,10)]
    public string features;
    public string length;
    public string width;
    public string height;
    public string price;
}
