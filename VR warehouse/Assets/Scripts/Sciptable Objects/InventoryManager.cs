using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [Header("Robots")]
    public List<GameObject> robots;

    [Header("Import and Export boxes")]
    public GameObject exportBox;
    public GameObject importBox;

    [Header("Products")]
    public List<Product> importList;
    public List<Product> exportList;
    public List<Product> inventory;
    
    public void AddProduct(Product newProduct)
    {
        if(inventory.Contains(newProduct))
        {
            inventory.ElementAt(inventory.IndexOf(newProduct)).productAmount++;
        }
        else
        {
            inventory.Add(newProduct);
            inventory.ElementAt(inventory.IndexOf(newProduct)).productAmount++;
        }
    }
    public void RemoveProduct(Product oldProduct)
    {
        if(inventory.Contains(oldProduct))
        {
            inventory.ElementAt(inventory.IndexOf(oldProduct)).productAmount--;
        }
    }
}