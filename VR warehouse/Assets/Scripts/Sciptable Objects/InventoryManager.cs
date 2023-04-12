using JetBrains.Annotations;
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
    [SerializeField]
    public List<Product> productList;
    public List<Product> productImportList;
    public List<Product> inventory;

    [Header("Trends")]
    public TrendGenerator trendGenerator;
    public float trendTimer;
    public int trendCooldown;
    public List<Product> trendList;
    public void Start()
    {
        trendTimer = trendCooldown;
    }
    public void Update()
    {
        trendTimer -= Time.deltaTime;
        if(trendTimer <= 0)
        {
            trendGenerator.ChangeTrend(Random.Range(trendGenerator.minChange, trendGenerator.maxChange));
            trendTimer = trendCooldown;
        }
    }
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
            if(inventory.ElementAt(inventory.IndexOf(oldProduct)).productAmount <= 0)
            {
                inventory.Remove(oldProduct);
            }
        }
    }

    public void ChangeTrendList()
    {
        trendList.Clear();
        for(int a = 0; a < productList.Count; a++)
        {
            for(int b = 0; b < productList[a].trendWeight; b++)
            {
                trendList.Add(productList[a]);
            }
        }
    }
}
