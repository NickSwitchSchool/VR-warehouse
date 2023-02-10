using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float speed;
    Vector3 movement;
    Rigidbody playerRB;
    [SerializeField] GameObject rightHand;
    [SerializeField] GameObject beam;
    RaycastHit tpCheck;
    [SerializeField] Material legitTPPos;
    [SerializeField] Material illigalTPPos;
    [Space(20)]
    [Header("Camera")]
    [SerializeField] bool testWithoutVR;
    Vector3 rotation;
    Vector3 camrotation;
    float mouseVertical;
    [Space(10)]
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
        Vector2 rightJoystick = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);
        if (rightJoystick.x >= .5f)
        {
            QuickRotate(quickRotateDegrees);
        }
        else if (rightJoystick.x <= -.5f)
        {
            QuickRotate(-quickRotateDegrees);
        }
        Quaternion headsetRotation = cameraRig.centerEyeAnchor.rotation;
        cam.transform.rotation = headsetRotation;

        //camera without VR
        if (testWithoutVR)
        {
            rotation.y += Input.GetAxis("Mouse X") * sensitivity;
            transform.eulerAngles = rotation;
            camrotation.y = rotation.y;
            mouseVertical = Input.GetAxis("Mouse Y");
            if (camrotation.x < maxXRotation && camrotation.x > -maxXRotation)
            {
                camrotation.x -= mouseVertical * sensitivity;
            }
            else if (camrotation.x >= maxXRotation && mouseVertical > 0)
            {
                camrotation.x -= mouseVertical * sensitivity;
            }
            else if (camrotation.x <= -maxXRotation && mouseVertical < 0)
            {
                camrotation.x -= mouseVertical * sensitivity;
            }
            cam.transform.eulerAngles = camrotation;
        }

        //teleport
        rightHand.transform.localPosition = cameraRig.rightHandAnchor.position + new Vector3(0, .9f, 0) + cameraRig.transform.forward * 0.2f;
        rightHand.transform.localRotation = cameraRig.rightHandAnchor.rotation;
        if (OVRInput.Get(OVRInput.Button.Two))
        {
            beam.SetActive(true);
            if (Physics.Raycast(rightHand.transform.position, rightHand.transform.forward, out tpCheck, 100))
            {
                beam.GetComponent<MeshRenderer>().material = legitTPPos;
            }
            else
            {
                beam.GetComponent<MeshRenderer>().material = illigalTPPos;
            }
        }
        else if (OVRInput.GetUp(OVRInput.Button.Two))
        {
            beam.SetActive(false);
            if (Physics.Raycast(rightHand.transform.position, rightHand.transform.forward, out tpCheck, 100))
            {
                transform.position = tpCheck.point + new Vector3(0, .3f, 0);
            }
        }
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
            //rightHand.transform.Rotate(0, -degrees, 0);
        }
    }
}
