using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour{
    public Transform player;
    public float moveSpeed = 1f;
    public float hunger;

    private Rigidbody rb;
    private Vector3 direction;

    public AudioSource footstep;

    public bool hitPlayer;

    public float timer;
    public float fullPeriod;

    // Start is called before the first frame update
    void Start() {
        hunger = 0f;

        rb = this.GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        hitPlayer = false;

        footstep = GetComponent<AudioSource>();
        footstep.loop = true;
        footstep.Play();

        timer = 0;
        fullPeriod = Random.Range(20, 60);

        //AudioSource.PlayClipAtPoint(this.footstep, this.gameObject.transform.position);
        
    }

    // Update is called once per frame
    void Update() {
        direction = player.position - transform.position;
        if (!IsFull()) {

            // chase player
            //transform.LookAt(player);
            //direction = player.position - transform.position;

            // attack player
            if (Mathf.Abs(direction.x) <= 1 && Mathf.Abs(direction.z) <= 1) {
                if (!hitPlayer) {
                    hitPlayer = true;
                    Debug.Log("hit player");
                    GameObject globalObj = GameObject.Find("GlobalObject");
                    Global g = globalObj.GetComponent<Global>();
                    g.health--;
                    Debug.Log(g.health);
                }
            }

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            rb.rotation = Quaternion.AngleAxis(angle, Vector3.up);
            direction.Normalize();

        } else {
            direction.Normalize();
            direction = -direction;

            timer += Time.deltaTime;
            if (timer > fullPeriod) {
                timer = 0;
                Debug.Log("hungry again");
                hunger = Random.Range(1,3);
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
            } else if (collider.CompareTag("FoodHalf")) {
                hunger-=.5f;
            }
            
            GameObject ob = collision.gameObject;
            //collision.gameObject.GetComponent<Interactable>().colliders.Clear();
            ob.SetActive(false);
            //Destroy(collision.gameObject);
            Debug.Log(hunger);
        } 
        
        // else if (collider.CompareTag("Player")) {
        //     Debug.Log("hit player");
        //     GameObject globalObj = GameObject.Find("GlobalObject");
        //     Global g = globalObj.GetComponent<Global>();
        //     g.health--;
        //     Debug.Log(g.health);
        // }
    }

    bool IsFull() {
        return hunger <= 0;
    }
}