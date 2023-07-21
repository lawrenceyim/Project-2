using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyDisplay : MonoBehaviour
{
    private TextMeshPro text;

    void Start()
    {
        text = GetComponent<TextMeshPro>();
    }
    
    public void SetMoney(int amount) {
        text.text = $"${amount.ToString()}";
    }
    
}
