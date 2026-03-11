# Codex Task Template — Unity

## GOAL

<One sentence describing the feature>

Example:
Add a basic enemy patrol AI system.

---

## CONTEXT

Engine: Unity  
Language: C#  

Agents must read the following reference documents before making changes:

AI/AGENTS.md  
AI/ARCHITECTURE.md  
AI/CODE_STYLE.md  
AI/UNITY_INVARIANTS.md  
AI/GAME_DESIGN.md  
AI/MECHANICS_SPEC.md  
AI/REVIEW_CHECKLIST.md  
AI/PROJECT_MEMORY.md

These files define project architecture, coding standards, and gameplay rules.

Agents must follow them strictly.

---

## SCOPE

Allowed:

Assets/Scripts/**

Agents may:

- create new scripts
- modify existing scripts
- refactor gameplay systems

Forbidden:

ProjectSettings/**  
Packages/**  
.meta files  
Unity generated files  

Agents must not modify engine configuration or package dependencies.

---

## CHANGES REQUIRED

The agent should:

1. Review relevant scripts before implementing changes.
2. Create new scripts only when necessary.
3. Follow the architecture defined in `ARCHITECTURE.md`.
4. Ensure code follows `CODE_STYLE.md`.
5. Avoid creating duplicate systems.
6. Prefer modular components over large multi-responsibility classes.

---

## PROCESS

The agent must follow this process:

1. Analyze the GOAL.
2. Review reference documentation.
3. Check PROJECT_MEMORY.md for existing systems.
4. Inspect relevant scripts in the repository.
5. Implement the feature.
6. Perform SELF REVIEW.
7. Revise if necessary.
8. Produce final deliverable.

---

## SELF REVIEW

Before returning the final result, the agent must perform a review pass.

The solution must be checked against:

AI/AGENTS.md  
AI/ARCHITECTURE.md  
AI/CODE_STYLE.md  
AI/UNITY_INVARIANTS.md  
AI/UNITY_ANTI_PATTERNS.md
AI/REVIEW_CHECKLIST.md  
AI/GAME_DESIGN.md  
AI/MECHANICS_SPEC.md
AI/IMPLEMENTATION_PATTERNS.md
AI/DEBUGGING_GUIDELINES.md

During the review pass the agent must verify:

Architecture
- Scripts are placed in correct folders
- No duplicate systems were introduced

Coding Standards
- PascalCase used for classes and public members
- camelCase used for private fields and variables
- Boolean variables read like questions

Unity Performance
- No GameObject.Find in Update
- No repeated GetComponent calls
- Component references cached in Awake or Start
- No empty Update methods

Gameplay Consistency
- Implementation matches GAME_DESIGN.md
- Mechanics follow MECHANICS_SPEC.md

If any violations are found, the agent must revise the implementation before returning the final answer.

---

## OUTPUT

Return results using the following structure.

### Summary
Short explanation of the implemented feature.

### Files Created
List new files with paths.

Example:
Assets/Scripts/Enemies/EnemyPatrolAI.cs

### Files Modified
List modified files.

### Key Implementation Notes
Important design decisions or architecture choices.

### Unity Setup Instructions
Steps required inside the Unity editor.

Example:
1. Attach EnemyPatrolAI to enemy prefab
2. Assign waypoint transforms

### Testing Steps
Instructions to verify functionality in Play Mode.