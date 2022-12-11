using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemyUI : MonoBehaviour
{
    public Enemy enemy;
    public Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        slider.maxValue = enemy.maxHunger;
        slider.value = enemy.hunger;
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = enemy.hunger;
    }
}
