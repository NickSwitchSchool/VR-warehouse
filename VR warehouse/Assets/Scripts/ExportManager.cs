using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class ExportManager : MonoBehaviour
{
    public KeyCode spawnTruck;
    public GameObject truckSpawn;
    public GameObject truck;
    public InventoryManager inventoryManager;
    public List<Product> exportList;

    [Header("Truck")]
    public GameObject currentTruck;
    public void AddOrder(List<Product> truckList)
    {
        GameObject n_displayScreen = GameObject.FindGameObjectWithTag("DisplayScreen");
        //replace n_debugList with the list of export products
        //if statement is to make sure the game doesn't crash if there is no displayscreen in the scene
        if (n_displayScreen != null)
        {
            n_displayScreen.GetComponent<DisplayOrders>().SetList(truckList, this);
        }
        exportList = truckList;
    }
    public void GiveOrders()
    {
        //Call an ExportRobot and give it the order list
        
        bool robotFound = false;
        for(int y= 0; y < inventoryManager.robots.Count; y++)
        {
            if(inventoryManager.robots[y].GetComponent<RobotController>().gettingProduct == false)
            {
                inventoryManager.robots[y].GetComponent<RobotController>().NewOrders(exportList);
                robotFound = true;
                break;
            }
           
        }
        
    }
    public void RemoveProduct(Product productToExport)
    {
        inventoryManager.RemoveProduct(productToExport);
    }
}
