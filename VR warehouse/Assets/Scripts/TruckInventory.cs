using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckInventory : MonoBehaviour
{
    public bool[,] inventorySpaces =  new bool[2,3];
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateInventory(Product _newProduct)
    {
        GameObject productObject = Instantiate(_newProduct.productObject, transform.position, Quaternion.identity, this.gameObject.transform);
        Vector3 productPosistion = transform.position;
        bool updated = false;

        for (int y = 0; y < 3; y++)
        {
            for (int x = 0; x < 2; x++)
            {
                if (inventorySpaces[x, y] == false)
                {
                    productPosistion.x += -0.45f + (0.45f * x);
                    productPosistion.z += -1.5f + (1.5f * y);
                    productPosistion.y += -1.4f;
                    productObject.transform.position = productPosistion;
                    inventorySpaces[x, y] = true;
                    updated = true;
                    break;
                }
            }
            if(updated)
            {
                break;
            }
        }
        
    }
}
