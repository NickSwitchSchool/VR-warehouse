using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float sensitivity;
    public float speed;
    public float maxXRotation;
    float mouseVertical;

    public GameObject cam;

    Vector3 rotation;
    Vector3 camrotation;
    Vector3 movement;

    Rigidbody playerRB;

    public OVRCameraRig cameraRig;

    private void Start()
    {
        playerRB = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        //camera
        Vector3 headsetPosition = cameraRig.centerEyeAnchor.position;
        Quaternion headsetRotation = cameraRig.centerEyeAnchor.rotation;
        cam.transform.rotation = headsetRotation;
    }

    private void FixedUpdate()
    {
        //movement
        movement.x = Input.GetAxis("Horizontal");
        movement.z = Input.GetAxis("Vertical");
        MovePlayer();
    }

    private void MovePlayer()
    {
        Vector3 moveVector = transform.TransformDirection(movement) * speed * Time.deltaTime;
        playerRB.velocity = new Vector3(moveVector.x, playerRB.velocity.y, moveVector.z);
    }
}
