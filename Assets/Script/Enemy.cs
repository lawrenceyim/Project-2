using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private float speed;
    [SerializeField] private float damage;
    [SerializeField] private float attackCooldown;
    [SerializeField] private int experience;
    [SerializeField] private int money;
    [SerializeField] private float attackRange;
    [SerializeField] Sprite attackSprite;
    [SerializeField] Sprite moveSprite;
    public Animator animator;
    private float lastAttack;

    private GameObject player;
    private Rigidbody2D rb;

    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");    
        rb = GetComponent<Rigidbody2D>();
        lastAttack = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Attack();
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
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle - 90;
        rb.MovePosition(transform.position + (direction * speed * Time.deltaTime));
    }

    private void Attack() {
        if (Time.time - lastAttack < attackCooldown) {
            return;
        }
        if (Vector2.Distance(player.transform.position, transform.position) <= attackRange) {
            lastAttack = Time.time;
            player.GetComponent<PlayerData>().DecreaseHealth(damage);
            animator.SetBool("IsAttacking", true);
        }
    }
}
