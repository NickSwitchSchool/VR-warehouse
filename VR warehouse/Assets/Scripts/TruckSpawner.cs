using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckSpawner : MonoBehaviour
{
    public int ordersCompleted;
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
            spawnedTruck.GetComponent<TruckController>().task = TruckController.PortType.Import;
            spawnedTruck.GetComponent<TruckController>().startPos = importSpawn;
            spawnedTruck.GetComponent<TruckController>().endPos = importTrans;
            importSpot = true;

        }
        if(!exportSpot)
        {
            GameObject spawnedTruck = Instantiate(truck, exportTrans.position, Quaternion.identity);
            spawnedTruck.GetComponent<TruckController>().task = TruckController.PortType.Export;
            spawnedTruck.GetComponent<TruckController>().startPos = exportSpawn;
            spawnedTruck.GetComponent<TruckController>().endPos = exportTrans;
            exportSpot = true;
        }
    }
    public IEnumerator NewTruck()
    {
        yield return new WaitForSeconds(5);
        ordersCompleted++;
        //if (ordersCompleted >= difficulty)
        //{
        //    //complete game
        //}
        //else
        //{
        //    this.GetComponent<TruckSpawner>().SpawnTruck();
        //}
       
        StopCoroutine(NewTruck());
    }
    
}
