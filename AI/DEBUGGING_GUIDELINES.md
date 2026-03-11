# Unity Debugging Guidelines

This document defines how AI agents should diagnose and fix bugs in this Unity project.

The goal is to produce reliable fixes while avoiding risky or unnecessary refactors.

Agents must follow a structured debugging process.

---

# Debugging Process

When investigating a bug, agents must follow this order:

1. Identify the failing system.
2. Inspect relevant scripts.
3. Check console errors and stack traces.
4. Determine root cause before proposing changes.
5. Implement minimal corrective changes.
6. Verify the fix does not break related systems.

Agents should avoid rewriting entire systems unless absolutely necessary.

---

# Unity Console Errors

Unity errors usually provide the most important debugging information.

Agents must inspect:

- exception type
- stack trace
- script name
- line number

Example:

NullReferenceException: Object reference not set to an instance of an object
PlayerController.Update() (at Assets/Scripts/Player/PlayerController.cs:42)

Typical causes:

- missing serialized references
- object not assigned in inspector
- component not present

---

# NullReferenceException

Most common Unity error.

Typical causes:

- missing component
- missing inspector reference
- destroyed object access

Preferred fixes:

- add null checks
- validate serialized fields
- cache required components

Example:

if(player != null)
{
    player.Move();
}

---

# Missing References

Serialized fields must be validated.

Example:

[SerializeField] private Transform target;

If a required reference exists, agents should ensure:

- object is assigned in inspector
- fallback logic exists if missing

Example:

if(target == null)
{
    Debug.LogError("Target not assigned");
}

---

# Physics Bugs

Common physics issues include:

- movement applied in Update instead of FixedUpdate
- direct transform manipulation with Rigidbody
- conflicting physics components

Correct patterns:

Physics logic ? FixedUpdate  
Input logic ? Update  

---

# Performance Debugging

If performance problems occur, agents should check for:

- expensive operations in Update
- excessive allocations
- frequent Instantiate / Destroy
- large numbers of active objects

Typical fixes:

- object pooling
- cached references
- event-driven logic

---

# AI System Debugging

Enemy AI bugs often originate from:

- incorrect state transitions
- NavMeshAgent misconfiguration
- missing target references

Agents should inspect:

- state machine transitions
- movement targets
- navigation components

---

# Update Loop Misuse

Many bugs originate from excessive Update usage.

Agents should consider replacing Update logic with:

- events
- coroutines
- timers

---

# Scene Setup Errors

Some bugs are caused by scene configuration rather than code.

Agents should check:

- missing prefabs
- unassigned inspector references
- incorrect layer assignments
- disabled GameObjects

---

# Safe Fixing Guidelines

Agents must follow these rules when fixing bugs:

1. Prefer minimal targeted fixes.
2. Avoid rewriting entire systems.
3. Maintain compatibility with existing architecture.
4. Follow CODE_STYLE.md and IMPLEMENTATION_PATTERNS.md.
5. Ensure the fix compiles and does not introduce warnings.

---

# AI Agent Responsibilities

When returning a bug fix, agents must include:

Summary of the bug  
Root cause explanation  
Files modified  
Unity setup changes (if required)  
Steps to verify the fix in Play Mode

Agents must confirm that the fix follows project coding standards.
