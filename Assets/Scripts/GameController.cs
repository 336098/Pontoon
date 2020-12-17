using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Cabana cabanaScript;
    public GameObject gunScript;
    public Text gameOverText;
    public Text ammoText;
    public Text waveText;
    public Image waterTint;

    public AudioSource audioPlayer;
    public AudioClip WaveSound1;
    public AudioClip WaveSound2;

    public GameObject sailboatPrefab;
    public GameObject pirateshipPrefab;
    public GameObject battleshipPrefab;
    public GameObject balloonPrefab;
    public GameObject cabana;
    public GameObject player;
    bool isGameOver;

    int waveNum;
    int numEnemiesToSpawn = 6;
    float spawnRadius = 250;
    Vector3 cabanaPosition;
    public List<GameObject> enemyList = new List<GameObject>();
    //Island Y position = -1.25

    // Start is called before the first frame update
    void Start()
    {
        audioPlayer = this.GetComponent<AudioSource>();

        waveNum = 1;
        waterTint.enabled = false;
        cabanaPosition = cabana.transform.position;
        isGameOver = false;
        gameOverText.text = " ";

        PlayWaveSound();
        SpawnSailboats();
    }

    // Update is called once per frame
    void Update()
    {
        CheckForGameOver();

        ammoText.text = "Ammo: " + gunScript.GetComponent<GunScript>().currentAmmo.ToString() + " / 10";

        //Tint the camera blue if the player is under the water line
        if (player.transform.position.y <= -4.1f)
            waterTint.enabled = true;
        else
            waterTint.enabled = false;

        //If there are no more enemies left, spawn more
        if (enemyList.Count == 0)
        {
            SpawnSailboats();
            PlayWaveSound();
            waveNum++;
            waveText.text = "Wave " + waveNum;
        }

        if (isGameOver == true && Input.GetButtonDown("Jump"))
        {
            isGameOver = false;
            gameOverText.text = " ";
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    void CheckForGameOver()
    {
        if (cabanaScript.GetHealth() <= 0)
        {
            isGameOver = true;
            //Update the screen's text
            gameOverText.text = "Game Over Man!\n\n Press \"Space\" to Restart";
        }
    }

    public bool GetGameState()
    {
        return isGameOver;
    }

    void PlayWaveSound()
    {
        float randNum = Random.Range(0f, 1f);
        if (randNum < 0.5f)
        {
            audioPlayer.PlayOneShot(WaveSound1);
        }
        else
        {
            audioPlayer.PlayOneShot(WaveSound2);
        }
    }

    void SpawnSailboats()
    {
        for (int i = 0; i < numEnemiesToSpawn; i++)
        {
            float angle = Random.Range(0f, 360f);
            float distance = 250f;

            float spawnRoll = Random.Range(1f, 100f);
            if (spawnRoll <= 40) //40%
            {
                GameObject spawnedObj = (GameObject)Instantiate(sailboatPrefab, cabana.transform.position, Quaternion.identity);
                spawnedObj.transform.Rotate(new Vector3(0, angle, 0));
                spawnedObj.transform.Translate(new Vector3(distance, 0, distance));

                //Reset the Y position for whatever the boat's height in the water will be
                //Pirateship = -4.53
                //Battleship = -2.5
                //Balloon = 33
                spawnedObj.transform.position = new Vector3(spawnedObj.transform.position.x, -2.7f, spawnedObj.transform.position.z);
                enemyList.Add(spawnedObj);
            }
            else if (spawnRoll <= 70) //30%
            {
                GameObject spawnedObj = (GameObject)Instantiate(pirateshipPrefab, cabana.transform.position, Quaternion.identity);
                spawnedObj.transform.Rotate(new Vector3(0, angle, 0));
                spawnedObj.transform.Translate(new Vector3(distance, 0, distance));

                //Reset the Y position for whatever the boat's height in the water will be
                spawnedObj.transform.position = new Vector3(spawnedObj.transform.position.x, -4.53f, spawnedObj.transform.position.z);
                enemyList.Add(spawnedObj);
            }
            else if (spawnRoll <= 95) //25%
            {
                GameObject spawnedObj = (GameObject)Instantiate(balloonPrefab, cabana.transform.position, Quaternion.identity);
                spawnedObj.transform.Rotate(new Vector3(0, angle, 0));
                spawnedObj.transform.Translate(new Vector3(distance, 0, distance));

                //Reset the Y position for whatever the boat's height in the water will be
                spawnedObj.transform.position = new Vector3(spawnedObj.transform.position.x, 33f, spawnedObj.transform.position.z);
                enemyList.Add(spawnedObj);
            }
            else //5%
            {
                GameObject spawnedObj = (GameObject)Instantiate(battleshipPrefab, cabana.transform.position, Quaternion.identity);
                spawnedObj.transform.Rotate(new Vector3(0, angle, 0));
                spawnedObj.transform.Translate(new Vector3(distance, 0, distance));

                //Reset the Y position for whatever the boat's height in the water will be
                spawnedObj.transform.position = new Vector3(spawnedObj.transform.position.x, -2.5f, spawnedObj.transform.position.z);
                enemyList.Add(spawnedObj);
            }
        }
    }
}
