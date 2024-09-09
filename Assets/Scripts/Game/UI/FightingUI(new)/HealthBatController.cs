using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBatController : MonoBehaviour
{
    public Player player => GameObject.Find("Player").GetComponent<Player>();

    [SerializeField]
    Image bar;
    [SerializeField]
    Image parentBar;

    int maxHealth;
    int currentHealth;

    const float delayTime = 0.5f;
    float timer = 0;
    
    void Start()
    {
        // bar = GetComponent<Image>();
        // parentBar = GetComponentInParent<Image>();
        maxHealth = player.characterData.MaxHealth;
        player.OnHealthChange += OnPlayerHealthChange;
    }

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        if (bar.fillAmount > parentBar.fillAmount)
        {
            parentBar.fillAmount = bar.fillAmount;
        }
        else if (bar.fillAmount < parentBar.fillAmount && timer <= 0)
        {
            parentBar.fillAmount -= 0.005f;
        }
        
    }

    private void OnPlayerHealthChange(Characters go, int health)
    {
        currentHealth = health;
        bar.fillAmount = 1.0f * currentHealth / maxHealth;
        timer = delayTime;
    }
}
