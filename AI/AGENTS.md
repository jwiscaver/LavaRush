# AI Agent Operating Rules — Unity Project

Agents must read this file before modifying the project.

## Project Overview

Engine: Unity
Language: C#
Architecture: MonoBehaviour + modular gameplay systems

The goal is to build maintainable gameplay systems rather than one-off scripts.

Agents must prioritize:

- readability
- modularity
- stability in Play Mode
- avoiding runtime allocations where possible

---

## Allowed Operations

Agents MAY:

- create new scripts
- modify scripts inside `Assets/Scripts`
- refactor existing gameplay systems
- add helper utilities

Agents MUST NOT:

- modify Unity generated files
- modify `.meta` files
- modify `ProjectSettings`
- modify `Packages/manifest.json`

---

## Required Behavior

Before making changes the agent must:

1. Read ARCHITECTURE.md
2. Review related scripts
3. Ensure new code follows CODE_STYLE.md
4. Avoid duplicating existing systems