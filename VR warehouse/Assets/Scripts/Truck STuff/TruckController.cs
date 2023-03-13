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
    public List<Product> exportNeeds;
    public List<Product> truckInventory;
    public List<Product> exportedProducts;
    public enum PortType { Import, Export }
    public PortType task;
    public float timer;
    public float timeWindow;

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
        if (task == PortType.Export)
        {
            RandomOrder();
            Debug.Log("Setting Export list");
            exportManager.AddOrder(exportNeeds);
        }
        if (task == PortType.Import)
        {
            for (int a = 0; a < inventoryManager.productList.Count; a++)
            {
                Product virtualProduct = inventoryManager.productList.ElementAt(a);
                Product newProduct = new Product(virtualProduct.productName, virtualProduct.productAmount, virtualProduct.productObject, virtualProduct.trendWeight);
                truckInventory.Add(newProduct);
                truckInventory.ElementAt(truckInventory.IndexOf(newProduct)).productAmount = 3;
            }
        }
        startTime = Time.time;
        travelTime = Vector3.Distance(startPos.position, endPos.position);
        timer = timeWindow;
    }
    public void Update()
    {
        //check if truck is on the spot it needs to be
        if (!onSpot)
        {
            float distCovered = (Time.time - startTime) * speed;

            float fracComplete = distCovered / travelTime;

            gameObject.transform.position = Vector3.Lerp(startPos.position, endPos.position, fracComplete);
        }
        //if the truck has the correct position tell it its onSpot and open the doors
        //also tell the export manager what truck is the export truck
        if (gameObject.transform.position == endPos.position && !onSpot)
        {
            onSpot = true;
            doorState = true;
            ToggleDoors(task);
            if (task == PortType.Export)
            {
                exportManager.currentTruck = this.gameObject;
                
                
            }
        }
        if (onSpot)
        {
            
            timer = timer - Time.deltaTime;
            if (timer <= 0)
            {
                if (task == PortType.Import)
                {
                    if (truckInventory.Count > 0)
                    {
                        timer += 5;
                    }
                    else
                    {
                        StartCoroutine(CloseDoor());
                    }
                }
                else if (task == PortType.Export)
                {
                    exportManager.currentTruck = this.gameObject;

                    truckInventory.Clear();
                    StartCoroutine(CloseDoor());
                }
            }
        }

    }
    public void RandomOrder()
    {
        for (int i = 0; i < 7; i++)
        {
            Product virtualProduct = inventoryManager.productList.ElementAt(Random.Range(0, inventoryManager.productList.Count));
            Product newProduct = new Product(virtualProduct.productName, virtualProduct.productAmount, virtualProduct.productObject, virtualProduct.trendWeight);

            if (exportNeeds.Count > 0)
            {
                for (int a = 0; a < truckInventory.Count; a++)
                {
                    while (truckInventory[a].productObject == newProduct.productObject && truckInventory[a].productAmount == 3)
                    {
                        virtualProduct = inventoryManager.productList.ElementAt(Random.Range(0, inventoryManager.productList.Count));
                        newProduct = new Product(virtualProduct.productName, virtualProduct.productAmount, virtualProduct.productObject, virtualProduct.trendWeight);
                    }
                }

                bool addProduct = true;
                int productInd = 0;
                for (int a = 0; a < exportNeeds.Count; a++)
                {
                    if (exportNeeds[a].productObject == newProduct.productObject)
                    {
                        exportNeeds[a].productAmount++;
                        addProduct = false;
                        break;
                    }
                    else
                    {
                        productInd++;
                    }
                }
                if (addProduct)
                {
                    exportNeeds.Add(newProduct);
                    exportNeeds[productInd].productAmount++;
                }
            }
            else
            {
                exportNeeds.Add(newProduct);
                exportNeeds[0].productAmount++;
            }
        }
    }

    public void ImportOrder()
    {
        for (int a = 0; a < truckInventory.Count; a++)
        {
            Product virtualProduct = inventoryManager.productList.ElementAt(a);
            Product newProduct = new Product(virtualProduct.productName, virtualProduct.productAmount, virtualProduct.productObject, virtualProduct.trendWeight);
            truckInventory.Add(newProduct);
            truckInventory.ElementAt(truckInventory.IndexOf(newProduct)).productAmount++;
        }
            
    }

    public void ToggleDoors(PortType _type)
    {
        //if doorstate is false, door are closed, if true they are open
        this.GetComponent<Animator>().SetBool("TruckDoor", doorState);
        if (_type == PortType.Import)
        {
            //importDoor.GetComponent<Animator>().SetBool("DoorState", doorState);
        }
        else
        {
            //exportDoor.GetComponent<Animator>().SetBool("DoorState", doorState);
        }
    }
    public void EmptyTruck(Product newProduct)
    {

        if (truckInventory.Count <= 0 && onSpot)
        {
            StartCoroutine(CloseDoor());
        }
    }

    public void FillTruck(Product _newProduct)
    {
        Product virtualProduct = _newProduct;
        Product newProduct = new Product(virtualProduct.productName, virtualProduct.productAmount, virtualProduct.productObject, virtualProduct.trendWeight);
        if(exportNeeds.Contains(newProduct))
        {
            exportNeeds.ElementAt(exportNeeds.IndexOf(newProduct)).productAmount--;
            if (exportNeeds.ElementAt(exportNeeds.IndexOf(newProduct)).productAmount <= 0)
            {
                exportNeeds.Remove(newProduct);
            }
        }
        if(truckInventory.Contains(newProduct))
        {
            truckInventory.ElementAt(truckInventory.IndexOf(newProduct)).productAmount++;
        }
        else
        {
            truckInventory.Add(_newProduct);
            truckInventory.ElementAt(truckInventory.IndexOf(newProduct)).productAmount++;
        }
        if (exportNeeds.Count <= 0 && onSpot)
        {
            StartCoroutine(CloseDoor());
        }
    }

    IEnumerator CloseDoor()
    {
        doorState = false;
        ToggleDoors(this.GetComponent<TruckController>().task);
        yield return new WaitForSeconds(5);
        if (onSpot)
        {
            startTime = Time.time;
            Transform oldPos = endPos;
            endPos = startPos;
            startPos = oldPos;
            onSpot = false;
        }
        yield return new WaitForSeconds(3);
        var truckManager = GameObject.Find("TruckManager").GetComponent<TruckSpawner>();
        if (this.task == PortType.Import)
        {
            truckManager.importSpot = false;
        }
        if (this.task == PortType.Export)
        {
            truckManager.exportSpot = false;
        }
        truckManager.StartCoroutine(truckManager.NewTruck());
        Destroy(gameObject);
    }
}
