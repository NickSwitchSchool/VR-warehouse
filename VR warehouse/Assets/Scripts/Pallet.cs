using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pallet : MonoBehaviour
{
    [SerializeField] List<GameObject> boxes;
    public GameObject emptyPalletVariation;
    [SerializeField] GameObject strap;

    public void Strap()
    {
        strap.SetActive(true);
    }

    public List<GameObject> GetBoxesThatNeedToBeOrdered()
    {
        return boxes;
    }
}
