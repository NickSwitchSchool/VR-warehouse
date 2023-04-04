using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageSlot : MonoBehaviour
{
    [SerializeField] int height;
    [SerializeField] Vector3 robotGoalPosition;

    private void OnCollisionStay(Collision other)
    {
        GameObject n_other = other.gameObject;
        if (n_other.TryGetComponent<Pallet>(out Pallet n_pallet))
        {
            Debug.Log("YESSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSS");
            other.transform.position = transform.position;
            n_pallet.Store(height, robotGoalPosition);
        }
    }
}
