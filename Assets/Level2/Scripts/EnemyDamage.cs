using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] protected int damage;

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
            collision.GetComponent<Player>().TakeDamage(damage);
    }
}