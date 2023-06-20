using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBehaviour : MonoBehaviour
{
    [SerializeField] float Speed;
    Rigidbody rig;

    private void Start()
    {
        rig = GetComponent<Rigidbody>();
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            if(Vector3.Distance(other.transform.position,transform.position) > 2)
                rig.MovePosition(Vector3.Slerp(transform.position,other.transform.position,Time.deltaTime * Speed));

            transform.LookAt(new Vector3(other.transform.position.x, transform.position.y, other.transform.position.z));
        }
    }
}
