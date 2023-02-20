using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Product
{
    public int productAmount;
    public GameObject productObject;


    public Product(int productAmount, GameObject productObject)
    {
        this.productAmount = productAmount;
        this.productObject = productObject;
    }



    //public void Start()
    //{
    //    productAmount = 1;
    //    productObject = this.gameObject;
    //}
}
