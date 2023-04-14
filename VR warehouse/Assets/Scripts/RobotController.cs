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
    public bool walking;
    public float distanceWalked;
    private GameObject currentProduct;
    public List<Product> orderList;
    [SerializeField]
    private int currentOrder;
    public Transform target;

    public List<Pallet> productsInScene;
    
    public void NewOrders(List<Product> _newOrders)
    {
        this.orderList = _newOrders;
        currentOrder = 0;
        distanceWalked= 0;
        GetProduct();
    }

    public void Update()
    {
        if(walking)
        {
            distanceWalked += Mathf.Round(Time.deltaTime);
        }
    }
    public void GetProduct()
    {
        currentOrder = 0;
        if(currentOrder >= orderList.Count)
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
        for(int i = 0; i < productsInScene.Count; i++) 
        {
            if (productsInScene[i].name == orderList.ElementAt(currentOrder).productName)
            {
                agent.SetDestination(productsInScene[i].robotGoalPosition);
            }
        }
        
        
        
        if(target != null)
        {
            gettingProduct = true;
        }
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
            this.GetComponent<Animator>().SetBool("WalkB", true);

            agent.SetDestination(inventoryManager.exportBox.transform.position);
        }
        if (collision.gameObject == inventoryManager.exportBox)
        {
            this.GetComponent<Animator>().SetBool("WalkB", false);
            this.GetComponent<Animator>().SetTrigger("DropBox");
            collision.gameObject.GetComponent<ExportManager>().RemoveProduct(holdingProduct);
            GetProduct();
        }
    }


}
