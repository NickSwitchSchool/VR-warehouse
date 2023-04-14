using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCalc : MonoBehaviour
{
    int totalDistance = 0;
    int truckScore = 0;
    // Start is called before the first frame update
    void Start()
    {
        totalDistance = PlayerPrefs.GetInt("Robot Distance");
        truckScore = PlayerPrefs.GetInt("TruckScore");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
