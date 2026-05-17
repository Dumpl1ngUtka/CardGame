---
version: 1.0.0
---

# Unity AI Navigation

> **Scope**: Unity AI Navigation package (com.unity.ai.navigation) — NavMeshSurface, NavMeshAgent, NavMeshLink, NavMeshObstacle, NavMeshModifier, runtime baking, pathfinding, and agent configuration.
> **Load when**: implementing AI movement, pathfinding, or environment navigation; setting up NavMesh surfaces; handling dynamic obstacles; wiring agent logic; or debugging navigation issues.

---

## Core Concepts

**NavMeshSurface** — The primary component for defining and baking NavMesh. It replaces the old Window-based baking. Supports multiple surfaces per scene and runtime baking.

**NavMeshAgent** — Component for moving objects on the NavMesh. Handles pathfinding, avoidance, and movement.

**NavMeshLink** — Connects two separate NavMesh surfaces or locations (e.g., for jumping, teleports, or ladder climbing).

**NavMeshObstacle** — Used for objects that agents should avoid. Can be static (carved into NavMesh) or dynamic (avoided at runtime).

**NavMeshModifier** — Fine-tunes how specific objects affect NavMesh generation (e.g., making an object non-walkable or assigning a specific area type).

---

## API / Interface

```csharp
using UnityEngine.AI;
using Unity.AI.Navigation;

// NavMeshAgent basic movement
agent.SetDestination(targetPosition);
agent.isStopped = true; // Pause movement
agent.ResetPath();      // Clear current path

// Checking status
if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
{
    // Reached destination
}

// Runtime Baking (NavMeshSurface)
navMeshSurface.BuildNavMesh();
navMeshSurface.UpdateNavMesh(navMeshSurface.navMeshData);

// Path Querying without Agent
NavMeshPath path = new NavMeshPath();
if (NavMesh.CalculatePath(startPos, endPos, NavMesh.AllAreas, path))
{
    // Path found
}

// Sample Position (Snap to NavMesh)
NavMeshHit hit;
if (NavMesh.SamplePosition(origin, out hit, 1.0f, NavMesh.AllAreas))
{
    Vector3 snappedPos = hit.position;
}
```

**Key NavMeshAgent members:**
| Member | Type | Notes |
|---|---|---|
| `destination` | `Vector3` | Target position in world space |
| `remainingDistance` | `float` | Distance to target |
| `stoppingDistance` | `float` | Stop when within this range |
| `velocity` | `Vector3` | Current movement velocity |
| `isStopped` | `bool` | Toggle to pause/resume movement |
| `updatePosition` | `bool` | If false, agent doesn't move the Transform |
| `updateRotation` | `bool` | If false, agent doesn't rotate the Transform |

---

## Setup Patterns

### Pattern A — Scene-wide Navigation

1. Create an empty GameObject named "NavMesh".
2. Add `NavMeshSurface` component.
3. Configure **Collect Objects** (e.g., "All" or "Volume").
4. Configure **Include Layers**.
5. Click **Bake**.

### Pattern B — Moving Agent with Animator

Syncing Agent and Animator is critical for root motion or specific animations.

```csharp
public class AgentAnimatorSync : MonoBehaviour
{
    private NavMeshAgent _agent;
    private Animator _animator;

    void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        // If using root motion, let animator handle position
        // _agent.updatePosition = false; 
    }

    void Update()
    {
        float speed = _agent.velocity.magnitude / _agent.speed;
        _animator.SetFloat("Speed", speed);
    }
}
```

### Pattern C — Runtime Dynamic Baking

```csharp
public class DynamicNavMesh : MonoBehaviour
{
    public NavMeshSurface surface;

    public void Refresh()
    {
        // Expensive: use sparingly or with LocalNavMeshBuilder
        surface.BuildNavMesh(); 
    }
}
```

---

## Patterns & Examples

### Area Masks & Costs

Use area types for different terrains (e.g., "Water" with high cost, "Road" with low cost).

```csharp
// Change which areas an agent can traverse
int waterArea = NavMesh.GetAreaFromName("Water");
int mask = 1 << waterArea;
agent.areaMask &= ~mask; // Disable water
```

### Off-Mesh Links (NavMeshLink)

Use `NavMeshLink` for non-contiguous navigation. Ensure "Auto Update Positions" is checked if the link moves.

---

## Best Practices

- **Use NavMeshSurface** instead of the legacy `Navigation` window. It provides much more control and is the standard for modern Unity.
- **Prefer `NavMeshObstacle` with "Carve"** for stationary objects that appear at runtime.
- **Keep `stoppingDistance` slightly above 0** to prevent agents from jittering at their exact destination.
- **Avoid calling `SetDestination` every frame**. It triggers path recalculation which is expensive. Use a timer or threshold.
- **Use `NavMesh.SamplePosition`** to ensure a target point is actually on the NavMesh before sending an agent there.
- **Separate Navigation from Visuals**. Use simple colliders or specific layers for NavMesh collection to keep the mesh clean and efficient.

---

## Common Pitfalls

### Agent "Teleporting" or Jittering
Often caused by the Agent fighting with a Rigidbody or root motion. Disable `updatePosition` if you manually sync the Transform.

### NavMesh not baking on specific objects
Ensure the objects have a `MeshRenderer` or `Terrain` component and are on a layer included in the `NavMeshSurface` settings.

### Path Pending
`agent.remainingDistance` is not reliable immediately after calling `SetDestination`. Check `agent.pathPending` first.

```csharp
agent.SetDestination(target);
// WAIT until next frame or check pathPending
if (!agent.pathPending && agent.remainingDistance < 0.1f) { ... }
```

### Obstacle Carving Performance
Large numbers of carving `NavMeshObstacle` components can cause performance spikes during NavMesh updates. Group static obstacles where possible.

---

## Anti-patterns

- **Never use `NavMesh.AllAreas` in every query** — be specific with masks to optimize pathfinding.
- **Never ignore `NavMesh.SamplePosition`** when spawning agents — spawning off-mesh can lead to agents getting stuck or failing to find paths.
- **Don't use `NavMeshAgent.nextPosition` incorrectly** — if `updatePosition` is false, you MUST sync `transform.position` to `agent.nextPosition` or vice versa depending on your movement strategy.
