using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public Animator animator;
    public GameController gameMaster;

    public AudioClip gunShoot;
    public AudioClip gunImpact;
    public AudioClip gunReload;
    private AudioSource audioPlayer;

    float damage = 10f;
    float range = 1000f;
    float fireRate = 3.5f;
    float nextTimeToFire = 0f;

    int maxAmmo = 10;
    int currentAmmo;
    float reloadTime = 3.5f;
    bool isReloading = false;

    // Start is called before the first frame update
    void Start()
    {
        currentAmmo = maxAmmo;
        audioPlayer = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //Check if the game is over
        if (gameMaster.GetGameState() == false)
        {
            if (isReloading)
                return;

            if (currentAmmo <= 0)
            {
                StartCoroutine(Reload());
                return;
            }

            if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1f / fireRate;
                Shoot();
                audioPlayer.PlayOneShot(gunShoot);
            }
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        audioPlayer.PlayOneShot(gunReload);
        animator.SetBool("Reloading", true);
        yield return new WaitForSeconds(reloadTime);
        animator.SetBool("Reloading", false);

        currentAmmo = maxAmmo;
        isReloading = false;
    }

    void Shoot()
    {
        muzzleFlash.Play();
        currentAmmo--;

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            //Debug.Log(hit.transform.name);

            //Enemy target = hit.transform.GetComponent<Enemy>();
            //if (target != null)
            //{
            //    target.TakeDamage(damage);
            //}
            if (hit.transform.tag == "Enemy")
            {
                Enemy target = hit.transform.GetComponent<Enemy>();
                if (target != null)
                {
                    target.TakeDamage(damage);
                }
            }
            else if (hit.transform.tag == "Battleship")
            {
                Battleship target = hit.transform.GetComponent<Battleship>();
                if (target != null)
                {
                    target.TakeDamage(damage);
                }
            }
            else if (hit.transform.tag == "Pirateship")
            {
                PirateShip target = hit.transform.GetComponent<PirateShip>();
                if (target != null)
                {
                    target.TakeDamage(damage);
                }
            }
            else if (hit.transform.tag == "Balloon")
            {
                Balloon target = hit.transform.GetComponent<Balloon>();
                if (target != null)
                {
                    target.TakeDamage(damage);
                }
            }

            GameObject impactObj = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            AudioSource.PlayClipAtPoint(gunImpact, hit.point);
            Destroy(impactObj, 2f);
        }
    }
}
