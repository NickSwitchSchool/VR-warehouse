using Oculus.Platform;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TruckSpawner : MonoBehaviour
{
    public int ordersCompleted;
    public int importAmount;
    public bool orderFilled;
    public GameObject truck;
    public bool exportSpot;
    public bool importSpot;
    public float timeWindow;
    public int truckScore;
    public Transform exportTrans;
    public Transform exportSpawn;
    public GameObject exportDoor;
    public Transform importTrans;
    public Transform importSpawn;
    public GameObject importDoor;

    [Header("Current Trucks")]
    public GameObject importTruck;
    public GameObject exportTruck;

    public void Start()
    {
        SpawnTruck();   
    }
    public void SpawnTruck()
    {
        if (!importSpot)
        {
           
            GameObject spawnedTruck = Instantiate(truck, importTrans.position, Quaternion.identity);
            spawnedTruck.GetComponent<TruckController>().task = TruckController.PortType.Import;
            spawnedTruck.GetComponent<TruckController>().startPos = importSpawn;
            spawnedTruck.GetComponent<TruckController>().endPos = importTrans;
            importSpot = true;
            spawnedTruck.GetComponent<TruckController>().timeWindow = timeWindow;
            spawnedTruck.GetComponent<TruckController>().importDoor = importDoor;

            importTruck = spawnedTruck;
            if(importAmount < 3)
            {
                importAmount++;
                return;
            }
        }
        if(!exportSpot)
        {
            GameObject spawnedTruck = Instantiate(truck, exportTrans.position, Quaternion.identity);
            spawnedTruck.GetComponent<TruckController>().task = TruckController.PortType.Export;
            spawnedTruck.GetComponent<TruckController>().startPos = exportSpawn;
            spawnedTruck.GetComponent<TruckController>().endPos = exportTrans;
            exportSpot = true;
            spawnedTruck.GetComponent<TruckController>().timeWindow = timeWindow;
            spawnedTruck.GetComponent<TruckController>().exportDoor = exportDoor;

            exportTruck = spawnedTruck;
        }
        
    }
    public IEnumerator NewTruck()
    {
        if (exportTruck != null)
        {
            if (orderFilled)
            {
                truckScore += 20;
                ordersCompleted++;
                orderFilled = false;
            }
            else
            {
                truckScore -= 10;
                orderFilled = false;
            }
        }
        if (ordersCompleted >= 20)
        {
            SceneManager.LoadScene("EndScene");
        }
        else
        {
            this.GetComponent<TruckSpawner>().SpawnTruck();
        }
        yield return new WaitForSeconds(5);
        
        StopCoroutine(NewTruck());
    }
    
}
