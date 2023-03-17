using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    [SerializeField] List<GameObject> content;
    Rigidbody rb;
    PlayerScript playerscript;
    [SerializeField] float pickedUpDistancePC;
    [SerializeField] float pickedUpDistanceVR;
    float pickedUpTime;
    bool pickedUp;
    Animator anim;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        TryGetComponent<Animator>(out anim);
        if (anim != null)
        {
            anim.SetBool("Open", false);
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<PalletCreation>(out PalletCreation n_pallet))
        {
            n_pallet.AddItemToPallet(this.gameObject, n_pallet.IsItemRequested(gameObject.tag));
        }
    }
}
