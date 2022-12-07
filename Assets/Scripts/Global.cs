using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour
{
    public int health;
    public int foodCount;
    public int enemyCount;

    public GameObject enemy;
    public GameObject food;
    public GameObject player;
    public GameObject teleportPlane;

    // Start is called before the first frame update
    void Start()
    {
        health = 5;
        foodCount = 100;
        enemyCount = 5;

        // Vector3 playerPos = player.transform.position;

        for(int i = 0; i < foodCount; ++i) {
            Instantiate(food, RandomPosition(), Quaternion.identity);
        }

        for (int i = 0; i < enemyCount; ++i) {
            //Instantiate(enemy, RandomPosition(), Quaternion.identity);
        }

        // spawn teleport points
        float xStart = -30;
        float zStart = -30;
        float z = zStart;
        float x;
        for (int row = 0; row < 11; ++row) {
            x = xStart;
            for (int col = 0; col < 10; ++col) {
                Instantiate(teleportPlane, new Vector3(x, 0.01f, z), Quaternion.identity);
                x += 15;
            }
            z += 13;
        }       
    }

    Vector3 RandomPosition() {
        int x = Random.Range(-37, 37*3);
        int z = Random.Range(-37, 37*3);
        return new Vector3(x, 5f, z);
    }

    // Update is called once per frame
    void Update()
    {
        if (health == 0) {
            Debug.Log("game over");
        }
    }
}
