# Rules Index

Knowledge base rules for the project. Located in `.unikit/memory/`.

## How to use this index

You were directed here by a skill or subagent. The name after "instructions for" in that directive is **your identity** — use it when checking the Required By column below.

### Override Priority (highest wins)

1. **`.unikit/RULES.md`** — project-specific overrides (always wins)
2. **`.unikit/ARCHITECTURE.md`** — project architecture decisions
3. **Core rules** (`.unikit/memory/core/`) — universal best practices
4. **Stack rules** (`.unikit/memory/stack/`) — framework-specific knowledge

When a project rule in RULES.md or ARCHITECTURE.md conflicts with a core or stack rule, the project rule wins.

### Step 1: Load RULES.md
Read `.unikit/RULES.md` before loading any rule below. It contains project-specific overrides that take highest priority.

### Step 2: Load Core rules
For each row in the Core table, check the **Required By** column:
- `all` → **MUST load** (mandatory for every skill and subagent)
- Contains your name → **MUST load**
- Does NOT contain your name and is NOT `all` → **skip**

### Step 3: Load Stack rules (on demand)
Load ONLY when the current task involves the framework described in the **Load When** column.

## Core (`.unikit/memory/core/`)

| File | Description | Required By | Load When |
|------|-------------|-------------|-----------|
| code-style.md | Universal C#/Unity code style conventions — naming, access modifiers, member ordering, class structure, formatting, component approach, documentation. | all | writing or reviewing any C# code, creating new classes, checking code style. |
| design-principles.md | Universal software design principles — SOLID, GRASP, KISS, DRY, inheritance guidelines, SRP decision framework, method design, defensive programming. | all | designing systems, creating new classes, choosing architecture patterns, deciding when to split or merge classes, method design, defensive programming. |
| folders-structure.md | Project folder organization, module structure, namespace conventions, external asset boundaries | all | Creating new scripts, choosing file location, setting namespace, creating folders, module structure, folder layout |
| performance.md | Rules for performance optimization — memory/GC, caching, ZLinq, strings, object pooling, delegates, math, physics, UI optimization, mobile specifics. | all | performance issues, optimization, hot paths, memory/GC, pooling, ZLinq. |
| testing.md | Rules for NUnit unit tests — AAA pattern, test class structure, naming, test doubles (Fake/Stub/Mock), parameterized tests, boundary conditions, assembly definitions, ScriptableObject in tests, PlayMode tests. | all | writing or reviewing unit tests, creating test doubles, setting up test assemblies. |

## Stack (`.unikit/memory/stack/`)

| File | Description | Load When |
|------|-------------|-----------|
| ai-navigation.md | Unity AI Navigation package (com.unity.ai.navigation) — NavMeshSurface, NavMeshAgent, NavMeshLink, NavMeshObstacle, NavMeshModifier, runtime baking, pathfinding, and agent configuration. | implementing AI movement, pathfinding, or environment navigation; setting up NavMesh surfaces; handling dynamic obstacles; wiring agent logic; or debugging navigation issues. |
| input-system.md | Unity Input System package (com.unity.inputsystem) — action setup and lifecycle, callback patterns, InputActionAsset and generated wrappers, PlayerInput component, control schemes, runtime rebinding, and update mode configuration. | handling player input with the new Input System, creating InputActions or InputActionAssets, wiring input callbacks, setting up control schemes or device switching, implementing runtime rebinding, debugging input not firing, choosing between PlayerInput and manual action management. |
| ugui.md | Unity's standard UI system (uGUI) — Canvas management, RectTransform layout, UI components (Image, TextMeshPro, Button, etc.), event handling, and optimization techniques. | creating or modifying UI screens, optimizing UI performance, handling UI events, setting up responsive layouts, or debugging UI-related issues. |
| urp.md | Universal Render Pipeline package (com.unity.render-pipelines.universal) — pipeline assets, renderer features, Shader Graph, Volume framework, and post-processing. | configuring rendering settings, creating custom shaders with Shader Graph, implementing custom Render Features, setting up post-processing Volumes, or optimizing rendering performance (SRP Batcher, draw calls). |
