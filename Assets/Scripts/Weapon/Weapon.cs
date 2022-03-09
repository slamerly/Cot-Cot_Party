using UnityEngine;
using UnityEngine.Audio;

public class Weapon : MonoBehaviour
{
    public float damage = 10f;
    public float range = 50f;
    public float fireRate = 15f;
    public float impactForce = 60f;

    public int nbMaxBullet = 30;
    public int currentBullet;
    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public bool auto = false;
    public bool weaponReady = true;
    //public GameObject impactEffect;

    private float delayFire = 0f;

    private void Start()
    {
        fpsCam = Camera.main;
        currentBullet = nbMaxBullet;
    }

    /*private void OnEnable()
    {
        weaponReady = false;
        GameObject.Find("Weapon").GetComponent<WeaponSwitch>().animator.SetBool("Switching", false);
    }
    */

    void Update()
    {
        if (weaponReady)
        {
            if (auto)
            {
                if (Input.GetButton("Fire1") && Time.time >= delayFire && currentBullet >= 0)
                {
                    delayFire = Time.time + 1f / fireRate;
                    Shoot();
                }
            }
            else
            {
                if (Input.GetButtonDown("Fire1") && Time.time >= delayFire && currentBullet >= 0)
                {
                    delayFire = Time.time + 1f / fireRate;
                    Shoot();
                }
            }
        }
    }

    void Shoot()
    {
        muzzleFlash.Play();
        gameObject.GetComponent<AudioSource>().Play();

        currentBullet--;

        RaycastHit hit;
        Target target;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            //Debug.Log(hit.transform.name);

            target = hit.transform.GetComponent<Target>();
            if(target != null)
                target.TakeDamage(damage);

            if (hit.rigidbody != null)
                hit.rigidbody.AddForce(-hit.normal * impactForce);

            //GameObject impact = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            //Destroy(impact, 2f);
        }
    }
}
