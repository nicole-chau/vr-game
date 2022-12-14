using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    public Transform player;
    Global g;

    // Start is called before the first frame update
    void Start()
    {
        GameObject globalObj = GameObject.Find("GlobalObject");
        g = globalObj.GetComponent<Global>();

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = player.position - transform.position;

        // Debug.Log(direction.x);
        // Debug.Log(direction.z);

        if (Mathf.Abs(direction.x) <= 2 && Mathf.Abs(direction.z) <= 1) {
            g.health += 1;
            this.gameObject.SetActive(false);
            g.heartNum -= 1;
            Debug.Log(g.health);
        }
    }
}
