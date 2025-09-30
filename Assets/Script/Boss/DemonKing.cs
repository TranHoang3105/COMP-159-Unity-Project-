using UnityEngine;

public class DemonKing : MonoBehaviour
{
    Rigidbody2D rb;
    
    public float moveSpeed = 3f;

    public enum WalkableDirection{Right, Left}
    private WalkableDirection _walkDirection;
    TouchingDirection touchingDirection;
    // Stores the current walk direction as a Vector2
    public Vector2 WalkDirectionVector;
    public WalkableDirection WalkDirection
    {
        get { return _walkDirection; }
        set
        {
            if (_walkDirection != value)
            {
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);
                if(value == WalkableDirection.Right)
                {
                    WalkDirectionVector = Vector2.right;
                }
                else if (value == WalkableDirection.Left)
                {
                    WalkDirectionVector = Vector2.left;
                }
   
            }
            _walkDirection = value;
        }
    }

    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        touchingDirection = GetComponent<TouchingDirection>();
        
    }

    private void FixedUpdate()
    {
        if (touchingDirection.IsGrounded && touchingDirection.isOnWall)
        {
            FlipDirection();
        }
        rb.linearVelocity = new Vector2(moveSpeed * WalkDirectionVector.x, rb.linearVelocity.y);
    }
    

    public void FlipDirection()
    {
        if (WalkDirection == WalkableDirection.Right)
        {
            WalkDirection = WalkableDirection.Left;
        }
        else if (WalkDirection == WalkableDirection.Left)
        {
            WalkDirection = WalkableDirection.Right;
        }
        else
        {
            Debug.LogError("WalkDirection not set properly");
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _walkDirection = WalkableDirection.Left;
        WalkDirectionVector = Vector2.left;
   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
