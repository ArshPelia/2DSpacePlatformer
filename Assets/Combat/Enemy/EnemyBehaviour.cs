using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{

    [SerializeField] float moveSpeed = 1f;

    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start(){
        rb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update(){
        if (IsFacingRight()){
            // Move Right
            rb.velocity = new Vector2(moveSpeed, 0f);
        } else {
            // move left
            rb.velocity = new Vector2(-moveSpeed, 0f);
        }
    }

    private bool IsFacingRight() {
        return transform.localScale.x > Mathf.Epsilon;
    }

    private void OnTriggerExit2D(Collider2D collision){
        //turn
        transform.localScale = new Vector2(-(Mathf.Sign(transform.localScale.x)*2) , transform.localScale.y);
    }
}
