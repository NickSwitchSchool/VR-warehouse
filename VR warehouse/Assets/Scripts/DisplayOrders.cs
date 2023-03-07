using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayOrders : MonoBehaviour
{
    List<Product> newOrderList;
    List<Product> activeOrderList;
    [SerializeField] GameObject newRow;
    [SerializeField] Vector3 firstRowPos;
    Vector3 newRowPos;
    [SerializeField] float distanceBetweenRows;

    private void Start()
    {
        SetList(null);
    }

    private void Update()
    {
        if (newOrderList != null)
        {
            newRowPos = firstRowPos;
            for (int i = 0; i < newOrderList.Count; i++)
            {
                GameObject n_row = Instantiate(newRow, newRowPos, Quaternion.identity);
                newRowPos.y -= distanceBetweenRows;
                n_row.GetComponent<DisplayRow>().CompleteRowInstatiation(newOrderList[i].productAmount, newOrderList[i].productObject.name, null, firstRowPos, newOrderList);
            }
            activeOrderList = newOrderList;
            newOrderList = null;
        }    
    }

    public void SetList(List<Product> n_orderList)
    {
        newOrderList = n_orderList;
    }
}
