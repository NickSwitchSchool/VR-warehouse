using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayRow : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI amountDisplayText;
    [SerializeField] TextMeshProUGUI nameDisplayText;
    [SerializeField] Color completedColor;
    GameObject[] allRows;
    GameObject detailsPrefab;
    Vector3 screenTopLeft;
    List<Product> orderList;
    
    public void CompleteRowInstatiation(int n_amount, string n_name, GameObject n_detailsPrefab, Vector3 n_screenTopLeft, List<Product> n_orderList)
    {
        amountDisplayText.text = $"{n_amount.ToString()}x";
        nameDisplayText.text = n_name;
        //detailsPrefab = n_detailsPrefab;
        screenTopLeft = n_screenTopLeft;
        orderList = n_orderList;
    }

    public void RowCompleted()
    {
        amountDisplayText.color = completedColor;
        nameDisplayText.color = completedColor;
    }

    public void ViewInfoOnProduct()
    {
        allRows = GameObject.FindGameObjectsWithTag(this.tag);
        GameObject n_details = Instantiate(detailsPrefab, screenTopLeft, Quaternion.identity);
    }
}
