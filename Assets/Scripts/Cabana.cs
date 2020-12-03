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
<<<<<<< Updated upstream
        return health;
=======
        return currentHealth;
    }

    public float GetCabanaPositionX()
    {
        return this.transform.position.x;
    }

    public float GetCabanaPositionZ()
    {
        return this.transform.position.z;
    }

    void healthBarFacePlayer()
    {
        healthCanvas.transform.LookAt(playerCamera.transform);
        healthCanvas.transform.Rotate(0, 180, 0);
>>>>>>> Stashed changes
    }
}
