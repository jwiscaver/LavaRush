# Unity C# Code Style & Engineering Standards

This document defines the coding standards for all scripts in this Unity project.

AI agents MUST follow these rules when generating or modifying code.

The goals are:

- readability
- maintainability
- performance
- modular design

Agents must prioritize clarity over cleverness.

---

# General C# Coding Standards

## Naming Conventions

Use descriptive and meaningful names for variables, methods, and classes.

### PascalCase

Use PascalCase for:

- classes
- public properties
- public fields
- public methods

Examples:

PlayerController  
CalculateScore()  
InventoryManager  

---

### camelCase

Use camelCase for:

- private fields
- local variables
- method parameters

Examples:

movementSpeed  
playerScore  
spawnPoint  

---

### Boolean Naming

Boolean variables and methods should read like questions.

Examples:

isDead  
isGrounded  
hasStartedTurn  
canAttack  

Avoid ambiguous names like:

flag  
state  

---

# Code Structure & Readability

## Single Responsibility

Classes should have one clear responsibility.

Bad:

PlayerController handling:
- movement
- combat
- inventory
- UI

Good:

PlayerController  
PlayerCombat  
PlayerInventory  

---

## Prefer Composition Over Inheritance

Avoid deep inheritance hierarchies.

Use Unity’s component model instead.

Good:

GameObject
    PlayerController
    PlayerHealth
    PlayerCombat

Bad:

BaseCharacter
    PlayerCharacter
        AdvancedPlayerCharacter

---

## Explicit Access Modifiers

Always declare access modifiers.

Never rely on defaults.

Example:

private int health;

not

int health;

---

## Namespaces

Scripts should use namespaces that mirror the folder structure.

Example:

Assets/Scripts/Player/PlayerController.cs

```csharp
namespace Game.Player
{
    public class PlayerController : MonoBehaviour
    {
    }
}```

This prevents naming collisions and keeps large projects organized.



Braces

Always use braces even for single-line statements.

Correct:

```csharp
if (isDead)
{
    return;
}
```

Avoid:

```csharp
if (isDead)
    return;

```

This improves readability and prevents future bugs.


Commenting & Documentation

Self-explanatory code is preferred over excessive comments.

Write code that explains itself through clear naming and structure.

Use comments only when logic is not obvious.

Example:

// Prevent player from jumping while already airborne

Public APIs or reusable systems should use XML documentation.

Example:

/// Applies damage to the player
/// </summary>
public void ApplyDamage(int amount)
Unity Performance Guidelines

Unity performance rules must always be followed.

Avoid Expensive Operations in Update()

Never place expensive calls inside Update loops.

Avoid:

GameObject.Find()

GetComponent() repeatedly

LINQ queries

string concatenation

Cache Component References

Always cache component references in Awake() or Start().

Example:

private Rigidbody rb;

void Awake()
{
    rb = GetComponent<Rigidbody>();
}

Do NOT repeatedly call GetComponent in Update.

Object Pooling

Avoid frequent Instantiate() and Destroy() calls.

For frequently spawned objects (bullets, particles, enemies), use object pools.

Example systems:

BulletPool
ParticlePool
EnemyPool

Remove Empty Unity Methods

Empty MonoBehaviour methods should be removed.

Example to avoid:

void Update()
{
}

Even empty methods incur a small performance cost.

Unity Architecture Guidelines
ScriptableObjects

Game data should be separated from runtime logic.

Use ScriptableObjects for:

item definitions

weapon stats

configuration values

enemy definitions

game balance data

Example:

WeaponData : ScriptableObject

Design Patterns

Use common game development patterns when appropriate.

Examples:

Observer Pattern
Factory Pattern
State Pattern
Singleton (only when justified)

Avoid overengineering.

Patterns should improve modularity and readability.

Scene Organization

Scenes should remain clean and easy to navigate.

Best practices:

Use empty parent GameObjects for grouping

Avoid excessive root objects

Group related systems

Example hierarchy:

GameSystems
Player
Enemies
Environment
UI

Avoid Strings for Identification

Strings are fragile and slow.

Avoid using strings for identifiers.

Prefer:

Enums
ScriptableObjects
Hash values

Example:

EnemyType.Zombie

instead of

"Zombie"

Event Handling

When subscribing to events, always unsubscribe.

Failing to do this can cause memory leaks.

Example:

void OnEnable()
{
    GameEvents.OnGameStart += HandleGameStart;
}

void OnDisable()
{
    GameEvents.OnGameStart -= HandleGameStart;
}




