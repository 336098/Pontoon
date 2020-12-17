using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonCannonball : MonoBehaviour
{
    public Cabana cabanaScript;
    float damage = 20f;

    public ParticleSystem[] explosionArray;
    public Renderer rend;
    public AudioSource audioPlayer;
    public AudioClip explosionSound;

    // Start is called before the first frame update
    void Start()
    {
        audioPlayer = this.GetComponent<AudioSource>();
        rend = this.GetComponent<Renderer>();

        cabanaScript = GameObject.FindWithTag("Cabana").GetComponent<Cabana>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Cabana")
        {
            cabanaScript.TakeDamage(damage);

            foreach (ParticleSystem explosion in explosionArray)
            {
                explosion.Play();
            }
            audioPlayer.PlayOneShot(explosionSound);
            rend.enabled = false;

            Destroy(gameObject, 3f);
        }
    }
}
