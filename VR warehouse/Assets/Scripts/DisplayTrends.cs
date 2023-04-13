using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayTrends : MonoBehaviour
{
    public List<Product> newTrendList;
    public List<Product> activeTrendList;
    [SerializeField] GameObject newRow;
    [SerializeField] Vector3 firstRowPosOffset;
    Vector3 newRowPosOffset;
    [SerializeField] float distanceBetweenRows;
    // Start is called before the first frame update
    void Start()
    {
        SetTrendList(null);
    }

    // Update is called once per frame
    void Update()
    {
        if (newTrendList != null)
        {
            newRowPosOffset = firstRowPosOffset;
            GameObject[] n_allRows = GameObject.FindGameObjectsWithTag(newRow.tag);
            foreach (GameObject n_row in n_allRows)
            {
                Destroy(n_row);
            }
            for (int i = 0; i < newTrendList.Count; i++)
            {
                GameObject n_row = Instantiate(newRow, transform.position + newRowPosOffset, Quaternion.identity);
                newRowPosOffset.y -= distanceBetweenRows;
                n_row.GetComponent<DisplayRow>().CompleteRowInstatiation(newTrendList[i].productAmount, newTrendList[i].productName, newTrendList[i].productObject, firstRowPosOffset, newTrendList);
            }
            activeTrendList = newTrendList;
            newTrendList = null;
        }
    }
    public void SetTrendList(List<Product> n_trendList)
    {
        newTrendList = n_trendList;
    }
}
