using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour{
    public Transform player;
    public float moveSpeed = 1f;
    public float hunger;
    public float maxHunger;

    private Rigidbody rb;
    private Vector3 direction;

    public AudioSource footstep;

    public bool hitPlayer;

    public float timer;
    public float attack_timer;
    public float wander_timer;
    public float fullPeriod;

    float attackPeriod;
    float wanderPeriod; // the time till enemy changes direction

    Global g;

    // Start is called before the first frame update
    void Start() {
        GameObject globalObj = GameObject.Find("GlobalObject");
        g = globalObj.GetComponent<Global>();

        maxHunger = Random.Range(1,2);
        hunger = 0f;

        rb = this.GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        hitPlayer = false;

        footstep = GetComponent<AudioSource>();
        footstep.loop = true;
        footstep.Play();

        timer = 0;
        attack_timer = 0;
        wander_timer = 0;
        fullPeriod = Random.Range(20, 300);

        // attack again every 3 seconds
        attackPeriod = 3f;
        wanderPeriod = (float)Random.Range(5, 10);
        
    }

    // Update is called once per frame
    void Update() {
        
        timer += Time.deltaTime;
        attack_timer += Time.deltaTime;
        wander_timer += Time.deltaTime;
        if (!IsFull()) {
            direction = player.position - transform.position;

            // chase player
            //transform.LookAt(player);
            //direction = player.position - transform.position;

            // attack player
            if (Mathf.Abs(direction.x) <= 1 && Mathf.Abs(direction.z) <= 1) {
                if (!hitPlayer) {
                    hitPlayer = true;
                    Debug.Log("hit player");
                    
                    g.health--;
                    Debug.Log(g.health);
                } else if (attack_timer > attackPeriod) {
                    // attack player again after 3 seconds
                    attack_timer = 0;
                    hitPlayer = false;
                }
            }

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            rb.rotation = Quaternion.AngleAxis(angle, Vector3.up);
            direction.Normalize();

        } else {
            // run away from player
            if (wander_timer > wanderPeriod) {
                direction = new Vector3((float)Random.Range(-10,10), (float)0.0, (float)Random.Range(-10,10));
                direction.Normalize();
                wander_timer = 0;
                if (transform.position.x > 33 || transform.position.x < -33 || transform.position.z > 33 || transform.position.z < -33) {
                    direction = -direction;
                }
                // if (transform.position.z > 33 || transform.position.z < -33) {
                //     direction.z = -direction.z;
                // }
            }
            

            // become hunger again after a random period of time
            if (timer > fullPeriod) {
                timer = 0;
                Debug.Log("hungry again");
                hunger = maxHunger;
                hitPlayer = false;
            }
        }
        

    }

    void FixedUpdate() {
        rb.MovePosition(transform.position + (direction * moveSpeed * Time.deltaTime));
    }

    void OnCollisionEnter(Collision collision) {
        Debug.Log("collision enter");
        Collider collider = collision.collider;
        if ((collider.CompareTag("Food") || collider.CompareTag("FoodHalf")) && hunger > 0) {
            if (collider.CompareTag("Food")) {
                hunger-=1f;
                g.foodCount--;
            } else if (collider.CompareTag("FoodHalf")) {
                hunger-=.5f;
                g.foodCount-=0.5f;
            }
            
            GameObject ob = collision.gameObject;
            ob.SetActive(false);
            Debug.Log(hunger);
        } 
    }

    bool IsFull() {
        return hunger <= 0;
    }
}