using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;
    public bool canMove{get
        {
            return animator.GetBool(AnimationString.canMove);
        }
    }

    public bool IsAlive
    {
        get
        {
            return animator.GetBool(AnimationString.isAlive);
        }
    }
    public float moveSpeed = 4f; // Move Speed
    public float runSpeed = 7f;  // Run Speed

    public float airWalkSpeed = 3f; // Air Travel Distance Speed

    public float jumpImpulse = 6f; // How high player can jump 
    [SerializeField] private bool _isMoving = false;  // Check if player is moving or not 
    [SerializeField] private bool _isRunning = false; // Check if player is running or not
    Vector2 moveInput;


    //Reference to TouchingDirection script
    TouchingDirection touchingDir;
    public bool _isFacingRight = true;
    public bool isFacingRight
    {
        get
        {
            return _isFacingRight;
        }
        private set
        {
            if (_isFacingRight != value)
            {
                transform.localScale *= new Vector2(-1, 1);
            }
            _isFacingRight = value;
        }
    }

    // Check the currentMove speed of the player
    public float currentSpeed
    {
        get
        {
            if (canMove)
            {
                if (touchingDir.IsGrounded)
            {
                if (IsMoving)
                {
                    return IsRunning ? runSpeed : moveSpeed;
                }
                else
                {
                    return 0; // idle on ground
                }
            }
            else
            {
                // In the air, limited control
                return airWalkSpeed;
            }
            }
            else
            {
                return 0; // cannot move
            }
        }
    }





    public bool IsMoving
    {
        get
        {
            return _isMoving;
        }
        private set
        {
            _isMoving = value;
            animator.SetBool(AnimationString.IsMoving, value);
        }
    }


    public bool IsRunning
    {
        get
        {
            return _isRunning;
        }
        private set
        {
            _isRunning = value;
            animator.SetBool(AnimationString.IsRunning, value);
        }
    }


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDir = GetComponent<TouchingDirection>();


    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {


    }


    // Update is called once per frame
    void Update()
    {


    }




    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveInput.x * currentSpeed, rb.linearVelocity.y);
        animator.SetFloat(AnimationString.yVelocity, rb.linearVelocity.y);
    }


    //Basic Movement for Player
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        // Check whether the char is moving or not

        if (IsAlive)
        {
            IsMoving = moveInput != Vector2.zero;
            setFacingDirection(moveInput);
        }
        else
        {
            IsMoving = false;
        }
        
    }


    //Determine the facing direction of the player
    private void setFacingDirection(Vector2 moveInput)
    {
        if (moveInput.x > 0 && !isFacingRight)
        {
            //face right
            isFacingRight = true;
        }
        else if (moveInput.x < 0 && isFacingRight)
        {
            // face left
            isFacingRight = false;
        }
    }




    //Check whether the player is running or not, if yes then run, no back to idle state.
    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            IsRunning = true;
        }
        else if (context.canceled)
        {
            IsRunning = false;
        }
    }

    //Jump function for the player
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started && touchingDir.IsGrounded && canMove)
        {
            animator.SetTrigger(AnimationString.jump);
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpImpulse);
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            animator.SetTrigger(AnimationString.attack);
        }
       
    }
}


