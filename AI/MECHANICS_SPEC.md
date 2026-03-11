# Gameplay Mechanics Specification


This document defines the detailed mechanics of gameplay systems.

Agents implementing gameplay features must follow this specification.

---
# Player Movement

Movement Type:

Physics-based using Rigidbody.

Controls:

W / Up Arrow: Move forward  
S / Down Arrow: Move backward  
A / Left Arrow: Move left  
D / Right Arrow: Move right  

Movement Speed:

Default speed = 5 units/sec

Jump:

Spacebar triggers jump.

Jump force = 7

Constraints:

Player cannot jump while airborne.


---

# Combat System

Attack Input:

Left mouse button

Attack Range:

2 units

Damage:

Base damage = 10

Attack Cooldown:

0.5 seconds

Enemy hit detection uses Unity colliders.


---

# Health System

Player Health:

Maximum health = 100

Damage reduces health.

When health reaches 0:

Player dies.

---

# Enemy AI

Enemies use NavMeshAgent for movement.

Behavior states:

Idle  

Patrol  

Chase  

Attack  


State transitions:

Idle → Patrol after spawn  

Patrol → Chase when player detected  

Chase → Attack within range  

Attack → Chase if player leaves range

---

# Inventory System

Inventory capacity = 20 items.

Items stored as ScriptableObject definitions.

Item types:

Weapon  
Consumable  

---

# Loot System

Enemies have drop tables.

Example:

Goblin:

Gold (80%)
Health Potion (20%)

---

# Save System

Game saves:

Player stats  

Inventory  

Current level


Save format:

JSON serialized data.

