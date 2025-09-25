using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;
    public float moveSpeed = 5f;
    public float runSpeed = 8f;


    public float jumpImpulse = 10f;
    [SerializeField] private bool _isMoving = false;
    [SerializeField] private bool _isRunning = false;
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
    public float currentSpeed
    {
        get
        {
            if (IsMoving)
            {
                if (IsRunning)
                {
                    return runSpeed;
                }
                else
                {
                    return moveSpeed;
                }
            }
            else
            {
                //Idle state
                return 0;
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
        IsMoving = moveInput != Vector2.zero;
        setFacingDirection(moveInput);
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
        if (context.started && touchingDir.IsGrounded)
        {
            animator.SetTrigger(AnimationString.jump);
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpImpulse);
        }
    }
}


