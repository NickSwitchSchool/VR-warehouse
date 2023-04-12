using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class TrendGenerator : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public List<Product> products = new List<Product>();
    public int maxValue;
    public float TotalValue;
    public bool changeTrend;

    private List<Product> skipList;

    public int minChange;
    public int maxChange;
    private int rndProduct;

    public void Start()
    {
        products = inventoryManager.productList;
        for (int x = 0; x < products.Count; x++)
        {
            products[x].trendWeight = maxValue / products.Count;
        }
        skipList = new List<Product>(products);
        CheckTotal(skipList);
    }
    public void Update()
    {
        if (changeTrend == true)
        {
            ChangeTrend(Random.Range(minChange, maxChange));
            changeTrend = false;
        }
    }
    public void ChangeTrend(int value)
    {
        Debug.Log("Changing Trends");

        //picking a random product out of the list
        rndProduct = Random.Range(0, products.Count);
        int index = rndProduct;

        products[index].trendWeight = products[index].trendWeight + value;
        

        //removing the changed product out of the list
        skipList.Remove(products[index]);

        CalcBalance(value, skipList);
    }

    public void CalcBalance(int _value, List<Product> _skipList)
    {
        Debug.Log("Calculating....");
        float remainder = (maxValue - products[rndProduct].trendWeight) % _skipList.Count;



        for (int i = 0; i < _skipList.Count; i++)
        {

            _skipList[i].trendWeight = ((maxValue - products[rndProduct].trendWeight) - remainder) / _skipList.Count;
        }
        if (remainder % _skipList.Count == 0)
        {
            for (int j = 0; j < _skipList.Count; j++)
            {
                _skipList[j].trendWeight += remainder / _skipList.Count;
                remainder -= remainder / _skipList.Count;
            }

        }
        else
        {
            int newRemainder = (int)Mathf.Round(remainder % _skipList.Count);
            remainder -= newRemainder;
            for (int j = 0; j < _skipList.Count; j++)
            {
                _skipList[j].trendWeight += remainder / _skipList.Count;
                remainder -= remainder / _skipList.Count;
            }
        }
        CheckTotal(_skipList);
    }

    public void CheckTotal(List<Product> _skipList)
    {
        float currentValue = 0;
        for (int x = 0; x < products.Count; x++)
        {
            currentValue += products[x].trendWeight;
        }
        TotalValue = currentValue;

        if(TotalValue < maxValue)
        {
            float missingValue = maxValue - TotalValue;
            int rndNumber = Random.Range(0,_skipList.Count - 1);
            _skipList[rndNumber].trendWeight += missingValue;
            TotalValue += missingValue;
        }
        inventoryManager.ChangeTrendList();
    }
}
