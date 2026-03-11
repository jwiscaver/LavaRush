# Implementation Patterns

This document defines preferred implementation patterns for this Unity project.

AI agents must follow these patterns when building new systems.

The goal is to maintain consistency, modularity, and predictable architecture.

---

# Component-Based Systems

Unity projects should favor component composition rather than inheritance.

Preferred pattern:

GameObject
    MovementComponent
    HealthComponent
    CombatComponent
    InventoryComponent

Avoid creating large "god classes".

Example:

Bad:

PlayerController handling movement, combat, UI, and inventory.

Good:

PlayerController
PlayerCombat
PlayerInventory
PlayerHealth

Each component should have a clear responsibility.

---

# Event Driven Communication

Systems should communicate using events rather than direct references.

Preferred pattern:

GameEvents.OnPlayerDamaged
GameEvents.OnEnemyKilled

Benefits:

- loose coupling
- easier testing
- easier system replacement

Example:

PlayerHealth raises event when damaged.

UI listens to update health bar.

---

# Manager Systems

Global systems should be implemented as managers.

Examples:

GameManager
AudioManager
SaveManager
PoolManager

Guidelines:

Managers should:

- be minimal
- control a specific domain
- avoid becoming global god objects

---

# ScriptableObject Data Pattern

Configuration data should live in ScriptableObjects.

Examples:

WeaponData
EnemyData
ItemData
LevelConfig

ScriptableObjects should contain:

- configuration values
- design-time data
- balancing parameters

They should NOT contain runtime state.

---

# Object Pool Pattern

Objects that spawn frequently should use object pooling.

Examples:

Bullets
Particles
Enemies
Projectiles

Preferred system:

PoolManager handles creation and reuse.

Example usage:

PoolManager.Get("Bullet")

---

# State Machine Pattern

Complex behaviors should use state machines.

Examples:

Enemy AI
Boss behaviors
Player combat states

Typical states:

Idle
Patrol
Chase
Attack
Dead

Avoid large Update methods filled with conditional logic.

---

# Dependency Injection (Lightweight)

Avoid excessive use of FindObjectOfType or GameObject.Find.

Dependencies should be assigned via:

[SerializeField]

Example:

[SerializeField] private PlayerController player;

This improves performance and reduces hidden dependencies.

---

# System Folder Placement

New systems must be placed in the correct folders.

Assets/Scripts/Core
Assets/Scripts/Player
Assets/Scripts/Enemies
Assets/Scripts/Systems
Assets/Scripts/UI
Assets/Scripts/Utilities

Agents must follow this structure when creating new scripts.

---

# Update Loop Usage

Update loops should be minimal.

Prefer:

Events
Coroutines
Timers

Example:

Use coroutine for cooldowns instead of Update polling.

---

# Pattern Compliance

When implementing new gameplay systems, agents must:

1. Follow component-based architecture.
2. Prefer event-driven communication.
3. Use ScriptableObjects for configuration.
4. Use object pooling when spawning frequently.
5. Avoid tightly coupled systems.
6. Avoid large monolithic classes.

If a generated solution violates these patterns, the agent must revise the implementation before returning the result.
