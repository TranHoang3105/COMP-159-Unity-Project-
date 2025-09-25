using UnityEngine;


public class TouchingDirection : MonoBehaviour
{


    Rigidbody2D rb;
    Animator animator;
    CapsuleCollider2D touchingCol;
    RaycastHit2D[] groundHit = new RaycastHit2D[5];
    public ContactFilter2D castFilter;
    public float groundDistance = 0.5f;
    [SerializeField]private bool _isGrounded;
    public bool IsGrounded { get {
            return _isGrounded;
    } private set
        {
            _isGrounded = value;
            animator.SetBool(AnimationString.IsGrounded, value);
    } }


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingCol = GetComponent<CapsuleCollider2D>();
    }




    void Update()
    {
        IsGrounded = touchingCol.Cast(Vector2.down, castFilter, groundHit, groundDistance) > 0;
    }
}
