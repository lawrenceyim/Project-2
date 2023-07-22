using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private float speed;
    [SerializeField] private float damage;
    [SerializeField] private float attackCooldown;
    [SerializeField] private float nextAttackIn;
    [SerializeField] private int experience;
    [SerializeField] private int money;
    private GameObject player;
    private Rigidbody2D rb;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");    
        nextAttackIn = Time.time;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void TakeDamage(float damage) {
        health -= damage;
        if (health <= 0) {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            for (int i = 0; i < players.Length; i++) {
                players[i].GetComponent<PlayerData>().TakeKillReward(experience, money);
            }
            Destroy(gameObject);
        }
    }

    private void Move() {
        Vector3 direction = player.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle - 90;
        rb.MovePosition(transform.position + (direction * speed * Time.deltaTime));
    }
}
