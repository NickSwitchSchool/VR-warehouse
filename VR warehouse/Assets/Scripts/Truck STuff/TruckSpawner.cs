using Oculus.Platform;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TruckSpawner : MonoBehaviour
{
    public int ordersCompleted;
    public GameObject truck;
    public bool exportSpot;
    public bool importSpot;
    public float timeWindow;
    public Transform exportTrans;
    public Transform exportSpawn;
    public GameObject exportDoor;
    public Transform importTrans;
    public Transform importSpawn;
    public GameObject importDoor;

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
        }
        
    }
    public IEnumerator NewTruck()
    {
        yield return new WaitForSeconds(5);
        ordersCompleted++;
        if (ordersCompleted >= 20)
        {
            SceneManager.LoadScene("EndScene");
        }
        else
        {
            this.GetComponent<TruckSpawner>().SpawnTruck();
        }
        StopCoroutine(NewTruck());
    }
    
}
