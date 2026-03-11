using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class PlayerController : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float sprintMultiplier = 1.5f;

        [Header("Jump")]
        [SerializeField] private float jumpForce = 7f;
        [SerializeField] private float jumpBufferSeconds = 0.1f;
        [SerializeField] private float coyoteTimeSeconds = 0.15f;

        [Header("Ground Check")]
        [SerializeField] private Transform groundCheck;
        [SerializeField] private float groundCheckRadius = 0.2f;
        [SerializeField] private LayerMask groundLayers = ~0;

        private Rigidbody2D playerRigidbody;
        private GameInput gameInput;
        private InputAction moveAction;
        private InputAction jumpAction;
        private InputAction sprintAction;
        private Vector2 moveInput;
        private bool isGrounded;
        private bool isSprintPressed;
        private float lastGroundedTime = float.NegativeInfinity;
        private float lastJumpPressedTime = float.NegativeInfinity;

        public bool IsGrounded => isGrounded;
        public bool IsSprintPressed => isSprintPressed;

        private void Awake()
        {   
            playerRigidbody = GetComponent<Rigidbody2D>();
            playerRigidbody.interpolation = RigidbodyInterpolation2D.Interpolate;
            playerRigidbody.constraints |= RigidbodyConstraints2D.FreezeRotation;

            if (groundCheck == null)
            {
                Transform groundCheckTransform = transform.Find("GroundCheck");
                if (groundCheckTransform != null)
                {
                    groundCheck = groundCheckTransform;
                }
            }

            gameInput = new GameInput();
            moveAction = gameInput.FindAction("Player/Move", true);
            jumpAction = gameInput.FindAction("Player/Jump", true);
            sprintAction = gameInput.FindAction("Player/Sprint", true);
        }

        private void OnEnable()
        {
            gameInput?.Enable();
        }

        private void Update()
        {
            if (moveAction == null || jumpAction == null || sprintAction == null)
            {
                return;
            }

            moveInput = moveAction.ReadValue<Vector2>();

            isSprintPressed = sprintAction.IsPressed();

            if (jumpAction.WasPressedThisFrame())
            {
                lastJumpPressedTime = Time.time;
            }
        }

        private void FixedUpdate()
        {
            UpdateGroundedState();
            ApplyMovement();
            TryJump();
        }

        private void OnDisable()
        {
            gameInput?.Disable();
        }

        private void OnDestroy()
        {
            if (gameInput != null)
            {
                gameInput.Dispose();
            }
        }

        private void UpdateGroundedState()
        {
            if (groundCheck == null)
            {
                isGrounded = false;
                return;
            }

            Vector2 checkPosition = groundCheck.position;
            isGrounded = Physics2D.OverlapCircle(checkPosition, groundCheckRadius, groundLayers) != null;

            if (isGrounded)
            {
                lastGroundedTime = Time.time;
            }
        }

        private void ApplyMovement()
        {
            float currentSpeed = isSprintPressed ? moveSpeed * sprintMultiplier : moveSpeed;
            Vector2 currentVelocity = playerRigidbody.linearVelocity;

            currentVelocity.x = moveInput.x * currentSpeed;
            playerRigidbody.linearVelocity = currentVelocity;
        }

        private void TryJump()
        {
            if (!HasBufferedJump() || !HasCoyoteTime())
            {
                return;
            }

            Vector2 currentVelocity = playerRigidbody.linearVelocity;
            if (currentVelocity.y < 0f)
            {
                currentVelocity.y = 0f;
            }

            playerRigidbody.linearVelocity = currentVelocity;
            playerRigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
            lastGroundedTime = float.NegativeInfinity;
            lastJumpPressedTime = float.NegativeInfinity;
        }

        private bool HasBufferedJump()
        {
            return Time.time - lastJumpPressedTime <= jumpBufferSeconds;
        }

        private bool HasCoyoteTime()
        {
            return Time.time - lastGroundedTime <= coyoteTimeSeconds;
        }

        private void OnDrawGizmosSelected()
        {
            if (groundCheck == null)
            {
                return;
            }

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }

        private void OnValidate()
        {
            moveSpeed = Mathf.Max(0f, moveSpeed);
            sprintMultiplier = Mathf.Max(1f, sprintMultiplier);
            jumpForce = Mathf.Max(0f, jumpForce);
            jumpBufferSeconds = Mathf.Max(0f, jumpBufferSeconds);
            coyoteTimeSeconds = Mathf.Max(0f, coyoteTimeSeconds);
            groundCheckRadius = Mathf.Max(0.01f, groundCheckRadius);
        }
    }
}
