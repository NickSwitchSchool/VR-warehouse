using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class TrendGenerator : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public List<Product> products = new List<Product>();
    public int maxValue;
    public float TotalValue;
    public bool changeTrend;
    public int minValue;

    public int minChange;
    public int maxChange;


    public void Start()
    {
        products = inventoryManager.productList;
        for (int x = 0; x < products.Count; x++)
        {
            products[x].trendWeight = maxValue / products.Count;
        }
    }
    public void Update()
    {
        if (changeTrend == true)
        {
            ChangeTrend(Random.Range(minChange, maxChange));
        }
    }
    public void ChangeTrend(int value)
    {
        //check if value is not to high
        Debug.Log("Changing Trends");
        if (value < maxValue - maxChange)
        {
            //picking a random product out of the list
            int rndProduct = Random.Range(0, products.Count);
            int index = rndProduct;

            products[index].trendWeight = products[index].trendWeight + value;
            List<Product> skipList = new List<Product>(products);

            //removing the changed product out of the list
            skipList.Remove(products[index]);

            CalcBalance(value, skipList);

            changeTrend = false;
        }
    }

    public void CalcBalance(int _value, List<Product> _skipList)
    {
        //dividing the value by the amount of products after removing the remainder
        int remainder = _value % _skipList.Count;
        _value = _value - remainder;

        for (int a = 0; a < _skipList.Count; a++)
        {
            if (_skipList[a].trendWeight - (_value / _skipList.Count) > minValue)
            {
                _skipList[a].trendWeight -= _value / _skipList.Count;
            }
            else
            {
                _skipList[a].trendWeight -= _value / _skipList.Count;
                _skipList[a].trendWeight = minValue;
            }
        }
        
        
        CheckTotal();
        if(remainder > 0)
        {
            CalcBalance(remainder, _skipList);
        }
    }

    public void CheckTotal()
    {
        float currentValue = 0;
        for (int x = 0; x < products.Count; x++)
        {
            currentValue += products[x].trendWeight;
        }
        TotalValue = currentValue;
    }
}
