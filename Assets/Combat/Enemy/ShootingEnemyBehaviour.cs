using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemyBehaviour : MonoBehaviour
{

    [SerializeField] float moveSpeed = 1f;

    public GameObject bullet;
    Rigidbody2D rb;
    private SpriteRenderer viewRender;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        viewRender = transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        viewRender.flipX = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Player").GetComponent<Player>().GetPosition().x - transform.position.x > 0) {
            viewRender.flipX = false;
        }
        else {
            viewRender.flipX = true;
        }
        if (IsFacingRight())
        {
            // Move Right
            rb.velocity = new Vector2(moveSpeed, 0f);
        }
        else
        {
            // move left
            rb.velocity = new Vector2(-moveSpeed, 0f);
        }
    }

    private bool IsFacingRight()
    {
        return transform.localScale.x > Mathf.Epsilon;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //turn
        if (collision.transform.tag != "enemyBullet")
        {
            transform.localScale = new Vector2(-(Mathf.Sign(transform.localScale.x) * 2), transform.localScale.y);
        }
    }
}
