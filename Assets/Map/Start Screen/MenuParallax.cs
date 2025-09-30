using UnityEngine;
using UnityEngine.InputSystem;  // 新 Input System

public class MenuParallax : MonoBehaviour
{
    public float offsetMultiplier = 1f;
    public float smoothTime = .3f;

    private Vector3 startPosition;
    private Vector3 velocity;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        // 新 Input System 获取鼠标位置
        Vector3 mousePos = Mouse.current.position.ReadValue();

        Vector3 offset = Camera.main.ScreenToViewportPoint(mousePos);
        transform.position = Vector3.SmoothDamp(
            transform.position,
            startPosition + (offset * offsetMultiplier),
            ref velocity,
            smoothTime
        );
    }
}
