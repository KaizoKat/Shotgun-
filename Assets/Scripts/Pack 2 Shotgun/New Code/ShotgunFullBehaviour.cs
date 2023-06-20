using System.Collections;
using UnityEngine;


public class ShotgunFullBehaviour : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] int magazine = 8;
    [SerializeField] int fullAmmo = 256;
    [SerializeField] byte damage;
    [SerializeField] byte shotPellets;
    [SerializeField] float area;
    [SerializeField] LayerMask hitLayer;
    public float scaleLimit = 2.0f;
    public float castDistance = 10f;

    bool animatedShot;
    bool animatedReload;
    public bool reload; 
    bool hasShot;
    bool canShoot;

    [Header("Game Objects")]
    [SerializeField] GameObject shotOutput;
    [SerializeField] GameObject shotDecal;
    [SerializeField] GameObject flash;
    [SerializeField] GameObject Model;

    int startingMagazine = 0;
    int startingAmmo = 0;
    void Start()
    {
        startingMagazine = magazine;
        startingAmmo = fullAmmo;
    }

    void Update()
    {
        StartCoroutine(HandleGun());
    }

    IEnumerator HandleGun()
    {
        if(Input.GetMouseButtonDown(0))
            canShoot = true;

        if(Input.GetKeyDown(KeyCode.R))
            reload = true;

        if (magazine == 0)
            reload = true;

        if (magazine < 0)
            magazine = 0;

        if(reload == true && magazine < startingMagazine)
        {
            canShoot = false;
            if(animatedReload == false)
                Model.GetComponent<Animator>().SetBool("reload", true);

            animatedReload = true;

            if(animatedReload == true && Model.GetComponent<Animator>().GetBool("reload") == false)
            {
                fullAmmo -= startingMagazine - magazine;
                magazine = startingMagazine;
                reload = false;
            }
        }
        else if(reload == true && magazine == startingMagazine)
            reload = false;
            

        if (animatedReload == true && Model.GetComponent<Animator>().GetBool("reload") == false)
            animatedReload = false;

        if (hasShot == false && reload == false && canShoot == true)
        {
            hasShot = true;
            if(magazine != 0)
            {
                
                flash.SetActive(true);
                yield return new WaitForSeconds(0.05f);
                flash.SetActive(false);
                magazine--;

                if (animatedShot == false)
                {
                    animatedShot = true;
                    Model.GetComponent<Animator>().SetBool("shoot", true);
                    Shoot();
                }
            }
        }

        if (animatedShot == true && Model.GetComponent<Animator>().GetBool("shoot") == false)
        {
            animatedShot = false;
            hasShot = false;
            canShoot = false;
        }
    }
    void Shoot()
    {
        for (int i = 0; i < shotPellets; ++i)
        {
            float randomRadius = Random.Range(0, scaleLimit);

            float randomAngle = Random.Range(0, 2 * Mathf.PI);

            Vector3 direction = new Vector3(
                randomRadius * Mathf.Cos(randomAngle),
                randomRadius * Mathf.Sin(randomAngle),
                castDistance
            );

            direction = transform.TransformDirection(direction.normalized);

            Ray r = new Ray(transform.position, direction);
            RaycastHit hit;
            if (Physics.Raycast(r, out hit))
            {
                Debug.DrawLine(transform.position, hit.point);

                if (hit.transform.tag == "Enemy")
                {
                    Instantiate(shotDecal, hit.point, Quaternion.LookRotation(hit.normal, hit.point), hit.transform);
                    hit.transform.GetComponent<Lifeline>().hp -= damage;
                }
                else if (hit.transform.tag != "Enemy")
                {
                    Instantiate(shotDecal, hit.point, Quaternion.LookRotation(hit.normal, hit.point), hit.transform.parent);
                }
            }
        }
    }
}
