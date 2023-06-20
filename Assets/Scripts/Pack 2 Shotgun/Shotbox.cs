using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotbox : MonoBehaviour
{
    public float ShotDMG;
    bool damaged;
    Ray ray;
    RaycastHit hit;

    private void Awake()
    {
        ray = new Ray(transform.position, transform.forward);
    }

    private void Update()
    {
        if(Physics.SphereCast(ray,0.5f,out hit,9) && damaged == false)
        {
            hit.transform.gameObject.GetComponent<Lifeline>().hp -= ShotDMG;
            damaged = true;
        }
    }
}
