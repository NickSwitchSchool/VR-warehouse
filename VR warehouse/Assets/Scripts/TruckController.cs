using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Schema;
using Unity.VisualScripting;
using UnityEngine;

public class TruckController : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public ExportManager exportManager;
    public List<Product> truckInventory;
    public enum PortType {Import, Export}
    public PortType task;

    public bool onSpot;
    private float travelTime;
    private float startTime;
    public float speed;

    public Transform startPos;
    public Transform endPos;

    public bool doorState;
    public GameObject importDoor;
    public GameObject exportDoor;
    public void Start()
    {
        exportManager = GameObject.Find("ExportBox").GetComponent<ExportManager>();
        inventoryManager = exportManager.inventoryManager;
        if(task == PortType.Export)
        {

        }
        if(task == PortType.Import)
        {
            RandomOrder();
        }
        startTime = Time.time;
        travelTime = Vector3.Distance(startPos.position, endPos.position);
    }
    public void Update()
    {
        if(!onSpot)
        {
            float distCovered = (Time.time - startTime) * speed;

            float fracComplete = distCovered / travelTime;

            gameObject.transform.position = Vector3.Lerp(startPos.position, endPos.position, fracComplete);
        }
        if(gameObject.transform.position == endPos.position)
        {
            doorState = true;
            ToggleDoors(task);
        }
    }
    public void RandomOrder()
    {
        for (int i = 0; i < 7; i++)
        {
            Product virtualProduct = inventoryManager.productList.ElementAt(Random.Range(0, inventoryManager.productList.Count));
            Product newProduct = new Product(virtualProduct.productAmount, virtualProduct.productObject);
            if (truckInventory.Count > 0)
            {
                bool addProduct = true;
                int productInd = 0;
                for (int a = 0; a < truckInventory.Count; a++)
                {
                    if (truckInventory[a].productObject == newProduct.productObject)
                    {
                        truckInventory[a].productAmount++;
                        addProduct = false;
                        break;
                    }
                    else
                    {
                        productInd++;
                    }
                }
                if(addProduct)
                {
                    truckInventory.Add(newProduct);
                    truckInventory[productInd].productAmount++;
                }
            }
            else
            {
                truckInventory.Add(newProduct);
                truckInventory[0].productAmount++;
            }
        }

    }

    public void ToggleDoors(PortType _type)
    {
        this.GetComponent<Animator>().SetBool("TruckDoor", doorState);
        if(_type == PortType.Import)
        {
            //importDoor.GetComponent<Animator>().SetBool("DoorState", doorState);
        }
        else
        {
            //exportDoor.GetComponent<Animator>().SetBool("DoorState", doorState);
        }
    }
    public void EmptyTruck()
    {

    }

    public void FillTruck()
    {
        
    }
    
}
