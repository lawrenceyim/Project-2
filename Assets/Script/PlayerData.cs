using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerData : MonoBehaviour 
{
    [SerializeField] private int money;
    [SerializeField] private int experience;
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;
    [SerializeField] private float armor;

    private void Start() {
        money = 0;
        experience = 0;
        currentHealth = maxHealth;
    }

    public void TakeKillReward(int experience, int money) {
        this.experience += experience;
        this.money += money;
    }

    public void IncreaseMoney(int amount) {
        money += amount;
    }

    public void DecreaseMoney(int amount) {
        money -= amount;
    }

    public bool CanAfford(int cost) {
        return money >= cost;
    }

    public int GetMoney() {
        return money;
    }

    public void IncreaseHealth(float amount) {
        currentHealth += amount;
        if (currentHealth > maxHealth) {
            currentHealth = maxHealth;
        }
    }

    public void DecreaseHealth(float amount) {
        currentHealth -= amount;
        if (currentHealth <= 0) {
            Debug.Log("You have died");
            Time.timeScale = 0f;
        }
    }
}
