using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationCtrl : MonoBehaviour
{
    [SerializeField] Transform mover;
    [SerializeField] float treshold;

    Vector3 dirc;

    private void Update()
    {
        dirc = mover.transform.forward;
        dirc = Vector3.Lerp(Vector3.zero, dirc, treshold);

        transform.rotation = Quaternion.LookRotation(dirc);
    }
}