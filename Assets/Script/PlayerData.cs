using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerData : MonoBehaviour 
{
    [SerializeField] private int money;
    [SerializeField] private int experience;

    private void Start() {
        money = 0;
        experience = 0;
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

}
