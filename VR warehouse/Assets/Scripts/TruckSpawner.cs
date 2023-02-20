using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckSpawner : MonoBehaviour
{
    public GameObject truck;
    public bool exportSpot;
    public bool importSpot;
    public Transform exportTrans;
    public Transform exportSpawn;
    public Transform importTrans;
    public Transform importSpawn;

    public void Start()
    {
        SpawnTruck();   
    }
    public void SpawnTruck()
    {
        if (!importSpot)
        {
           
            GameObject spawnedTruck = Instantiate(truck, importTrans.position, Quaternion.identity);
            spawnedTruck.GetComponent<TruckController>().startPos = importSpawn;
            spawnedTruck.GetComponent<TruckController>().endPos = importTrans;
            importSpot = true;
        }
        if(!exportSpot)
        {
            GameObject spawnedTruck = Instantiate(truck, exportTrans.position, Quaternion.identity);
            spawnedTruck.GetComponent<TruckController>().startPos = exportSpawn;
            spawnedTruck.GetComponent<TruckController>().endPos = exportTrans;
            exportSpot = true;
        }
    }
}
