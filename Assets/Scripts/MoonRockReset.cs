using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonRockReset : MonoBehaviour
{
    int playerHealth;
    // Start is called before the first frame update
    void Start()
    {
        playerHealth = GameObject.Find("Player").GetComponent<Player>().currentHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Player").GetComponent<Player>().currentHealth < playerHealth)
        {
            Destroy(gameObject);
        }
        playerHealth = GameObject.Find("Player").GetComponent<Player>().currentHealth;
    }
}
