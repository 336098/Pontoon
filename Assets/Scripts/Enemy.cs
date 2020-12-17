using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    float maxHealth = 50f;
    float currentHealth;
    float currentHealthPercent;

    public ShootPointList pointCollection;
    public Cabana cabanaScript;
    public Transform cabanaPosition;
    public ParticleSystem cannonFlash;
    public GameController gameMaster;
    float moveSpeed = 10f;
    float rotationSpeed = 3f;
    float fireRate = 5f;
    float nextTimeToFire = 0f;
    float shootDamage = 5f;

    public Camera playerCamera;
    public Canvas healthCanvas;
    public Image healthBar;

    public bool isDead = false;
    public ParticleSystem[] explosionArray;
    public AudioSource audioPlayer;
    public AudioClip shootSound;
    public AudioClip explosionSound;

    GameObject closestObj;
    Vector3 direction;
    Quaternion lookRotation;

    // Start is called before the first frame update
    void Start()
    {
        audioPlayer = this.GetComponent<AudioSource>();

        currentHealth = maxHealth;
        currentHealthPercent = (float)currentHealth / (float)maxHealth;

        pointCollection = GameObject.FindWithTag("ShootPositionCollection").GetComponent<ShootPointList>();
        cabanaScript = GameObject.FindWithTag("Cabana").GetComponent<Cabana>();
        cabanaPosition = GameObject.FindWithTag("Cabana").transform;
        gameMaster = GameObject.FindWithTag("GameMaster").GetComponent<GameController>();
        playerCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();

        DeterminePosition();

        transform.LookAt(closestObj.transform);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
            MoveToPosition();

        if (transform.position == closestObj.transform.position)
        {
            LookAtCabana();
            if (Time.time >= nextTimeToFire && !isDead)
            {
                nextTimeToFire = Time.time + fireRate;
                cannonFlash.Play();
                audioPlayer.PlayOneShot(shootSound, 0.5f);

                //Give damage to the cabana
                cabanaScript.TakeDamage(shootDamage);
            }
        }

        healthBarFacePlayer();
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealthPercent = (float)currentHealth / (float)maxHealth;
        healthBar.fillAmount = currentHealthPercent;

        if (currentHealth <= 0)
        {
            if (!isDead)
                Die();
        }
    }

    void Die()
    {
        foreach (ParticleSystem explosion in explosionArray)
        {
            explosion.Play();
        }
        isDead = true;
        healthCanvas.enabled = false;
        audioPlayer.PlayOneShot(explosionSound);

        Destroy(gameObject, 3f);
        gameMaster.enemyList.Remove(this.gameObject);
    }

    void MoveToPosition()
    {
        transform.position = Vector3.MoveTowards(transform.position, closestObj.transform.position, moveSpeed * Time.deltaTime);
    }

    void LookAtCabana()
    {
        direction = (cabanaPosition.position - transform.position).normalized;
        lookRotation = Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
    }

    void DeterminePosition()
    {
        List<GameObject> objectList = pointCollection.GetObjectList();

        float distance;
        float shortestDistance = Mathf.Infinity;

        for (int i = 0; i < objectList.Count; i++)
        {
            distance = Vector3.Distance(transform.position, objectList[i].transform.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                closestObj = objectList[i];
            }
        }
    }

    void healthBarFacePlayer()
    {
        healthCanvas.transform.LookAt(playerCamera.transform);
        healthCanvas.transform.Rotate(0, 180, 0);
    }
}
