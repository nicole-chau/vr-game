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

    // Start is called before the first frame update
    void Start()
    {
        health = 5;
        foodCount = 40;
        enemyCount = 5;

        // Vector3 playerPos = player.transform.position;

        for(int i = 0; i < foodCount; ++i) {
            Instantiate(food, RandomPosition(), Quaternion.identity);
        }

        for (int i = 0; i < enemyCount; ++i) {
            Instantiate(enemy, RandomPosition(), Quaternion.identity);
        }       
    }

    Vector3 RandomPosition() {
        int x = Random.Range(-32, 53);
        int z = Random.Range(17, 55);
        return new Vector3(x, 0f, z);
    }

    // Update is called once per frame
    void Update()
    {
        if (health == 0) {
            Debug.Log("game over");
        }
    }
}
