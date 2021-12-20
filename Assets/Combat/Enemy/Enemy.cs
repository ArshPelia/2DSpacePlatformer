using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int max_enemyhealth = 10;
    public int curr_enemyHealth;

    public GameObject deathEffect;
    public GameObject moonRock;
    public GameObject confirmKill;
    int playerHealth;

    private void Start()
    {
        curr_enemyHealth = max_enemyhealth;
        playerHealth = GameObject.Find("Player").GetComponent<Player>().currentHealth;
    }

    void Update()
    {
        if (GameObject.Find("Player").GetComponent<Player>().currentHealth < playerHealth)
        {
            Destroy(gameObject);
        }
        playerHealth = GameObject.Find("Player").GetComponent<Player>().currentHealth;
    }

    public void TakeDamage (int damage) {
        curr_enemyHealth -= damage;

        if (curr_enemyHealth <= 0) {
            Die();
        }
    }

    void Die() {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Instantiate(moonRock, transform.position, Quaternion.identity);
        Instantiate(confirmKill, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public void Hurt() {
       // player.health--;
    }
}
