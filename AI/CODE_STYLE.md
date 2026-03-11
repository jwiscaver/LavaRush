# Unity C# Code Style & Engineering Standards

This document defines the coding standards for all scripts in this Unity project.

AI agents MUST follow these rules when generating or modifying code.

Goals:
- Readability
- Maintainability
- Performance
- Modular design
- Predictable architecture

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
- namespaces
- enums

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

Use Unity s component model instead.

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

Correct:

private int health;

Incorrect:

int health;

---

# Namespaces

Scripts should use namespaces mirroring the folder structure.

Example folder:

Assets/Scripts/Player/PlayerController.cs

Example namespace:

namespace Game.Player
{
    public class PlayerController : MonoBehaviour
    {
    }
}

Benefits:
- prevents naming conflicts
- improves large-project organization
- improves IDE navigation

---

# Braces

Always use braces even for single-line statements.

Correct:

if (isDead)
{
    return;
}

Avoid:

if (isDead)
    return;

---

# Commenting & Documentation

Prefer self-explanatory code.

Use comments only when logic is not obvious.

Example:

// Prevent player from jumping while airborne

Public APIs should use XML documentation.

Example:

/// <summary>
/// Applies damage to the player.
/// </summary>
public void ApplyDamage(int amount)

---

# Unity Lifecycle Order

Preferred MonoBehaviour method order:

Awake()  
OnEnable()  
Start()  
Update()  
FixedUpdate()  
LateUpdate()  
OnDisable()  
OnDestroy()  

Custom methods should appear after Unity lifecycle methods.

---

# Unity Performance Guidelines

## Avoid Expensive Operations in Update()

Avoid:

GameObject.Find()  
GetComponent() repeatedly  
LINQ queries  
string concatenation  

---

## Cache Component References

Always cache component references in Awake() or Start().

Example:

private Rigidbody rb;

void Awake()
{
    rb = GetComponent<Rigidbody>();
}

---

## Object Pooling

Avoid frequent Instantiate() and Destroy() calls.

Use object pools for frequently spawned objects such as:

- bullets
- particle systems
- enemies
- projectiles

---

## Remove Empty Unity Methods

Avoid empty MonoBehaviour methods:

void Update()
{
}

---

# Memory Allocation Rules

Avoid runtime allocations in frequently executed code.

Avoid inside Update:
- new object creation
- LINQ queries
- boxing operations
- string concatenation

Prefer:
- cached lists
- reused buffers
- object pools

---

# Unity Architecture Guidelines

## ScriptableObjects

Game data should be separated from runtime logic using ScriptableObjects.

Use ScriptableObjects for:
- item definitions
- weapon stats
- configuration values
- enemy definitions
- gameplay balance data

---

## Design Patterns

Recommended patterns:

Observer Pattern  
Factory Pattern  
State Pattern  

Singleton should only be used when necessary.

Avoid overengineering.

---

# Scene Organization

Scenes should remain clean and easy to navigate.

Example hierarchy:

GameSystems  
Player  
Enemies  
Environment  
UI  

Use empty parent GameObjects for grouping.

---

# Avoid Strings for Identification

Strings are fragile and error-prone.

Prefer:

Enums  
ScriptableObjects  
Hash values  

Example:

EnemyType.Zombie

instead of

"Zombie"

---

# Event Handling

When subscribing to events, always unsubscribe.

Example:

void OnEnable()
{
    GameEvents.OnGameStart += HandleGameStart;
}

void OnDisable()
{
    GameEvents.OnGameStart -= HandleGameStart;
}

---

# Folder Organization

Gameplay scripts must follow the project folder structure.

Assets/Scripts
    Core
    Player
    Enemies
    Systems
    UI
    Utilities

---

# AI Agent Compliance

AI agents generating code MUST:

1. Follow naming conventions exactly.
2. Place scripts in the correct folders.
3. Avoid performance anti-patterns.
4. Avoid duplicate gameplay systems.
5. Follow architecture rules defined in ARCHITECTURE.md.
6. Ensure scripts compile in Unity without warnings.
7. Prefer modular reusable components over monolithic scripts.

If a generated solution violates any rule in this document, the agent must revise the code before returning the result.
