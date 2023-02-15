using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Product : MonoBehaviour
{
    public int productAmount;
    public GameObject productObject;

    public void Start()
    {
        productAmount = 1;
        productObject = this.gameObject;
    }
}
