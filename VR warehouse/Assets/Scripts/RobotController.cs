using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class RobotController : MonoBehaviour
{
    public NavMeshAgent agent; 
    public bool gettingProduct;
    public GameObject holdingProduct;

    private GameObject currentProduct;
    public List<GameObject> orderList;
    private int currentOrder;

    public GameObject exportBox;
    
    public void NewOrders(List<GameObject> _newOrders)
    {
        orderList.Clear();
        this.orderList = _newOrders;
        currentOrder = 0;
        GetProduct();
    }
    public void GetProduct()
    {
        Transform product = orderList.ElementAt(currentOrder).transform;
        currentProduct = product.gameObject;
        
        agent.SetDestination(product.position);
        gettingProduct = true;
        currentOrder++;
    }

    private void OnCollisionEnter(Collision collision)
    {
        print("Collision Detected!");
        if (collision.gameObject == currentProduct)
        {
            print("Product Found!");
            holdingProduct = collision.gameObject;

            holdingProduct.transform.parent = this.transform.GetChild(0);
            holdingProduct.transform.position = this.transform.GetChild(0).position;


            agent.SetDestination(exportBox.transform.position);
        }
        if (collision.gameObject == exportBox)
        { 
            collision.gameObject.GetComponent<ExportManager>().ExportProduct(holdingProduct);
            Destroy(holdingProduct);
            gettingProduct = false;
            
        }
    }


}
