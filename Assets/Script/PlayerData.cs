using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerData : MonoBehaviour 
{
    [SerializeField] private int money;
    [SerializeField] private int experience;
    private MoneyDisplay moneyDisplay;

    private void Start() {
        money = 0;
        experience = 0;
        moneyDisplay = GameObject.FindGameObjectWithTag("Money Indicator").GetComponent<MoneyDisplay>();
    }

    public void TakeKillReward(int experience, int money) {
        this.experience += experience;
        this.money += money;
        moneyDisplay.SetMoney(this.money);
    }

    public void IncreaseMoney(int amount) {
        money += amount;
        moneyDisplay.SetMoney(money);
    }

    public void DecreaseMoney(int amount) {
        money -= amount;
        moneyDisplay.SetMoney(money);
    }

    public bool CanAfford(int cost) {
        return money >= cost;
    }

    public int GetMoney() {
        return money;
    }

}
