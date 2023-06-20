using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anima : MonoBehaviour
{
    [SerializeField] Animator anim;
    public void EndShoot()
    {
        anim.SetBool("shoot", false);
    }

    public void EndReload()
    {
        anim.SetBool("reload", false);
    }
}
