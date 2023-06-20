using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lifeline : MonoBehaviour
{
    public float hp;

    RaycastHit hit;
    private void Update()
    {
        if(hp < 0)
        {
            Destroy(gameObject);
        }
    }
}
