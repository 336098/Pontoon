using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Cabana cabanaScript;
    public Text gameOverText;
    public GameObject sailboatPrefab;
    public GameObject cabana;
    bool isGameOver;

    int numEnemiesToSpawn = 2;
    float spawnRadius = 250;
    Vector3 cabanaPosition;
    public List<GameObject> enemyList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        cabanaPosition = cabana.transform.position;
        isGameOver = false;
        gameOverText.text = " ";

        SpawnSailboats();
    }

    // Update is called once per frame
    void Update()
    {
        CheckForGameOver();

        //If there are no more enemies left, spawn more
        if (enemyList.Count == 0)
        {
            SpawnSailboats();
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

    void SpawnSailboats()
    {
        for (int i = 0; i < numEnemiesToSpawn; i++)
        {
            float angle = Random.Range(0f, 360f);
            float distance = 250f;

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
    }
}
