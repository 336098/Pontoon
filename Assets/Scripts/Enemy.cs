using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    float health = 50f;

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

    GameObject closestObj;
    Vector3 direction;
    Quaternion lookRotation;

    // Start is called before the first frame update
    void Start()
    {
        pointCollection = GameObject.FindWithTag("ShootPositionCollection").GetComponent<ShootPointList>();
        cabanaScript = GameObject.FindWithTag("Cabana").GetComponent<Cabana>();
        cabanaPosition = GameObject.FindWithTag("Cabana").transform;
        gameMaster = GameObject.FindWithTag("GameMaster").GetComponent<GameController>();

        DeterminePosition();

        transform.LookAt(closestObj.transform);
    }

    // Update is called once per frame
    void Update()
    {
        MoveToPosition();

        if (transform.position == closestObj.transform.position)
        {
            LookAtCabana();
            if (Time.time >= nextTimeToFire)
            {
                nextTimeToFire = Time.time + fireRate;
                cannonFlash.Play();

                //Give damage to the cabana
                cabanaScript.TakeDamage(shootDamage);
                Debug.Log("The cabana now has " + cabanaScript.GetHealth() + " health left!");
            }
        }
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
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
}
