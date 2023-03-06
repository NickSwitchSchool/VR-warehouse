using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Product
{
    public string productName;
    public int productAmount;
    public GameObject productObject;
    public float trendWeight;


    public Product(string productName, int productAmount, GameObject productObject, float weight)
    {
        this.productName = productName;
        this.productAmount = productAmount;
        this.productObject = productObject;
        this.trendWeight = weight;
    }



    //public void Start()
    //{
    //    productAmount = 1;
    //    productObject = this.gameObject;
    //}
}
