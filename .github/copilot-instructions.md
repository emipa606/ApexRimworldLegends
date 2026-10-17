# Copilot Instructions for Apex: Rimworld Legends (Continued)

## Mod Overview and Purpose

### Overview
Apex: Rimworld Legends (Continued) is a mod created for the game RimWorld, aimed at enhancing the weapon variety through the introduction of over 20 new weapons inspired by the game Apex: Legends. The mod is designed to blend seamlessly into the vanilla game environment, keeping all added weapons in line with the look and feel of unmodded RimWorld.

### Purpose
The primary purpose of the mod is to provide players with a richer combat experience by expanding the selection of weapons available. This includes late-game Mechanoid Weapons that are craftable after researching advanced technologies. The mod accommodates player feedback to adjust weapon attributes, ensuring a balanced gameplay experience.

## Key Features and Systems

- **Vanilla-Friendly Weapon Expansion:** New weapons that fit the RimWorld style. Includes weapons such as the Bullpup Rifle, Hand Cannon, and Mozambique.
- **Charge Weapons Variety:** Expanded charge weapon options for late game, not limited to just the Charge rifle.
- **Mechanoid Weapons:** A new tier of high-tech weapons available after advanced research.
- **Combat Extended Compatibility:** The mod includes integrated patches for compatibility with Combat Extended.
- **Dynamic Weapon Updates:** Commitment to keep adding new weapons as Apex: Legends updates are released.

## Coding Patterns and Conventions

- **C# Classes and Methods:** Definitions are structured in meaningful classes that encapsulate specific behaviors and data processes. For instance, classes like `OPPowerBeam` and `OPOrbitalBeam` handle intricate parts of weapon operations.
- **Descriptive Naming:** Classes, methods, and members utilize clear and descriptive naming to ensure readability and maintainability.
- **Single Responsibility Principle:** Aim to keep each class and method focused on a single task to increase coherence and ease of understanding.

## XML Integration

- **Defining Game Objects:** XML files are used extensively to define new game objects such as weapons and sounds, with examples including `ARL_Grenades.xml`, `ARL_Weapons.xml`, and `Defs_OPToxin.xml`.
- **Separation of Concerns:** Keep XML definitions organized by function, e.g., separate files for DamageDefs, SoundDefs, and ThingDefs.
- **Modular Structure:** Arrange XML files in a way that allows for easy updates and extensions.

## Harmony Patching

- **Purpose of Harmony:** Patching is crucial for altering or extending the behavior of the original game classes without modifying the original source code.
- **Patch Examples:** Specific examples of Harmony patches should follow best practices ensuring patches are conditionally applied and checked for potential conflicts.
- **Documentation:** Ensure patches are well-documented to facilitate ease of understanding by other modders and community contributors.

## Suggestions for Copilot

- **Helper Functions:** Consider suggesting reusable helper functions for repeated operations like damage calculations or sound initialization.
- **Patch Templates:** Provide templates for setting up harmony patches, ensuring correct prefix and postfix patches are in place.
- **Compile-Time Checks:** Encourage use of compile-time assertions to catch potential issues in weapon statistics and definitions before runtime.
- **Code Commenting:** Suggest comments for new code segments that describe the purpose and functionality succinctly to maintain understanding and flow for other developers.

By following these guidelines, Copilot can help produce more efficient and consistent coding efforts for continued development and maintenance of Apex: Rimworld Legends (Continued).

## Project Solution Guidelines
- Relevant mod XML files are included as Solution Items under the solution folder named XML, these can be read and modified from within the solution.
- Use these in-solution XML files as the primary files for reference and modification.
- The `.github/copilot-instructions.md` file is included in the solution under the `.github` solution folder, so it should be read/modified from within the solution instead of using paths outside the solution. Update this file once only, as it and the parent-path solution reference point to the same file in this workspace.
- When making functional changes in this mod, ensure the documented features stay in sync with implementation; use the in-solution `.github` copy as the primary file.
- In the solution is also a project called Assembly-CSharp, containing a read-only version of the decompiled game source, for reference and debugging purposes.
- For any new documentation, update this copilot-instructions.md file rather than creating separate documentation files.


## Hard rules (must follow)
- Do NOT run commands that modify the repo (no git commit, git apply, dotnet format) unless explicitly asked.
- Prefer minimal reads: read only the smallest code region needed (around the suspicious lines).
- When mentioning SonarQube issues, automatically use the SonarQube MCP service to fetch and address issues instead of making inferred fixes without querying SonarQube first.
- When mentioning the rimworld log, automatically use the Rimworld MCP service to fetch the log.

