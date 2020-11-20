using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cabana : MonoBehaviour
{
    float maxHealth = 100f;
    float currentHealth;
    float currentHealthPercent;

    public Canvas healthCanvas;
    public Camera playerCamera;
    public Image healthBar;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        currentHealthPercent = (float)currentHealth / (float)maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        healthBarFacePlayer();

        currentHealthPercent = (float)currentHealth / (float)maxHealth;
        healthBar.fillAmount = currentHealthPercent;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
    }

    public float GetHealth()
    {
        return currentHealth;
    }

    void healthBarFacePlayer()
    {
        healthCanvas.transform.LookAt(playerCamera.transform);
        healthCanvas.transform.Rotate(0, 180, 0);
    }
}
