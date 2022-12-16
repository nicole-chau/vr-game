using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    Global g;

    public Text healthText;
    // Start is called before the first frame update
    void Start()
    {
        // GameObject globalObj = GameObject.Find("GlobalObject");
        // g = globalObj.GetComponent<Global>();

        healthText.text = "Score: " + PlayerPrefs.GetInt("score").ToString();
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(g.score);
        // healthText.text = "Score: " + g.score.ToString();
    }
}
