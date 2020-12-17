using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Balloon : MonoBehaviour
{
    float maxHealth = 30f;
    float currentHealth;
    float currentHealthPercent;

    public Cabana cabanaScript;
    public GameController gameMaster;
    public GameObject cannonballPrefab;
    public Transform cannonballSpawnPosition;

    public Camera playerCamera;
    public Canvas healthCanvas;
    public Image healthBar;

    float moveSpeed = 20f;
    float fireRate = 9f;
    float nextTimeToFire = 0f;
    Vector3 balloonPosition;

    public bool isDead = false;
    public ParticleSystem[] explosionArray;
    public AudioSource audioPlayer;
    public AudioClip explosionSound;

    // Start is called before the first frame update
    void Start()
    {
        audioPlayer = this.GetComponent<AudioSource>();

        currentHealth = maxHealth;
        currentHealthPercent = (float)currentHealth / (float)maxHealth;
        balloonPosition = new Vector3(13f, 33f, 15f);

        cabanaScript = GameObject.FindWithTag("Cabana").GetComponent<Cabana>();
        gameMaster = GameObject.FindWithTag("GameMaster").GetComponent<GameController>();
        playerCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
            MoveToPosition();

        if (transform.position == balloonPosition && !isDead)
        {
            if (Time.time >= nextTimeToFire)
            {
                nextTimeToFire = Time.time + fireRate;

                //Instantiate the cannonball
                Instantiate(cannonballPrefab, cannonballSpawnPosition.position, Quaternion.identity);
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
        transform.position = Vector3.MoveTowards(transform.position, balloonPosition, moveSpeed * Time.deltaTime);
    }

    void healthBarFacePlayer()
    {
        healthCanvas.transform.LookAt(playerCamera.transform);
        healthCanvas.transform.Rotate(0, 180, 0);
    }
}
