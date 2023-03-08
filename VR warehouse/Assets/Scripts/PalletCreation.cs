using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalletCreation : MonoBehaviour
{
    [SerializeField] string palletName;
    [SerializeField] List<string> needsTags;
    [SerializeField] List<GameObject> boxes;
    public List<GameObject> packedBoxes;
    [SerializeField] GameObject completeProductPrefab;

    private void Update()
    {
        if (needsTags.Count == 0)
        {
            foreach (GameObject packedBox in packedBoxes)
            {
                Destroy(packedBox);
            }
            Instantiate(completeProductPrefab, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    public void AddItemToPallet(GameObject n_item, int n_ListIndex)
    {
        if (n_ListIndex != -1)
        {
            needsTags.RemoveAt(n_ListIndex);
            boxes[n_ListIndex].SetActive(true);
            packedBoxes.Add(boxes[n_ListIndex]);
            boxes.RemoveAt(n_ListIndex);
            Destroy(n_item);
        }
    }

    public int IsItemRequested(string n_itemTag)
    {
        for (int i = 0; i < needsTags.Count; i++)
        {
            if (needsTags[i] == n_itemTag)
            {
                return i;
                i = needsTags.Count;
            }
        }
        return -1;
    }
}
