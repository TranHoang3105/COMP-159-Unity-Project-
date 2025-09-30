using UnityEngine;


public class TouchingDirection : MonoBehaviour
{


    Rigidbody2D rb;
    Animator animator;
    CapsuleCollider2D touchingCol;
    RaycastHit2D[] groundHit = new RaycastHit2D[5];
    RaycastHit2D[] wallHits = new RaycastHit2D[5];
    [SerializeField] public ContactFilter2D castFilter;
    public float groundDistance = 0.5f;
    public float wallDistance = 0.2f;
    [SerializeField] private bool _isGrounded;
    [SerializeField]private bool _isOnWall;
    private Vector2 wallCheckDirection => gameObject.transform.localScale.x> 0 ? Vector2.right : Vector2.left;

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

     public bool IsOnWall { get {
            return _isOnWall;
    } private set
        {
            _isOnWall = value;
            animator.SetBool(AnimationString.isOnWall, value);
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
        IsOnWall = touchingCol.Cast(wallCheckDirection, castFilter, wallHits, wallDistance) > 0;
    }
}
