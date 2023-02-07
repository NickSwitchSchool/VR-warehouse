using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ExportManager : MonoBehaviour
{
    public GameObject[] robots;
    public List<GameObject> products;
    // Start is called before the first frame update
    void Start()
    {
        robots = GameObject.FindGameObjectsWithTag("Robots");
        var _products = GameObject.FindGameObjectsWithTag("Products");
        products = _products.ToList();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GiveOrders()
    {
        for (int i = 0; i < products.Count / 6; i++)
        {

        }
    }
    //public void GetRandomProduct()
    //{
    //    int assingedRobot = 0;
    //    for(int i = 0; i < robots.Length; i++)
    //    {
    //        if (robots[assingedRobot].GetComponent<RobotController>().gettingProduct == false)
    //        {
    //            if(products.Count == 0)
    //            {
    //                print("all products are exported");
    //                break;
    //            }
    //            robots[assingedRobot].GetComponent<RobotController>().GetProduct();
    //        }
    //        else
    //        {
    //            if (assingedRobot + 1 != robots.Length)
    //            {
    //                assingedRobot++;
    //            }
    //            else
    //            {
    //                assingedRobot = 0;
    //            }

    //        }
    //    }

    //}

    public void ExportProduct(GameObject proToExport)
    {
        products.Remove(proToExport);
    }
}
