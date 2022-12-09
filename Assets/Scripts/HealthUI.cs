using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    Global globalObj;
    Text healthText;
    // Start is called before the first frame update
    public Slider slider;

    void Start()
    {
        // healthText = gameObject.GetComponent<Text>();
        GameObject g = GameObject.Find("GlobalObject");
        globalObj = g.GetComponent<Global>();
        slider.value = globalObj.health;

    }

    // Update is called once per frame
    void Update()
    {
        // healthText.text = "Health: " + globalObj.health.ToString();
        slider.value = globalObj.health;
    }
}
