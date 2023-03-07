using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayOrders : MonoBehaviour
{
    public List<Product> newOrderList;
    public List<Product> activeOrderList;
    [SerializeField] GameObject newRow;
    [SerializeField] Vector3 firstRowPosOffset;
    Vector3 newRowPosOffset;
    [SerializeField] float distanceBetweenRows;

    private void Start()
    {
        SetList(null);
    }

    private void Update()
    {
        if (newOrderList != null)
        {
            newRowPosOffset = firstRowPosOffset;
            GameObject[] n_allRows = GameObject.FindGameObjectsWithTag(newRow.tag);
            foreach (GameObject n_row in n_allRows)
            {
                Destroy(n_row);
            }
            for (int i = 0; i < newOrderList.Count; i++)
            {
                GameObject n_row = Instantiate(newRow, transform.position + newRowPosOffset, Quaternion.identity);
                newRowPosOffset.y -= distanceBetweenRows;
                n_row.GetComponent<DisplayRow>().CompleteRowInstatiation(newOrderList[i].productAmount, newOrderList[i].productName, newOrderList[i].productObject, firstRowPosOffset, newOrderList);
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
