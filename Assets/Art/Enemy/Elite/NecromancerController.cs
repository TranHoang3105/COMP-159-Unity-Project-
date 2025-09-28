using UnityEngine;

public class NecromancerController : MonoBehaviour
{
    // === 变量声明 ===
    public Animator animator;
    public Transform player;
    public float moveSpeed = 2f;
    public float meleeRange = 2f;
    public float rangeRange = 6f;

    void Update()
    {
        if (player == null) return; // 检查玩家是否绑定

        float distance = Vector2.Distance(transform.position, player.position);

        // 移动逻辑
        if (distance > meleeRange && distance <= rangeRange)
        {
            animator.SetBool("Walk", true);
            MoveTowardsPlayer();
        }
        else
        {
            animator.SetBool("Walk", false);
        }

        // 攻击逻辑
        if (distance <= meleeRange)
        {
            animator.SetTrigger("Attack");
        }
        else if (distance <= rangeRange)
        {
            animator.SetTrigger("Cast");
        }
    }

    void MoveTowardsPlayer()
    {
        Vector2 target = new Vector2(player.position.x, transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);

        // 面向玩家
        Vector3 s = transform.localScale;
        s.x = (player.position.x < transform.position.x) ? -Mathf.Abs(s.x) : Mathf.Abs(s.x);
        transform.localScale = s;
    }
}
