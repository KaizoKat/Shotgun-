using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore;

public class Animate : MonoBehaviour
{
    [SerializeField] GameObject Model;

    [SerializeField] ClickChecking Checker;
    
    Transform s_pos;
    Transform s_posBarrel;
    Transform s_posHandle;
    Transform s_posRelaoder;

    byte previousAmmo;
    bool click = false;
    private void Awake()
    {
        previousAmmo = Checker.mag;
        s_pos = Model.transform;
    }

    bool shoot;
    private void FixedUpdate()
    {
        if (previousAmmo != Checker.mag)
            AnimateShot();

        if (Checker.reloading == true)
            StartCoroutine(AnimateRelaod());

        previousAmmo = Checker.mag;
    }

    void AnimateShot()
    {
        Model.GetComponent<Animator>().SetBool("shoot", true);
    }

    IEnumerator AnimateRelaod()
    {
        if (Input.GetMouseButton(0))
            click = true;


        if(click == true)
        {
            Model.GetComponent<Animator>().SetBool("reload", true);
            if (Model.GetComponent<Animator>().GetBool("shellUp") == false && Checker.mag < Checker.startingMag)
            {
                if (Checker.mag != Checker.startingMag - 1)
                    Model.GetComponent<Animator>().SetBool("shellUp", true);
                Checker.mag++;
            }
            else if (Checker.mag == Checker.startingMag)
            {
                Model.GetComponent<Animator>().SetBool("reload", false);
                yield return new WaitForSeconds(1);
                Checker.reloading = false;
            }
        }
        else if (click == false)
        {
            Model.GetComponent<Animator>().SetBool("reload", false);
            yield return new WaitForSeconds(1);
            Checker.reloading = false;
        }
    }
}
