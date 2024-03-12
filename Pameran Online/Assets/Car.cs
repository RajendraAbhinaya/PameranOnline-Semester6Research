using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Car", menuName = "Car")]
public class Car : ScriptableObject
{
    public GameObject prefab;
    public new string name;
    public int length;
    public int width;
    public int height;
    public int price;
}
