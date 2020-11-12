using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cabana : MonoBehaviour
{
    float health = 50f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
    }

    public float GetHealth()
    {
        return health;
    }
}
