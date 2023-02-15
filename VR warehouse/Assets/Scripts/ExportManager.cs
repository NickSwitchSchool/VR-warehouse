using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class ExportManager : MonoBehaviour
{ 
    public InventoryManager inventoryManager;
    public List<Product> exportList;

    [Header("Truck")]
    public GameObject currentTruck;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void AddOrder(List<Product> truckList)
    {
        //check with InventoryManager if product is present and add it to order list
        GiveOrders();
    }
    public void GiveOrders()
    {
        //Call ExportRobot and give it the order list
        //Add order list to InventoryManagers ExportList
    }
    public void RemoveProduct(Product productToExport)
    {
        inventoryManager.RemoveProduct(productToExport);
    }
}
