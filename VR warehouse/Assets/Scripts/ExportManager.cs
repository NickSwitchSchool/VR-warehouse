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
        for(int i = 0; i < truckList.Count; i++)
        {
            Product virtualProduct = truckList[i];
            Product newProduct = new Product(virtualProduct.productAmount, virtualProduct.productObject);
            //check with InventoryManager if product is present and add it to order list
            for (int x = 0; x < inventoryManager.inventory.Count; x++)
            {
                if (inventoryManager.inventory[x].productObject == newProduct.productObject)
                {
                    bool addProduct = true;
                    int productInd = 0;
                    for (int a = 0; a < exportList.Count; a++)
                    {
                        if (exportList[a].productObject == newProduct.productObject)
                        {
                            exportList[a].productAmount++;
                            addProduct = false;
                            break;
                        }
                        else
                        {
                            productInd++;
                        }
                    }
                    if (addProduct)
                    {
                        exportList.Add(newProduct);
                        exportList[productInd].productAmount++;
                    }
                }
            }
        }
        GiveOrders();
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
        if (!robotFound)
        {
            GiveOrders();
        }
        
    }
    public void RemoveProduct(Product productToExport)
    {
        inventoryManager.RemoveProduct(productToExport);
    }
}
