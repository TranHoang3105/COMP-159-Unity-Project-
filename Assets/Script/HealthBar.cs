using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider;
    public bool smoothTransition = true; 
    public float transitionSpeed = 5f;   
    
    private Damageable playerDamageable;
    private float targetValue;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerDamageable = player.GetComponent<Damageable>();
            if (playerDamageable != null)
            {
                
                targetValue = CalculateSlider(playerDamageable.Health, playerDamageable.MaxHealth);
                healthSlider.value = targetValue;
                
                
            }
            else
            {
                Debug.LogError("Player GameObject does not have Damageable component!");
            }
        }
        else
        {
            Debug.LogError("No GameObject with tag 'Player' found!");
        }
    }

    void Update()
    {
        if (playerDamageable != null)
        {

            targetValue = CalculateSlider(playerDamageable.Health, playerDamageable.MaxHealth);
            
           
            if (smoothTransition)
            {
            
                healthSlider.value = Mathf.Lerp(healthSlider.value, targetValue, Time.deltaTime * transitionSpeed);
            }
            else
            {
              
                healthSlider.value = targetValue;
            }
        }
    }

    private float CalculateSlider(int currentHealth, int maxHealth)
    {
        if (maxHealth <= 0) return 0f;
        return (float)currentHealth / maxHealth;
    }

}