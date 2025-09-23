using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    public float moveSpeed = 5f;
    Vector2 moveInput;
    public bool IsMoving { get; private set; }

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        // Check whether the char is moving or not 
        IsMoving = moveInput != Vector2.zero;
    }
}
