using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider;
    public bool smoothTransition = true; // 是否平滑过渡
    public float transitionSpeed = 5f;   // 平滑过渡速度
    
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
                // 初始设置
                targetValue = CalculateSlider(playerDamageable.Health, playerDamageable.MaxHealth);
                healthSlider.value = targetValue;
                
                // 可选：监听伤害事件（更高效）
                // playerDamageable.healthChanged.AddListener(OnHealthChanged);
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
            // 计算目标血量值
            targetValue = CalculateSlider(playerDamageable.Health, playerDamageable.MaxHealth);
            
            // 更新血条显示
            if (smoothTransition)
            {
                // 平滑过渡
                healthSlider.value = Mathf.Lerp(healthSlider.value, targetValue, Time.deltaTime * transitionSpeed);
            }
            else
            {
                // 直接设置
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