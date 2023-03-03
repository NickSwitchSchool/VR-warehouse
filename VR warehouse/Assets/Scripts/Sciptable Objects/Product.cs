using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Product
{
    public int productAmount;
    public GameObject productObject;
    public float trendWeight;

    public Product(int productAmount, GameObject productObject, float weight)
    {
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
