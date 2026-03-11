# Unity Anti-Patterns

This document lists common Unity development mistakes.

AI agents must avoid these patterns when generating code.

The goal is to prevent:

- performance issues
- fragile systems
- poor architecture
- unmaintainable code

---

# Performance Anti-Patterns

## Using GameObject.Find()

Bad:

GameObject player = GameObject.Find("Player");

Why it is bad:

- slow
- fragile
- breaks if object name changes

Preferred:

[SerializeField] private GameObject player;

or dependency injection.

---

## Repeated GetComponent() Calls

Bad:

void Update()
{
    GetComponent<Rigidbody>().AddForce(Vector3.forward);
}

Problem:

GetComponent is relatively expensive and should not be called repeatedly.

Correct:

private Rigidbody rb;

void Awake()
{
    rb = GetComponent<Rigidbody>();
}

---

## Instantiating Objects Every Frame

Bad:

Instantiate(bulletPrefab);

Problem:

Frequent instantiation causes garbage collection spikes.

Correct:

Use object pooling.

Example:

BulletPool.GetBullet();

---

## Allocations in Update()

Avoid allocating objects every frame.

Bad:

void Update()
{
    List<int> values = new List<int>();
}

This causes memory churn and GC spikes.

---

# Architecture Anti-Patterns

## God Classes

Bad:

PlayerController handling:

movement  
combat  
inventory  
UI  
audio  
animations  

Correct:

Split into components.

PlayerController  
PlayerCombat  
PlayerInventory  
PlayerAnimation

---

## Deep Inheritance Trees

Bad:

Character  
PlayerCharacter  
MageCharacter  
AdvancedMageCharacter  

Prefer Unity composition instead.

Attach multiple components to a GameObject.

---

## Mixing UI and Gameplay Logic

Bad:

PlayerController modifying UI directly.

Correct:

Gameplay systems emit events.

UI listens for events.

Example:

GameEvents.OnPlayerHealthChanged

---

# ScriptableObject Misuse

## Storing Runtime State

ScriptableObjects should not store runtime state.

Bad:

ScriptableObject storing player health during gameplay.

Correct:

ScriptableObjects should only contain configuration data.

Example:

Weapon stats  
Enemy definitions  
Game balance values

---

# Scene Management Anti-Patterns

## Too Many Root Objects

Bad hierarchy:

Scene
Object1
Object2
Object3
Object4
Object5

Correct:

Group objects under logical parents.

Example:

GameSystems
Player
Enemies
Environment
UI

---

# String Based Identification

Bad:

if(enemyType == "Zombie")

Problems:

- slow
- fragile
- typo prone

Correct:

Use enums.

Example:

EnemyType.Zombie

---

# Update Loop Abuse

Not every script needs Update().

Bad:

void Update()
{
}

Empty Update methods waste CPU cycles.

Correct:

Use events or coroutines when possible.

---

# Tight Coupling Between Systems

Bad:

Inventory directly accessing PlayerController internals.

Correct:

Use events or interfaces to decouple systems.

Example:

IInventoryUser

---

## Mixing 2D and 3D Physics APIs

Unity has two completely separate physics engines:

2D Physics
- Rigidbody2D
- Collider2D
- Physics2D

3D Physics
- Rigidbody
- Collider
- Physics

Agents must never mix the two systems.

Incorrect:
- Rigidbody2D with Physics.CheckSphere
- Rigidbody with Physics2D.OverlapCircle
- Collider with Rigidbody2D

Correct:
2D controllers must exclusively use:
- Rigidbody2D
- Collider2D
- Physics2D queries

3D controllers must exclusively use:
- Rigidbody
- Collider
- Physics queries

If the project uses 2D gameplay, all player locomotion systems must use the 2D physics API.

-- 

# AI Agent Rules

When generating Unity code, agents must avoid all anti-patterns listed in this document.

If a generated solution contains one of these patterns, the agent must revise the implementation before returning the final result.
