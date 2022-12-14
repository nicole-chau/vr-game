using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliceListener : MonoBehaviour
{
    bool isTouched;
    public GameObject foodHalf;
    public GameObject hand;
    Global g;

    void Start()
    {
        GameObject globalObj = GameObject.Find("GlobalObject");
        g = globalObj.GetComponent<Global>();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Food")) {
            isTouched = true;

        }

        if (collider.CompareTag("Heart")) {
            g.health += 2;
            if (g.health > 10) g.health = 10;
            collider.gameObject.SetActive(false);
            g.heartNum -= 1;
            Debug.Log(g.health);

        }
    }

    private void OnTriggerExit(Collider collider) {
        if (isTouched && collider.CompareTag("Food")) {
            GameObject obj = collider.gameObject;

            Vector3 pos = obj.transform.position;
            Quaternion rot = hand.transform.rotation;

            obj.SetActive(false);

            Instantiate(foodHalf, pos+ new Vector3(0,0,-.2f), rot);
            Instantiate(foodHalf, pos + new Vector3(0,0,.2f), rot);
        }
    }
}
