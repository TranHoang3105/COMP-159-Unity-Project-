using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Damageable))]
public class Boss : MonoBehaviour
{
    public DetectionZone attackZone;
    
    private Animator animator;
    private Damageable damageable;

    public bool HasTarget
    {
        get { return attackZone.detectedColliders.Count > 0; }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        damageable = GetComponent<Damageable>();
    }

    void Update()
    {
        // 更新是否有目标的状态
        animator.SetBool(AnimationString.hasTarget, HasTarget);
        
        // 如果可以攻击且有目标，触发攻击
        if (HasTarget)
        {
            Attack();
        }
    }



    private void Attack()
    {
        // 触发攻击动画
        animator.SetTrigger(AnimationString.attack);
        
        // 这里可以添加攻击逻辑，比如造成伤害等
        // 例如：如果检测到玩家，对玩家造成伤害
    }

    // 重写OnHit方法，使其不会被击退
    public void OnHit(int damage, Vector2 knockback)
    {
        // 只承受伤害，不被击退
        // 可以在这里添加受伤特效或音效
        Debug.Log($"Enemy took {damage} damage, but didn't move!");
    }
}