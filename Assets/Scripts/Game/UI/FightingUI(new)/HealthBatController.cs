using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBatController : MonoBehaviour
{
    public Player player;

    Image bar;
    Image parentBar;

    int maxHealth;
    int currentHealth;
    
    void Start()
    {
        bar = GetComponent<Image>();
        parentBar = GetComponentInParent<Image>();
        maxHealth = player.characterData.MaxHealth;
        player.OnHealthChange += OnPlayerHealthChange;
    }

    private void Update()
    {
        if (bar.fillAmount > parentBar.fillAmount)
        {
            parentBar.fillAmount = bar.fillAmount;
        }
        else if (bar.fillAmount < parentBar.fillAmount)
        {
            parentBar.fillAmount -= 0.01f;
        }
    }

    private void OnPlayerHealthChange(Characters go, int health)
    {
        currentHealth = health;
        bar.fillAmount = 1.0f * currentHealth / maxHealth;
    }
}
