using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerScript : MonoBehaviour
{
    [Header("Toggle VR/pc")]
    [SerializeField] bool pcMode;
    [Space(20)]
    [Header("Movement")]
    [SerializeField] float speed;
    Vector3 movement;
    Rigidbody playerRB;
    [SerializeField] GameObject rightHand;
    [SerializeField] XRController rightHandController;
    [SerializeField] float heightBonus;
    [SerializeField] GameObject beam;
    RaycastHit tpCheck;
    [SerializeField] Material legitTPPos;
    [SerializeField] Material illigalTPPos;
    [Space(20)]
    [Header("Camera")]
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
    [Header("Interaction")]
    RaycastHit interactableCheck;
    [SerializeField] Material vrButtonIndicator;
    [SerializeField] Material pcButtonIndicator;
    [Space(20)]
    [Header("Debugging")]
    [SerializeField] TimeToLoadTruck stresser;
    [Space(20)]
    [Header("Other")]
    bool paused;

    private void Start()
    {
        playerRB = GetComponent<Rigidbody>();

    }
    private void Update()
    {
        //pause game
        if (Input.GetButtonDown("Cancel"))
        {
            paused = !paused;
        }

        if (paused)
        {
            Cursor.lockState = CursorLockMode.None;
            return;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        //camera
        if (!pcMode)
        {
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
            cam.transform.localPosition = cameraRig.centerEyeAnchor.position + new Vector3(0, heightBonus, 0);
        }

        //camera without VR
        if (pcMode)
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
        //rightHand.transform.position = transform.position + cameraRig.rightHandAnchor.position + transform.up * 0.5f;
        //rightHand.transform.localRotation = cameraRig.rightHandAnchor.localRotation;
        //rightHand.transform.Rotate(0, -90, 0);
        //if (rightJoystick.y >= .5f)
        //{
        //    beam.SetActive(true);
        //    if (Physics.Raycast(rightHand.transform.position, -rightHand.transform.right, out tpCheck, 100))
        //    {
        //        beam.GetComponent<MeshRenderer>().material = legitTPPos;
        //    }
        //    else
        //    {
        //        beam.GetComponent<MeshRenderer>().material = illigalTPPos;
        //    }
        //}
        //else if (rightJoystick.y < .5f && beam.activeSelf)
        //{
        //    beam.SetActive(false);
        //    if (Physics.Raycast(rightHand.transform.position, -rightHand.transform.right, out tpCheck, 100))
        //    {
        //        transform.position = tpCheck.point + new Vector3(0, .3f, 0);
        //    }
        //}

        //interaction
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out interactableCheck, 6))
        {
            if (interactableCheck.transform.gameObject.TryGetComponent(out Interactable interactable))
            {
                if (pcMode)
                {
                    interactable.ShowUXButton(pcButtonIndicator);
                }
                else
                {
                    interactable.ShowUXButton(vrButtonIndicator);
                }

                if (Input.GetButtonDown("Use"))
                {
                    //pickup box
                    if (interactable.gameObject.TryGetComponent<Box>(out Box n_box))
                    {
                        n_box.PickUpBox(this);
                    }

                    //use button
                    if (interactable.gameObject.TryGetComponent<ButtonFunctions>(out ButtonFunctions n_function))
                    {
                        n_function.ActivateButtonFunction();
                    }

                    //pickup pallet
                    if (interactable.gameObject.TryGetComponent<Pallet>(out Pallet n_pallet))
                    {
                        n_pallet.PickUp(this);
                    }

                    //implement more interactions here
                }
            }
        }

        //chatgpt stuff
        //Vector3 headPosition = InputTracking.GetLocalPosition(XRNode.Head);
        //Quaternion headRotation = InputTracking.GetLocalRotation(XRNode.Head);

        //if (rightHandDevice.TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 handPosition))
        //{
        //    Vector3 newHandPosition = headRotation * (handPosition - headPosition) + headPosition + initialOffset;
        //    rightHand.transform.position = newHandPosition;
        //}

        //if (rightHandDevice.TryGetFeatureValue(CommonUsages.deviceRotation, out Quaternion handRotation))
        //{
        //    Quaternion newHandRotation = handRotation * Quaternion.Inverse(headRotation);
        //    rightHand.transform.rotation = newHandRotation;
        //}
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
            transform.Rotate(0, degrees, 0);
        }
    }

    public Transform PlayerTransform()
    {
        return transform;
    }

    public Transform CamTransform()
    {
        return cam.transform;
    }

    public Transform VRHandTransform()
    {
        return rightHand.transform;
    }

    public bool isVr()
    {
        return !pcMode;
    }

    public void DebugStressRelaxSwitch(bool n_stress)
    {
        if (n_stress)
        {
            stresser.ActivateStressMode();
        }
        else
        {
            stresser.DeactivateStressMode();
        }
    }

    public void DebugDisplayList()
    {
        List<Product> n_debugList = new List<Product> {
            new Product("Debug1", 1, null, 10),
            new Product("Debug2", 2, null, 10),
            new Product("Debug3", 3, null, 10),
            new Product("Debug4", 2, null, 10),
            new Product("Debug5", 1, null, 10),
            new Product("Debug6", 3, null, 10),
            new Product("Debug7", 1, null, 10)
        };
        GameObject n_displayScreen = GameObject.FindGameObjectWithTag("DisplayScreen");
        //replace n_debugList with the list of export products
        //if statement is to make sure the game doesn't crash if there is no displayscreen in the scene
        if (n_displayScreen != null)
        {
            n_displayScreen.GetComponent<DisplayOrders>().SetList(n_debugList, null);
        }
    }
}
