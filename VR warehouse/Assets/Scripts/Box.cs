using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    [SerializeField] List<GameObject> content;
    Rigidbody rb;
    PlayerScript playerscript;
    [SerializeField] float pickedUpDistance;
    float pickedUpTime;
    bool pickedUp;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (pickedUp)
        {
            pickedUpTime += Time.deltaTime;
            var newPos = playerscript.PlayerTransform().position + playerscript.CamTransform().forward * pickedUpDistance;
            transform.position = newPos;
            transform.rotation = playerscript.CamTransform().rotation;
        }
    }

    public void PickUpBox(PlayerScript n_playerscript)
    {
        pickedUp = true;
        rb.constraints = RigidbodyConstraints.FreezeAll;
        playerscript = n_playerscript;
    }

    public void PlaceDownBox()
    {
        pickedUp = false;
        pickedUpTime = 0;
        rb.constraints = RigidbodyConstraints.None;
    }

    public void AddContent(GameObject n_content)
    {
        //implement adding items here
    }

    private void OnCollisionStay(Collision collision)
    {
        if (pickedUp && pickedUpTime > .7f)
        {
            PlaceDownBox();
        }
    }
}
