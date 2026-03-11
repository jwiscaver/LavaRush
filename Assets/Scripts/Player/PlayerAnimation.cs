using UnityEngine;

namespace Game.Player
{
    /// <summary>
    /// Drives player animation parameters from current movement and player state.
    /// </summary>
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(PlayerController))]
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class PlayerAnimation : MonoBehaviour
    {
        private static readonly int SpeedHash = Animator.StringToHash("Speed");
        private static readonly int IsJumpingHash = Animator.StringToHash("IsJumping");
        private static readonly int ShortRangeAttackHash = Animator.StringToHash("ShortRangeAttack");
        private static readonly int LongRangeAttackHash = Animator.StringToHash("LongRangeAttack");
        private static readonly int IsKnockbackHash = Animator.StringToHash("IsKnockback");
        private static readonly int IsDeadHash = Animator.StringToHash("IsDead");
        private static readonly int DeathHash = Animator.StringToHash("Death");
        private static readonly int DirectionHash = Animator.StringToHash("Direction");

        private Animator animator;
        private PlayerController playerController;
        private Rigidbody2D playerRigidbody;
        private Vector3 defaultLocalScale;
        private float facingDirection = 1f;
        private bool isKnockback;
        private bool isDead;

        private void Awake()
        {
            animator = GetComponent<Animator>();
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
            bool isJumping = !playerController.IsGrounded;

            UpdateFacingDirection(horizontalVelocity);
            UpdateAnimatorParameters(speed, isJumping);
        }

        /// <summary>
        /// Triggers the default attack animation on the Animator.
        /// </summary>
        public void TriggerAttack()
        {
            TriggerShortRangeAttack();
        }

        /// <summary>
        /// Triggers the short-range weapon attack animation on the Animator.
        /// </summary>
        public void TriggerShortRangeAttack()
        {
            animator.SetTrigger(ShortRangeAttackHash);
        }

        /// <summary>
        /// Triggers the long-range weapon attack animation on the Animator.
        /// </summary>
        public void TriggerLongRangeAttack()
        {
            animator.SetTrigger(LongRangeAttackHash);
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

        private void UpdateAnimatorParameters(float speed, bool isJumping)
        {
            animator.SetFloat(SpeedHash, speed);
            animator.SetBool(IsJumpingHash, isJumping);
            animator.SetBool(IsKnockbackHash, isKnockback);
            animator.SetBool(IsDeadHash, isDead);
            animator.SetFloat(DirectionHash, facingDirection);
        }
    }
}
