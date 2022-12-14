using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour
{
    public int health;
    public float foodCount;
    public int enemyCount;

    public GameObject enemy;
    public GameObject food;
    public GameObject player;
    public GameObject teleportPlane;
    public GameObject fence;
    public GameObject heart;

    public float heartTimer;
    public float heartSpawnPeriod;
    public int heartNum;
    public int score;

    // Start is called before the first frame update
    void Start()
    {
        health = 10;
        foodCount = 60f;
        enemyCount = 8;
        score = 0;

        heartSpawnPeriod = 15;

        for(int i = 0; i < foodCount; ++i) {
            Instantiate(food, RandomPosition(), Quaternion.identity);
        }

        for (int i = 0; i < enemyCount; ++i) {
            Instantiate(enemy, RandomPosition(), Quaternion.Euler(0f, 0f, 0f));
        }

        // spawn teleport points
        // float xStart = -30;
        // float zStart = -30;
        // float z = zStart;
        // float x;
        // for (int row = 0; row < 11; ++row) {
        //     x = xStart;
        //     for (int col = 0; col < 10; ++col) {
                //Instantiate(teleportPlane, new Vector3(x, 0.01f, z), Quaternion.identity);
        //         x += 15;
        //     }
        //     z += 13;
        // }

        float offset = 0;
        for (int i = 0; i < 38; ++i) {
            Instantiate(fence, new Vector3(-20f + offset, 0.01f, -25), Quaternion.Euler(0f, 0f, 0f));
            Instantiate(fence, new Vector3(-20f + offset, 0.01f, 100), Quaternion.Euler(0f, 0f, 0f));
            Instantiate(fence, new Vector3(-20f, 0.01f, -25 + offset), Quaternion.Euler(0f, 90f, 0f));
            Instantiate(fence, new Vector3(100f, 0.01f, -25 + offset), Quaternion.Euler(0f, 90f, 0f));
            offset += 3.5f;
        }       
    }

    Vector3 RandomPosition() {
        int x = Random.Range(-15, 90);
        int z = Random.Range(-15, 90);
        return new Vector3(x, 1f, z);
    }


    // Update is called once per frame
    void Update()
    {
        if (health == 0) {
            // Debug.Log("game over");
            Application.LoadLevel("GameOver"); 
        }

        // spawn more food
        if (foodCount < 50) {
            for (int i = 0; i < 10; ++i) {
                // Debug.Log("spawned food");
                Instantiate(food, RandomPosition(), Quaternion.identity);
                foodCount++;
            }
        }

        heartTimer += Time.deltaTime;

        if (heartNum <= 5) {
            Debug.Log("spawning food");
            heartTimer = 0;
            Instantiate(heart, RandomPosition(), Quaternion.Euler(-90, 0, 0));
            heartNum += 1;
        }
    }
}
