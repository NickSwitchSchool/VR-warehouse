using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float speed;
    Vector3 movement;
    Rigidbody playerRB;
    [Space(20)]
    [Header("Camera")]
    [SerializeField] float sensitivity;
    [SerializeField] float maxXRotation;
    [SerializeField] float quickRotateInterval;
    [SerializeField] float quickRotateDegrees;
    float intervalTimer;
    [SerializeField] GameObject cam;
    [SerializeField] OVRCameraRig cameraRig;

    private void Start()
    {
        playerRB = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        //camera
        Vector2 rotateCam = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);
        if (rotateCam.x >= .5f)
        {
            QuickRotate(quickRotateDegrees);
        }
        else if (rotateCam.x <= -.5f)
        {
            QuickRotate(-quickRotateDegrees);
        }
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

    private void QuickRotate(float degrees)
    {
        intervalTimer += Time.deltaTime;

        if (intervalTimer >= quickRotateInterval)
        {
            intervalTimer = 0;
            cameraRig.gameObject.transform.Rotate(0, degrees, 0);
        }
    }
}
