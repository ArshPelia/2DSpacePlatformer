using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    public int damage = 5;

    private Player player;
    //public GameObject impactEffect;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    void Update(){
         player = GameObject.Find("Player").GetComponent<Player>();
        damage = player.currentDamage;
    }

    void OnTriggerEnter2D (Collider2D hitInfo) {
        
        Enemy enemy = hitInfo.GetComponent<Enemy>();
        if (enemy != null) {
            enemy.TakeDamage(damage);
        }

        BossHealth boss = hitInfo.GetComponent<BossHealth>();
        if (boss != null)
        {
            boss.TakeDamage(damage);
        }

        BirdHealth bird = hitInfo.GetComponent<BirdHealth>();
        if (bird != null)
        {
            bird.TakeDamage(damage);
        }

        //Instantiate(impactEffect, transform.position, transform.rotation);

        Destroy(gameObject);
    }
}
