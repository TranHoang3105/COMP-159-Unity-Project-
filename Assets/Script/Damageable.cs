using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement; // <-- needed for scene loading

public class Damageable : MonoBehaviour
{
    public UnityEvent<int, Vector2> damagableHit;
    Animator animator;

    [SerializeField]
    private int _maxHealth = 100;

    public int MaxHealth
    {
        get { return _maxHealth; }
        set { _maxHealth = value; }
    }

    [SerializeField]
    private int _health = 100;

    public int Health
    {
        get { return _health; }
        set
        {
            _health = value;

            if (_health <= 0 && IsAlive) // only trigger once
            {
                _health = 0;
                IsAlive = false;
                OnDeath(); // <- call the new method
            }
        }
    }

    [SerializeField]
    private bool _isAlive = true;

    [SerializeField]
    private bool isInvincible = false;

    private float timeSinceHit = 0;
    public float invincibilityTime = 0.25f;

    public bool IsAlive
    {
        get { return _isAlive; }
        set
        {
            _isAlive = value;
            animator.SetBool(AnimationString.isAlive, value);
        }
    }

    public bool LockVelocity
    {
        get { return animator.GetBool(AnimationString.lockVelocity); }
        set { animator.SetBool(AnimationString.lockVelocity, value); }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isInvincible)
        {
            if (timeSinceHit > invincibilityTime)
            {
                isInvincible = false;
                timeSinceHit = 0;
            }

            timeSinceHit += Time.deltaTime;
        }
    }

    public bool Hit(int damage, Vector2 knockback)
    {
        if (IsAlive && !isInvincible)
        {
            Health -= damage;
            isInvincible = true;

            animator.SetTrigger(AnimationString.hit);
            LockVelocity = true;
            damagableHit?.Invoke(damage, knockback);

            return true;
        }
        return false;
    }

   
    private void OnDeath()
    {
        if (CompareTag("Player"))
        {
            SceneManager.LoadScene("GameOver");
        }
        else if (CompareTag("Boss"))
        {
            SceneManager.LoadScene("VictoryScreen");
        }
    }
}
