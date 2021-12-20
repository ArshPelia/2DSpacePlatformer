using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    float lastTimeShot;
    public Transform firePoint;

    public GameObject bulletPrefab;
    Vector2 screenBounds;
    Vector2 screenOrigo;
    // Start is called before the first frame update
    void Start()
    {
        lastTimeShot = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        screenOrigo = Camera.main.ScreenToWorldPoint(Vector2.zero);
        if (Time.time - lastTimeShot > 2f && !(transform.position.x > screenBounds.x || transform.position.x < screenOrigo.x) &&
            Mathf.Sign(transform.localScale.x) == Mathf.Sign(GameObject.Find("Player").GetComponent<Player>().GetPosition().x - transform.position.x)) {
            Shoot();
            lastTimeShot = Time.time;
        }
    }

    void Shoot()
    {
        var bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }
}
