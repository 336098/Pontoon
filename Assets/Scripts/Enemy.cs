using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    float health = 50f;

    public ShootPointList pointCollection;
    public Transform cabana;
    float moveSpeed = 5f;
    float rotationSpeed = 3f;

    GameObject closestObj;
    Vector3 direction;
    Quaternion lookRotation;

    // Start is called before the first frame update
    void Start()
    {
        DeterminePosition();

        transform.LookAt(closestObj.transform);
        //lookRotation = Quaternion.LookRotation(closestObj.transform.position);
        //transform.rotation = lookRotation;
    }

    // Update is called once per frame
    void Update()
    {
        MoveToPosition();

        if (transform.position == closestObj.transform.position)
        {
            LookAtCabana();
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
    }

    void MoveToPosition()
    {
        transform.position = Vector3.MoveTowards(transform.position, closestObj.transform.position, moveSpeed * Time.deltaTime);
    }

    void LookAtCabana()
    {
        direction = (cabana.position - transform.position).normalized;
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
