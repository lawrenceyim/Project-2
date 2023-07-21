using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerData : MonoBehaviour 
{
    [SerializeField] private int money;
    [SerializeField] private int experience;

    public void TakeKillReward(int experience, int money) {
        this.experience += experience;
        this.money += money;
    }

}
