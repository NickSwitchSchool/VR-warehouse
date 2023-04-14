using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageSlot : MonoBehaviour
{
    [SerializeField] int height;
    [SerializeField] Vector3 robotGoalPosition;
    public bool empty;

    private void Start()
    {
        empty = true;
    }

    private void OnCollisionStay(Collision other)
    {
        if (!empty)
        {
            return;
        }

        GameObject n_other = other.gameObject;
        if (n_other.TryGetComponent<Pallet>(out Pallet n_pallet))
        {
            Debug.Log("YESSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSS");
            other.transform.position = transform.position;
            n_pallet.Store(height, robotGoalPosition, this);
            empty = false;
        }
    }
}
