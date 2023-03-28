using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pallet : MonoBehaviour
{
    [SerializeField] List<GameObject> boxes;
    public GameObject emptyPalletVariation;
    [SerializeField] GameObject strap;
    public int height;
    public Vector3 robotGoalPosition;
    public bool isStored;

    private void Start()
    {
        GameObject n_bot = GameObject.FindGameObjectWithTag("Robots");
        if (n_bot != null)
        {
            n_bot.GetComponent<RobotController>().productsInScene.Add(this);
        }
    }

    public void Strap()
    {
        strap.SetActive(true);
    }

    public void Store(int n_height, Vector3 n_robotGoalPosition)
    {
        height = n_height;
        robotGoalPosition = n_robotGoalPosition;
        isStored = true;
    }

    public List<GameObject> GetBoxesThatNeedToBeOrdered()
    {
        return boxes;
    }
}
