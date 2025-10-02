using UnityEngine;
using UnityEngine.UI;

public class SimpleEnemyHealthBar2D : MonoBehaviour
{
    public Slider healthSlider;
    public Vector3 offset = new Vector3(0, 1f, 0);

    private Damageable damageable;
    public int MaxHealth;

    void Start()
    {
        damageable = GetComponentInParent<Damageable>();

        if (damageable != null && healthSlider != null)
        {
            healthSlider.value = CalculateSlider(damageable.Health, MaxHealth);
        }
    }

    void Update()
    {
        if (damageable != null)
        {
            UpdateHealthBarPosition();
            healthSlider.value = CalculateSlider(damageable.Health, MaxHealth);
            

            if (!damageable.IsAlive)
            {
                gameObject.SetActive(false);
            }
        }
    }

    void UpdateHealthBarPosition()
    {
        if (healthSlider != null)
        {
            transform.position = damageable.transform.position + offset;  
        }
    }

    private float CalculateSlider(int currentHealth, int maxHealth)
    {
        if (maxHealth <= 0) return 0f;
        return (float)currentHealth / maxHealth;
    }
}