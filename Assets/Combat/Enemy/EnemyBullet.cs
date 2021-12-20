using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    public GameObject shootingEnemy;
    public int damage = 5;
    //public GameObject impactEffect;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Player player = hitInfo.GetComponent<Player>();
        if (player != null)
        {
            player.TakeDamage(10);
            GameObject.Find("Player").GetComponent<Player>().rb.transform.position = GameObject.Find("Player").GetComponent<Player>().originalPos;
        }

        //Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
