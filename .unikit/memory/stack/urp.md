---
version: 1.0.0
---

# Universal Render Pipeline (URP)

> **Scope**: Universal Render Pipeline package (com.unity.render-pipelines.universal) — pipeline assets, renderer features, Shader Graph, Volume framework, and post-processing.
> **Load when**: configuring rendering settings, creating custom shaders with Shader Graph, implementing custom Render Features, setting up post-processing Volumes, or optimizing rendering performance (SRP Batcher, draw calls).

---

## Core Concepts

**Universal Render Pipeline Asset** — The main configuration file for the pipeline. It defines global settings like shadow distance, MSAA, and which Renderer to use. The project uses multiple assets for different quality levels: `URP-Balanced`, `URP-HighFidelity`, and `URP-Performant`.

**Universal Renderer Data** — Defines how a frame is rendered. It holds the list of **Scriptable Render Features**. Each URP Asset points to a Renderer Data asset (e.g., `URP-Balanced-Renderer`).

**Scriptable Render Features** — Allow extending the URP with custom passes (e.g., blits, custom outlines, decals). Added to the Renderer Data asset.

**Volume Framework** — A system for overriding or blending scene-wide settings (Post-processing, Fog, Shadows).
- **Global Volume**: Affects the whole scene.
- **Local Volume**: Affects a specific area defined by a Collider.
- **Volume Profile**: A ScriptableObject containing the actual overrides (e.g., `DefaultVolumeProfile`).

**Shader Graph** — The primary tool for creating shaders in URP. Prefer it over hand-coded HLSL unless complex low-level optimizations or specific features (like Geometry Shaders) are required.

---

## API / Interface

### Camera Data
Access URP-specific camera data via the `GetUniversalAdditionalCameraData()` extension method.

```csharp
using UnityEngine.Rendering.Universal;

var cameraData = Camera.main.GetUniversalAdditionalCameraData();
cameraData.renderShadows = false; // Disable shadows for this camera
cameraData.antialiasing = AntialiasingMode.SubpixelMorphologicalAntialiasing;
```

### Volume Management
Programmatically adjusting Volume weights or profiles.

```csharp
using UnityEngine.Rendering;

public Volume sceneVolume;
public VolumeProfile highQualityProfile;

void SetHighQuality()
{
    sceneVolume.profile = highQualityProfile;
    sceneVolume.weight = 1.0f;
}
```

### Render Pipeline Callbacks
Use `RenderPipelineManager` to hook into the rendering lifecycle.

```csharp
void OnEnable() => RenderPipelineManager.beginCameraRendering += OnBeginCamera;
void OnDisable() => RenderPipelineManager.beginCameraRendering -= OnBeginCamera;

void OnBeginCamera(ScriptableRenderContext context, Camera camera)
{
    // Logic before rendering this camera
}
```

---

## Setup Patterns

### Creating a Custom Render Feature
1. Create a class inheriting from `ScriptableRenderFeature`.
2. Implement `Create()` to initialize your pass.
3. Implement `AddRenderPasses()` to enqueue the pass into the renderer.

```csharp
public class MyCustomFeature : ScriptableRenderFeature
{
    class MyCustomPass : ScriptableRenderPass { /* ... */ }

    MyCustomPass _myPass;

    public override void Create()
    {
        _myPass = new MyCustomPass();
        _myPass.renderPassEvent = RenderPassEvent.AfterRenderingTransparents;
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        renderer.EnqueuePass(_myPass);
    }
}
```

### Post-Processing Overrides
1. Add a **Volume** component to a GameObject.
2. Create or assign a **Volume Profile**.
3. Click **Add Override** to add effects like Bloom, Vignette, or Color Grading.

---

## Best Practices

- **Enable SRP Batcher**: Always keep "SRP Batcher" enabled in the URP Asset for significant draw call optimization. It works by batching draw calls for shaders that use the same "SetPass" call.
- **Use Shader Graph**: It is the native and most optimized way to write shaders for URP.
- **Minimize Render Features**: Each render feature can add overhead. Disable features like "Opaque Texture" or "Depth Texture" in the URP Asset if they are not used.
- **Volume Profile Re-use**: Share Volume Profiles across scenes to maintain a consistent look and reduce asset duplication.
- **LOD Cross-fade**: Use the "LOD Cross Fade" setting in the URP Asset for smoother transitions between LOD levels.
- **Shadow Distance**: Tune "Shadow Distance" per quality level (e.g., 50 for Balanced, 100 for HighFidelity) to balance performance and visuals.

---

## Common Pitfalls

### Using Built-in Shaders
URP **does not support** standard "Built-in" shaders (e.g., `Standard`, `Mobile/Diffuse`). Materials using these will appear with a pink "error" shader. Use `Universal Render Pipeline/Lit` or `Simple Lit` instead.

### Missing Volume Layer Mask
If a Global Volume isn't affecting the camera, check if the Camera's **Volume Mask** includes the layer of the Volume GameObject.

### Post-processing not enabled on Camera
Each Camera has a **Post Processing** checkbox in its Inspector. If unchecked, no Volume effects will be applied.

### SRP Batcher Compatibility
Custom HLSL shaders must wrap their properties in a `CBUFFER_START(UnityPerMaterial)` block to be compatible with the SRP Batcher.

```hlsl
CBUFFER_START(UnityPerMaterial)
    float4 _BaseColor;
    float _Metallic;
CBUFFER_END
```

---

## Anti-patterns

- **Never use `Camera.main` in a rendering loop**: It's expensive. Cache the camera reference or use the camera provided in the `RenderingData`.
- **Avoid `OnPreRender` / `OnPostRender`**: These are Built-in pipeline callbacks and do not work in URP. Use `RenderPipelineManager` events instead.
- **Don't over-use Local Volumes**: Too many overlapping local volumes can complicate blending and hurt performance. Use them sparingly for specific area-based mood changes.
- **Avoid "Always" updates for Volumes**: Set "Volume Framework Update Mode" to `Every Frame` only if you are animating volume parameters via code/timeline.
