using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public Slider healthSlider;
    public Vector3 offset = new Vector3(0, 1f, 0);

    private Damageable damageable;
    private Transform enemyTransform; 
    public int MaxHealth;

    void Start()
    {
  
        damageable = GetComponentInParent<Damageable>();
        enemyTransform = damageable.transform;
        
        transform.SetParent(null);

        if (damageable != null && healthSlider != null)
        {
            healthSlider.value = CalculateSlider(damageable.Health, MaxHealth);
        }
    }

    void Update()
    {
        if (damageable != null && enemyTransform != null)
        {
            UpdateHealthBarPosition();

            healthSlider.value = CalculateSlider(damageable.Health, MaxHealth);

            if (!damageable.IsAlive)
            {
                Destroy(gameObject); 
            }
        }
    }

    void UpdateHealthBarPosition()
    {
        if (healthSlider != null && enemyTransform != null)
        {
            transform.position = enemyTransform.position + offset;
            
            transform.rotation = Quaternion.identity;
        }
    }

    private float CalculateSlider(int currentHealth, int maxHealth)
    {
        if (maxHealth <= 0) return 0f;
        return (float)currentHealth / maxHealth;
    }

    void OnDestroy()
    {
        //Debug.Log("Health bar destroyed");
    }
}