using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class RobotController : MonoBehaviour
{
    public NavMeshAgent agent; 
    public bool gettingProduct;
    public Product holdingProduct;
    public InventoryManager inventoryManager;

    private GameObject currentProduct;
    public List<Product> orderList;
    [SerializeField]
    private int currentOrder;
    public Transform target;
    
    public void NewOrders(List<Product> _newOrders)
    {
        this.orderList = _newOrders;
        currentOrder = 0;
        GetProduct();
    }
    public void GetProduct()
    {
        currentOrder = 0;
        if(currentOrder >= this.orderList.Count)
        {
            gettingProduct = false;
            return;
        }
        else if(orderList.ElementAt(0) == null)
        {
            while(orderList.ElementAt(currentOrder) == null)
            {
                orderList.RemoveAt(currentOrder);
            }
        }
        target = orderList.ElementAt(currentOrder).productObject.transform;
        currentProduct = target.gameObject;
        
        agent.SetDestination(target.position);
        gettingProduct = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        print("Collision Detected!");
        if (collision.gameObject == currentProduct && holdingProduct == null)
        {
            print("Product Found!");
            holdingProduct = collision.gameObject.GetComponent<Product>();

            holdingProduct.productObject.transform.parent = this.transform.GetChild(0);
            holdingProduct.productObject.transform.position = this.transform.GetChild(0).position;
            holdingProduct.productObject.transform.rotation = this.transform.GetChild(0).rotation;


            agent.SetDestination(inventoryManager.exportBox.transform.position);
        }
        if (collision.gameObject == inventoryManager.exportBox)
        {
            collision.gameObject.GetComponent<ExportManager>().RemoveProduct(holdingProduct);
            GetProduct();
        }
    }


}
