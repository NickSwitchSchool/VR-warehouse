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
    [SerializeField] Transform[] productDebugSpawnLocations;
    public List<GameObject> spawnedDebugs;
    ExportManager exportManager;

    private void Start()
    {
        SetList(null, null);
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
            //foreach (GameObject n_thing in spawnedDebugs)
            //{
            //    Destroy(n_thing);
            //}
            spawnedDebugs.Clear();
            for (int i = 0; i < newOrderList.Count; i++)
            {
                GameObject n_row = Instantiate(newRow, transform.position + newRowPosOffset, Quaternion.identity);
                newRowPosOffset.y -= distanceBetweenRows;
                n_row.GetComponent<DisplayRow>().CompleteRowInstatiation(newOrderList[i].productAmount, newOrderList[i].productName, newOrderList[i].productObject, firstRowPosOffset, newOrderList);

                ////instantiate products for presentation
                //GameObject n_pallet = Instantiate(newOrderList[i].productObject.GetComponent<Pallet>().emptyPalletVariation, productDebugSpawnLocations[i].position, Quaternion.identity);
                //spawnedDebugs.Add(n_pallet);
                //for (int n_i = 0; n_i < newOrderList[i].productObject.GetComponent<Pallet>().GetBoxesThatNeedToBeOrdered().Count; n_i++)
                //{
                //    GameObject n_thing = Instantiate(newOrderList[i].productObject.GetComponent<Pallet>().GetBoxesThatNeedToBeOrdered()[n_i], productDebugSpawnLocations[i].position + new Vector3(0, 0, 2 + (n_i * 1.7f)), Quaternion.identity);
                //    spawnedDebugs.Add(n_thing);
                //}
            }
            activeOrderList = newOrderList;
            exportManager.GiveOrders();
            newOrderList = null;
        }    
    }

    public void SetList(List<Product> n_orderList, ExportManager n_exporManager)
    {
        newOrderList = n_orderList;
        exportManager = n_exporManager;
    }
}
