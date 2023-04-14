using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pallet : MonoBehaviour
{
    [SerializeField] List<GameObject> boxes;
    public GameObject emptyPalletVariation;
    [SerializeField] GameObject strap;
    public string name;
    public int height;
    public Vector3 robotGoalPosition;
    public bool isStored;

    PlayerScript playerscript;
    bool pickedUp;
    float pickedUpTime;
    [SerializeField] float pickedUpDistanceVR;
    [SerializeField] float pickedUpDistancePC;

    private void Start()
    {
        GameObject n_bot = GameObject.FindGameObjectWithTag("Robots");
        if (n_bot != null)
        {
            n_bot.GetComponent<RobotController>().productsInScene.Add(this);
        }
    }

    private void Update()
    {
        if (pickedUp && playerscript.isVr())
        {
            pickedUpTime += Time.deltaTime;
            transform.position = playerscript.VRHandTransform().position + playerscript.VRHandTransform().right * pickedUpDistanceVR;
            transform.rotation = playerscript.CamTransform().rotation;
        }
        else if (pickedUp)
        {
            pickedUpTime += Time.deltaTime;
            var newPos = playerscript.PlayerTransform().position + playerscript.CamTransform().forward * pickedUpDistancePC;
            transform.position = newPos;
            transform.rotation = playerscript.CamTransform().rotation;
        }
    }

    public void PickUp(PlayerScript n_playerscript)
    {
        pickedUp = true;
        playerscript = n_playerscript;
    }

    public void Strap()
    {
        strap.SetActive(true);
    }

    public void Store(int n_height, Vector3 n_robotGoalPosition)
    {
        pickedUp = false;
        height = n_height;
        robotGoalPosition = n_robotGoalPosition;
        isStored = true;
    }

    public List<GameObject> GetBoxesThatNeedToBeOrdered()
    {
        return boxes;
    }
}
