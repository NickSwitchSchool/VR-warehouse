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
        //check if value is not to high
        Debug.Log("Changing Trends");
        if (value < MaxValue - maxChange)
        {
            //picking a random product out of the list
            int rndProduct = Random.Range(0, products.Count);
            int index = rndProduct;

            if (products[index].trendWeight + value < MaxValue)
            {
                products[index].trendWeight = products[index].trendWeight + value;
                List<Product> changeList = new List<Product>(products);
                List<Product> tempList = new List<Product>();
                
                //removing the changed product out of the list
                changeList.Remove(products[index]);

                int count = 0;
                float newValue = 0;

                //if the value of other products become 0 
                for (int y = 0; y < changeList.Count; y++)
                {
                    if ((changeList[y].trendWeight - value) < 10)
                    {
                        newValue = value - changeList[y].trendWeight;
                        changeList[y].trendWeight = 10;
                        tempList.Add(changeList[y]);
                        changeList.RemoveAt(y);
                        count++;
                    }
                    else
                    {
                        changeList[y].trendWeight -= Mathf.Round((value + newValue) / changeList.Count);
                        newValue = 0;
                    }

                }

                CheckTrend(changeList);
                CheckTotal(changeList);

                print("done");
                changeTrend = false;
            }
        }

    }

    private void CheckTrend(List<Product> ChangeList)
    {
        if(TotalValue > MaxValue)
        {
            int z = 0;
           while(TotalValue > MaxValue)
           {
                if ((ChangeList[z].trendWeight - 1) > 10)
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
                if ((ChangeList[z].trendWeight + 1) < MaxValue)
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

        if (TotalValue != MaxValue)
        {
            CheckTrend(ChangeList);
        }
    }
}
