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
    GameObject player;
    

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");    
        nextAttackIn = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);        
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
}
