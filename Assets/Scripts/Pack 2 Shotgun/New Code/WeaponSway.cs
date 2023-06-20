using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    [SerializeField] private float smoothing;
    [SerializeField] private float swayMultiplier;

    private void Update()
    {
        Rotation();
    }

    void Rotation()
    {
        float moX = Input.GetAxisRaw("Mouse X") * swayMultiplier;
        float moY = Input.GetAxisRaw("Mouse Y") * swayMultiplier;

        Quaternion rotX = Quaternion.AngleAxis(-moY, Vector3.right);
        Quaternion rotY = Quaternion.AngleAxis(moX, Vector3.up);

        Quaternion tRot = rotX * rotY;

        transform.localRotation = Quaternion.Slerp(transform.localRotation, tRot, smoothing * Time.deltaTime);
    }
}
