# Unity Project Invariants

These rules must always remain true.

Agents must stop if a change would violate them.

---

## Script Placement

All scripts must live inside:

Assets/Scripts

---

## MonoBehaviour Rules

MonoBehaviour classes must:

- use PascalCase filenames
- contain one class per file
- match filename

Example:

PlayerController.cs
class PlayerController

---

## Performance Rules

Avoid:

- allocations in Update()
- FindObjectOfType in loops
- excessive GetComponent calls

Cache references in Start() or Awake().

--


## Physics System

This project uses Unity's 2D physics engine.

All gameplay physics must use:
- Rigidbody2D
- Collider2D
- Physics2D queries

3D physics APIs must not be used for gameplay systems.

--