using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float damage;
    [SerializeField] private float range;
    private Vector2 origin;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        origin = transform.position;
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(transform.up * speed, ForceMode2D.Impulse);
    }

    void FixedUpdate()
    {
        if (Vector2.Distance(origin, transform.position) > range) {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Enemy")) {
            other.gameObject.GetComponent<Enemy>().TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
