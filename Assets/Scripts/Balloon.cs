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

    // Start is called before the first frame update
    void Start()
    {
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
        MoveToPosition();

        if (transform.position == balloonPosition)
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
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
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
