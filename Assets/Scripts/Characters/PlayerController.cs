using UnityEngine;
using UnityEngine.InputSystem;

namespace Characters
{
    // PlayerController.cs
    // Controls 2D player left/right movement using the new Input System.
    // Attach to the player GameObject with Rigidbody2D and BoxCollider2D.
    // Set up Input System to call OnMove with a Vector2 action (e.g., WASD or arrow keys).
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 5f; // Movement speed
        private Rigidbody2D rb;
        private float moveInput;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        // Called by Input System (PlayerInput component, Send Messages)
        public void OnMove(InputValue value)
        {
            moveInput = value.Get<Vector2>().x;
        }

        private void FixedUpdate()
        {
            // Move the player left/right
            rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
        }
    }
} 