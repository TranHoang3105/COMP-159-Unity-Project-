using UnityEngine;


public class TouchingDirection : MonoBehaviour
{


    Rigidbody2D rb;
    Animator animator;
    CapsuleCollider2D touchingCol;
    RaycastHit2D[] groundHit = new RaycastHit2D[5];
    RaycastHit2D[] wallHit = new RaycastHit2D[5];
    RaycastHit2D[] ceilingHit = new RaycastHit2D[5];
    [SerializeField] public ContactFilter2D castFilter;
    public float groundDistance = 0.5f;
    public float wallDistance = 0.2f;
    public float ceilingDistance = 0.05f;

    [SerializeField] private bool _isGrounded;
    [SerializeField] private bool _isOnWall;
    [SerializeField] private bool _isOnCeiling;

    private Vector2 wallCheckDir => gameObject.transform.localScale.x > 0 ? Vector2.right : Vector2.left;
    public bool IsGrounded
    {
        get
        {
            return _isGrounded;
        }
        private set
        {
            _isGrounded = value;
            animator.SetBool(AnimationString.IsGrounded, value);
        }
    }

    public bool isOnWall { get {
            return _isOnWall;
    } private set
        {
            _isOnWall = value;
            animator.SetBool(AnimationString.isOnWall, value);
    } }

    public bool isOnCeiling
    {
        get
        {
            return _isOnCeiling;
        }
        private set
        {
            _isOnCeiling = value;
            animator.SetBool(AnimationString.isOnCeiling, value);
        }
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingCol = GetComponent<CapsuleCollider2D>();
    }




    void Update()
    {
        IsGrounded = touchingCol.Cast(Vector2.down, castFilter, groundHit, groundDistance) > 0;
        isOnWall = touchingCol.Cast(wallCheckDir, castFilter, wallHit, wallDistance) > 0;
        isOnCeiling = touchingCol.Cast(Vector2.up, castFilter, ceilingHit, ceilingDistance) > 0;
    }
    
}
