using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RobotController : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject currentProduct;
    public KeyCode GetProductKey;
    public GameObject exportBox;
    public GameObject holdingProduct;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    

    public void SetProduct()
    {
        
    }
    public void GetProduct(GameObject _newProduct)
    {
        Transform product = _newProduct.transform;

        agent.SetDestination(product.position);
    }

    private void OnCollisionEnter(Collision collision)
    {
        print("Collision Detected!");
        if (collision.gameObject == currentProduct)
        {
            print("Product Found!");
            holdingProduct = collision.gameObject;

            holdingProduct.transform.parent = this.gameObject.transform.GetChild(0);
            holdingProduct.transform.position = gameObject.transform.GetChild(0).position;


            agent.SetDestination(exportBox.transform.position);
        }
        if (collision.gameObject == exportBox)
        {
            Destroy(holdingProduct);
        }
    }


}
