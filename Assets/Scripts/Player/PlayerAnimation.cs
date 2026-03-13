using UnityEngine;

namespace Game.Player
{
    /// <summary>
    /// Drives player animation parameters from current movement and player state.
    /// </summary>
    [RequireComponent(typeof(PlayerController))]
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class PlayerAnimation : MonoBehaviour
    {
        private const float GroundedBufferSeconds = 0.05f;

        private static readonly int SpeedHash = Animator.StringToHash("Speed");
        private static readonly int SpeedMultiplierHash = Animator.StringToHash("SpeedMultiplier");
        private static readonly int IsJumpingHash = Animator.StringToHash("IsJumping");
        private static readonly int MeleeAttackHash = Animator.StringToHash("MeleeAttack");
        private static readonly int RangedAttackHash = Animator.StringToHash("RangedAttack");
        private static readonly int IsKnockbackHash = Animator.StringToHash("IsKnockback");
        private static readonly int IsDeadHash = Animator.StringToHash("IsDead");
        private static readonly int DeathHash = Animator.StringToHash("Death");
        private static readonly int DirectionHash = Animator.StringToHash("Direction");

        [SerializeField] Animator animator;
        private PlayerController playerController;
        private Rigidbody2D playerRigidbody;
        private Vector3 defaultLocalScale;
        private float facingDirection = 1f;
        private float groundedTimer;
        private bool isKnockback;
        private bool isDead;

        private void Awake()
        {
            playerController = GetComponent<PlayerController>();
            playerRigidbody = GetComponent<Rigidbody2D>();
            defaultLocalScale = transform.localScale;

            if (defaultLocalScale.x < 0f)
            {
                facingDirection = -1f;
            }
        }

        private void Update()
        {
            float horizontalVelocity = playerRigidbody.linearVelocity.x;
            float speed = Mathf.Abs(horizontalVelocity);

            UpdateGroundedBuffer();

            UpdateFacingDirection(horizontalVelocity);
            UpdateAnimatorParameters(speed, groundedTimer <= 0f);
        }

        /// <summary>
        /// Triggers the default attack animation on the Animator.
        /// </summary>
        public void TriggerAttack()
        {
            TriggerMeleeAttack();
        }

        /// <summary>
        /// Triggers the melee weapon attack animation on the Animator.
        /// </summary>
        public void TriggerMeleeAttack()
        {
            animator.SetTrigger(MeleeAttackHash);
        }

        /// <summary>
        /// Triggers the ranged weapon attack animation on the Animator.
        /// </summary>
        public void TriggerRangedAttack()
        {
            animator.SetTrigger(RangedAttackHash);
        }

        /// <summary>
        /// Sets whether the player is currently in knockback.
        /// </summary>
        /// <param name="value">True while knockback is active.</param>
        public void SetKnockback(bool value)
        {
            isKnockback = value;
            animator.SetBool(IsKnockbackHash, isKnockback);
        }

        /// <summary>
        /// Triggers the death animation on the Animator and marks the player as dead.
        /// </summary>
        public void TriggerDeath()
        {
            SetDead(true);
            animator.SetTrigger(DeathHash);
        }

        /// <summary>
        /// Sets whether the player is currently dead.
        /// </summary>
        /// <param name="value">True while the player should remain in the dead state.</param>
        public void SetDead(bool value)
        {
            isDead = value;
            animator.SetBool(IsDeadHash, isDead);
        }

        private void UpdateFacingDirection(float horizontalVelocity)
        {
            if (horizontalVelocity > 0f)
            {
                facingDirection = 1f;
            }
            else if (horizontalVelocity < 0f)
            {
                facingDirection = -1f;
            }

            transform.localScale = new Vector3(
                Mathf.Abs(defaultLocalScale.x) * facingDirection,
                defaultLocalScale.y,
                defaultLocalScale.z);
        }

        private void UpdateGroundedBuffer()
        {
            if (playerController.IsGrounded)
            {
                groundedTimer = GroundedBufferSeconds;
            }
            else
            {
                groundedTimer -= Time.deltaTime;
            }
        }

        private void UpdateAnimatorParameters(float speed, bool isJumping)
        {
            float maxHorizontalSpeed = Mathf.Max(playerController.MaxHorizontalSpeed, 0.01f);
            float normalizedSpeed = Mathf.Clamp01(speed / maxHorizontalSpeed);
            float speedMultiplier = speed / maxHorizontalSpeed;

            animator.SetFloat(SpeedHash, normalizedSpeed, 0.03f, Time.deltaTime);
            animator.SetFloat(SpeedMultiplierHash, speedMultiplier);
            animator.SetBool(IsJumpingHash, isJumping);
            animator.SetBool(IsKnockbackHash, isKnockback);
            animator.SetBool(IsDeadHash, isDead);
            animator.SetFloat(DirectionHash, facingDirection);
        }
    }
}
