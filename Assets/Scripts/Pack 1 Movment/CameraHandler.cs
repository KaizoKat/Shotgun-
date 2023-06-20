using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    [SerializeField] WallRun wallRunScr;
    [SerializeField] private float sensX;
    [SerializeField] private float sensY;
    [SerializeField] Transform mCam;
    [SerializeField] Transform orientation;

    float mouseX;
    float mouseY;

    float multiplier = 0.01f;

    float xRot;
    float yRot;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        MyInput();

        mCam.localRotation = Quaternion.Euler(xRot, yRot, wallRunScr.tilt);
        orientation.transform.rotation = Quaternion.Euler(0,yRot, 0);
    }

    void MyInput()
    {
        mouseX = Input.GetAxisRaw("Mouse X");
        mouseY = Input.GetAxisRaw("Mouse Y");

        yRot += mouseX * sensX * multiplier;
        xRot -= mouseY * sensY * multiplier;

        xRot = Mathf.Clamp(xRot, -89.0f, 90.0f);

    }
}
