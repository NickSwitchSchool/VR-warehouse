using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreCalc : MonoBehaviour
{
    float totalDistance = 0;
    int truckScore = 0;
    public TextMeshProUGUI scoreText;
    // Start is called before the first frame update
    void Start()
    {
        totalDistance = PlayerPrefs.GetInt("Robot Distance");
        truckScore = PlayerPrefs.GetInt("TruckScore");

        ChangeScore(totalDistance, truckScore);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeScore(float _totalDisance, int _truckScore)
    {
        scoreText.text = _totalDisance.ToString();
    }
}
