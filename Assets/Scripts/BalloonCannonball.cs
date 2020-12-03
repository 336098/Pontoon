using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonCannonball : MonoBehaviour
{
    public Cabana cabanaScript;
    float damage = 20f;

    // Start is called before the first frame update
    void Start()
    {
        cabanaScript = GameObject.FindWithTag("Cabana").GetComponent<Cabana>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Cabana")
        {
            cabanaScript.TakeDamage(damage);
            Destroy(this.gameObject);
        }
    }
}
