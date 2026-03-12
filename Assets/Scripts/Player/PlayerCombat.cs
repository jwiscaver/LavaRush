using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Player
{
    [RequireComponent(typeof(PlayerAnimation))]
    public sealed class PlayerCombat : MonoBehaviour
    {
        [Header("Debug Weapon Selection")]
        [SerializeField] private WeaponType equippedWeaponType = WeaponType.Melee;

        private PlayerAnimation playerAnimation;

        public WeaponType EquippedWeaponType => equippedWeaponType;

        private void Awake()
        {
            playerAnimation = GetComponent<PlayerAnimation>();
        }

        private void Update()
        {
            HandleWeaponSwitchInput();
            HandleAttackInput();
        }

        public void UseWeapon()
        {
            if (equippedWeaponType == WeaponType.Melee)
            {
                PerformMeleeAttack();
                return;
            }

            PerformRangedAttack();
        }

        public void PerformMeleeAttack()
        {
            playerAnimation.TriggerMeleeAttack();
        }

        public void PerformRangedAttack()
        {
            playerAnimation.TriggerRangedAttack();
        }

        private void HandleAttackInput()
        {
            Mouse mouse = Mouse.current;
            if (mouse == null || !mouse.leftButton.wasPressedThisFrame)
            {
                return;
            }

            UseWeapon();
        }

        private void HandleWeaponSwitchInput()
        {
            Keyboard keyboard = Keyboard.current;
            if (keyboard == null)
            {
                return;
            }

            bool switchedToMelee = keyboard.digit1Key.wasPressedThisFrame || keyboard.numpad1Key.wasPressedThisFrame;
            bool switchedToRanged = keyboard.digit2Key.wasPressedThisFrame || keyboard.numpad2Key.wasPressedThisFrame;

            if (switchedToMelee)
            {
                equippedWeaponType = WeaponType.Melee;
            }
            else if (switchedToRanged)
            {
                equippedWeaponType = WeaponType.Ranged;
            }
        }
    }
}
