---
version: 1.0.0
---

# Unity uGUI (Unity UI)

> **Scope**: Unity's standard UI system (uGUI) — Canvas management, RectTransform layout, UI components (Image, TextMeshPro, Button, etc.), event handling, and optimization techniques.
> **Load when**: creating or modifying UI screens, optimizing UI performance, handling UI events, setting up responsive layouts, or debugging UI-related issues.

---

## Core Concepts

**Canvas** — the root component for all UI elements.
- **Screen Space - Overlay**: Renders on top of everything. No camera required.
- **Screen Space - Camera**: Renders in front of a specific camera. Supports perspective and particle systems between UI layers.
- **World Space**: Renders as a 3D object in the scene. Useful for in-game HUDs or holographic displays.

**RectTransform** — a 2D version of Transform.
- **Anchors**: Define how the element positions/sizes relative to its parent.
- **Pivot**: The point around which the element rotates and scales.
- **Offset/SizeDelta**: Relative size based on anchors.

**Canvas Scaler** — controls how UI scales across different resolutions.
- **Constant Pixel Size**: Stays same size regardless of resolution.
- **Scale With Screen Size**: Scales based on a reference resolution (usually preferred for cross-platform).

**Graphic Raycaster** — processes input for UI elements. Elements must have **Raycast Target** enabled to receive events.

---

## API / Interface

```csharp
using UnityEngine.UI;
using TMPro; // Standard for modern Unity text

public class MyUIElement : MonoBehaviour
{
    [SerializeField] private Button _myButton;
    [SerializeField] private TextMeshProUGUI _myText;
    [SerializeField] private Image _myIcon;

    void Awake()
    {
        _myButton.onClick.AddListener(OnButtonClicked);
    }

    public void SetText(string content) => _myText.text = content;
    public void SetColor(Color color) => _myIcon.color = color;

    private void OnButtonClicked()
    {
        Debug.Log("Button Clicked!");
    }
}
```

**Common Components:**
| Component | Description | Key Property |
|---|---|---|
| `Image` | Displays sprites/colors | `sprite`, `type` (Simple, Sliced, Tiled, Filled) |
| `TextMeshProUGUI` | High-quality text rendering | `text`, `font`, `fontSize` |
| `Button` | Handles click events | `onClick` |
| `Toggle` | Checkbox / Radio button | `onValueChanged` |
| `Slider` | Value selection range | `value`, `onValueChanged` |
| `ScrollRect` | Scrollable content area | `content`, `viewport`, `normalizedPosition` |

---

## Setup Patterns

### Pattern A — Sliced Images (9-Slicing)

To maintain sharp corners while scaling UI boxes:
1. Select the Sprite in Project view.
2. Open **Sprite Editor**.
3. Set the borders (L, R, T, B).
4. Set the Image Component's **Image Type** to **Sliced**.

### Pattern B — Layout Groups

Use for automatic positioning of multiple elements:
- `VerticalLayoutGroup` / `HorizontalLayoutGroup`: Stack elements linearly.
- `GridLayoutGroup`: Arrange in a grid.
- `ContentSizeFitter`: Resize the parent automatically based on child sizes (pair with Layout Groups).

### Pattern C — UI Prefabs

Always create reusable UI elements (Buttons, Tooltips, Modals) as Prefabs. This ensures consistency and makes global updates easier.

---

## Patterns & Examples

### Responsive UI with Anchors

- **Full Screen**: Min (0,0), Max (1,1), Offsets (0,0,0,0).
- **Bottom Right**: Min (1,0), Max (1,0), Pivot (1,0).
- **Top Stretch**: Min (0,1), Max (1,1), Pivot (0.5, 1).

### Simple UI Controller

```csharp
public abstract class UIWindow : MonoBehaviour
{
    public virtual void Show() => gameObject.SetActive(true);
    public virtual void Hide() => gameObject.SetActive(false);
}

public class MainMenu : UIWindow
{
    [SerializeField] private Button _startButton;
    
    void Awake() => _startButton.onClick.AddListener(StartGame);
    
    private void StartGame()
    {
        // Handle logic
        Hide();
    }
}
```

---

## Configuration

**Canvas Scaler Recommended Settings:**
- **UI Scale Mode**: `Scale With Screen Size`
- **Reference Resolution**: e.g., `1920 x 1080`
- **Screen Match Mode**: `Match Width Or Height`
- **Match**: `0.5` (Balances both width and height scaling)

**TextMesh Pro:**
- Always use `TextMeshProUGUI` instead of the legacy `Text` component.
- Use **SDF (Signed Distance Field)** fonts for crisp text at any scale.

---

## Best Practices

- **Disable 'Raycast Target'** on all non-interactive UI elements (Images, Text) to reduce Graphic Raycaster overhead.
- **Split Canvases**: Put frequently changing (dirtying) elements on a separate nested Canvas. When one element changes, only that Canvas rebuilds, not the entire UI.
- **Hide UI by disabling the Canvas component** (or using a Canvas Group) instead of calling `SetActive(false)` if the UI is complex. This avoids the cost of `OnEnable` / `Start` when re-enabling.
- **Use Sprite Atlases**: Pack UI sprites together to reduce draw calls (Batches).
- **Prefer CanvasGroup Alpha** for fading rather than animating Image colors individually.

---

## Common Pitfalls

### The "All-in-one" Canvas
Putting the entire game's UI on one root Canvas causes a full UI rebuild every time a single value (like a health bar or timer) changes.

### Legacy Text
Using `UnityEngine.UI.Text` leads to blurry text and poor performance compared to TextMesh Pro.

### Overuse of Layout Groups
Layout groups recalculate every frame if elements are dirty. Avoid nesting them too deeply or using them for static layouts.

### Pixel Perfect on moving UI
Enabling `Pixel Perfect` on a Canvas that moves or scales frequently can cause jittering and performance drops.

---

## Anti-patterns

- **Never use `GameObject.Find` or `GetComponent`** every frame to update UI. Cache references in `Awake`.
- **Avoid `Update()` for UI logic**: Use events (`onValueChanged`, `onClick`) or a reactive pattern to update UI only when data changes.
- **Don't use empty Images as raycast targets**: If you need a click area without a graphic, use a `Graphic` subclass that overrides `OnPopulateMesh` to do nothing, or use a transparent Image with `Alpha Hit Test Minimum Threshold`.
