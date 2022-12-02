using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour{
    public Transform player;
    public float moveSpeed = 1f;
    public float hunger = 10f;

    private Rigidbody rb;
    private Vector3 direction;

    public bool hitPlayer;

    // Start is called before the first frame update
    void Start() {
        rb = this.GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        hitPlayer = false;

        StartCoroutine(Disappear());
    }

    // Update is called once per frame
    void Update() {
        direction = player.position - transform.position;

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

        if (IsFull()) {
            direction = -direction;
        }


    }

    void FixedUpdate() {
        rb.MovePosition(transform.position + (direction * moveSpeed * Time.deltaTime));
    }

    void OnCollisionEnter(Collision collision) {
        Debug.Log("collision enter");
        Collider collider = collision.collider;
        if (collider.CompareTag("Food")) {
            hunger-=2f;
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
        return hunger == 0;
    }

    IEnumerator Disappear() {
        yield return new WaitForSeconds(5f);

        if (IsFull()) {
            Destroy(this);
        }
    }
}