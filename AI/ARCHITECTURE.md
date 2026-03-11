# Unity Project Architecture

All gameplay code lives in:

Assets/Scripts

Structure:

Assets/Scripts
    Core
    Player
    Enemies
    Systems
    UI
    Utilities

---

## Core

Fundamental gameplay logic shared across the project.

Examples:

GameManager
SaveSystem
EventBus

---

## Player

Scripts controlling the player.

Examples:

PlayerController
PlayerCombat
PlayerInventory

---

## Enemies

AI behavior.

Examples:

EnemyAI
EnemySpawner
EnemyStats

---

## Systems

Reusable gameplay systems.

Examples:

InventorySystem
DialogueSystem
QuestSystem
LootSystem

---

## UI

Menu and interface logic.

Examples:

HealthBarUI
InventoryUI
DialogueUI

---

## Utilities

Helper scripts and shared tools.

Examples:

MathHelpers
Extensions
Timer