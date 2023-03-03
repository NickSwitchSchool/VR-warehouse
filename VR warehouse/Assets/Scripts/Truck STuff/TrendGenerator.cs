using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TrendGenerator : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public List<Product> products = new List<Product>();
    public float MaxValue;
    public float TotalValue;
    public bool changeTrend;

    public int minChange;
    public int maxChange;


    public void Start()
    {
        products = inventoryManager.productList;
        for (int x = 0; x < products.Count; x++)
        {
            products[x].trendWeight = MaxValue / products.Count;
        }
    }
    public void Update()
    {
        if (changeTrend == true)
        {
            ChangeTrend(Random.Range(minChange,maxChange));
        }
    }
    public void ChangeTrend(float value)
    {
        Debug.Log("Changing Trends");
        if (value < 99)
        {
            int rndProduct = Random.Range(0, products.Count);
            int index = rndProduct;

            if (products[index].trendWeight + value < MaxValue)
            {
                products[index].trendWeight = products[index].trendWeight + value;
                List<Product> ChangeList = new List<Product>(products);

                ChangeList.Remove(products[index]);

                int count = 0;
                float newValue = 0;

                for (int y = 0; y < ChangeList.Count; y++)
                {
                    if ((ChangeList[y].trendWeight - value) < 0)
                    {
                        newValue = value - ChangeList[y].trendWeight;
                        ChangeList[y].trendWeight = 0;
                        count++;
                    }
                    else
                    {
                        ChangeList[y].trendWeight -= (value + newValue) / (ChangeList.Count - count);
                    }

                }

                CheckTrend(ChangeList);
               

                print("done");
                changeTrend = false;
            }
        }

    }

    private void CheckTrend(List<Product> ChangeList)
    {
        TotalValue =0;

        
        if(TotalValue > MaxValue)
        {
            int z = 0;
           while(TotalValue > MaxValue)
           {
                if ((ChangeList[z].trendWeight - 1) > 0)
                {
                    ChangeList[z].trendWeight--;
                }
                z++;
                if (z >= ChangeList.Count)
                {
                    z = 0;
                }
                CheckTotal(ChangeList);
            }
        }
        else if(TotalValue < MaxValue)
        {
            int z = 0;
            while(TotalValue < MaxValue)
            {
                if ((ChangeList[z].trendWeight + 1) < 100)
                {
                    ChangeList[z].trendWeight++;
                }
                z++;
                if (z >= ChangeList.Count)
                {
                    z = 0;
                }
                CheckTotal(ChangeList);
            }
        }
    }

    public void CheckTotal(List<Product> ChangeList)
    {
        float currentValue = 0;
        for (int x = 0; x < products.Count; x++)
        {
            currentValue += products[x].trendWeight;
        }
        TotalValue = currentValue;
    }
}
