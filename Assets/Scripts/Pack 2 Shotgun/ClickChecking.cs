using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickChecking : MonoBehaviour
{
    [SerializeField] public byte ammo = 128;
    [SerializeField] public byte mag = 8;
    [SerializeField] public bool reloading;
    [SerializeField] public bool canShoot;
    [HideInInspector] public byte startingMag;
    [SerializeField] public float shotDamage;

    private float downtime;
    
    private void Start()
    {
        startingMag = mag;
    }

    private void Update()
    {   
        //shoot check
        if (canShoot == true)
        {
            if (Input.GetMouseButtonDown(0) && reloading == false && mag != 0 && ammo != 0)
            {
                downtime = 0.83f;
                mag--;
                canShoot = false;
            }
        }

        //magazine check
        if (mag == 0)
        {
            ammo -= startingMag;
            reloading = true;
        }
        else if(mag > 0 && reloading  == true)
        {
            downtime = 0.02f;
        }

        //downtime gate
        if (downtime > 0)
        {
            downtime -= Time.deltaTime;
        }
        else if (downtime < 0)
        {
            downtime = 0;
        }
        else if (downtime == 0)
        {
            canShoot = true;
        }
    }

}