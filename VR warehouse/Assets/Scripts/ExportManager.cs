using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class ExportManager : MonoBehaviour
{
    public List<GameObject> robots;
    public List<GameObject> products;

    public List<GameObject> allProducts;
    public GameObject currentTruck;
    // Start is called before the first frame update
    void Start()
    {
        robots = GameObject.FindGameObjectsWithTag("Robots").ToList();
        var _products = GameObject.FindGameObjectsWithTag("Products");
        products = _products.ToList();
        
    }

    // Update is called once per frame
    public void AddOrder(List<ProductData> truckList)
    {
        for(int i  = 0; i < truckList.Count; i++)
        {
            for(int am = 0; am < truckList.ElementAt(i).productAmount; am++)
            {
                allProducts.Add(truckList.ElementAt(i).productObject);
            }
            
        }
    }
    public void GiveOrders()
    {
        for(int a = 0; a < robots.Count; a++)
        {
            if(robots.ElementAt(a).GetComponent<RobotController>().gettingProduct)
            {
                robots.ElementAt(a).GetComponent<RobotController>().NewOrders(allProducts);
            }
        }
    }

    public void ExportProduct(GameObject proToExport)
    {
        products.Remove(proToExport);
        Destroy(proToExport);
    }
}
