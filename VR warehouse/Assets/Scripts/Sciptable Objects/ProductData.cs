using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Product")]
public class ProductData : ScriptableObject
{
    public string productName;
    public GameObject productObject;
    public int productAmount;
}
