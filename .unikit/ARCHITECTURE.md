# Architecture: Feature-based / Functional Modules

## Project

Это демонстрационный проект карточной игры с элементами RPG и продвинутым игровым ИИ, где юниты (Piece) принимают решения на основе анализа ситуации. Целевая платформа — Windows (Standalone).

## Tech Stack

| Category | Technology |
|----------|-----------|
| Engine | Unity 6000.0.58f2 / C# 12 / URP |
| DI | None (Manual Dependency Injection) |
| UI | uGUI with MVP (conceptual) |
| Input | Unity Input System |
| AI | SituationAnalyzer (Weight-based decision making) |
| Async | Unity Awaitable (C# 12 async/await) |
| Navigation | AI Navigation (NavMesh) |
| Camera | Cinemachine |

## Documentation Sources

When you need up-to-date API docs for these libraries:

- **Unity 6 Manual** — [Documentation lookup for Unity engine features]
- **uGUI & UI Toolkit** — [Standard Unity UI documentation]
- **AI Navigation** — [Unity AI Navigation package documentation]
- **Cinemachine** — [Cinemachine documentation]

## Architecture Overview

Проект организован по принципу **функциональных модулей** (Feature-based). Код разделен на независимые папки по игровым системам (AI, Battleground, Units), что упрощает навигацию и развитие отдельных фич.

В основе логики лежат **стейт-машины** (`PlayerStateMachine`, `PieceStateMachine`), которые управляют жизненным циклом игрока и юнитов. ИИ реализован через систему `SituationAnalyzer`, которая вычисляет веса (DangerWeight) для окружающих объектов, позволяя юнитам динамически менять состояния (`PieceState`).

## Folder Structure

```
Assets/Scripts/
├── AI/                 # Система анализа ситуации и взвешенных точек (IAIWeightPoint)
├── Battleground/       # Основная логика боя и управления миром
│   ├── Piece/          # Сущности на поле боя (Piece, Animator, Mover, Attributes)
│   ├── Player/         # Логика игрока, хранение карт и стейт-машина игрока
│   ├── StateMachines/  # Общие стейт-машины (GameState, PlayerState)
│   └── UI/             # Интерфейс боя (Карты, Меню, Таймлайн)
├── Units/              # Данные юнитов (Unit, Attributes, Inventory, Race, Class)
├── UI/                 # Общие компоненты интерфейса (ProgressBars, Markers)
├── Guild/              # (Зарезервировано) Системы гильдий или мета-игры
├── Spells/             # Система заклинаний и их логика
└── Timeline/           # Расширения и скрипты для Unity Timeline
```

## Dependency Rules

- **Units** является базовым модулем данных и не должен зависеть от других игровых систем.
- **Battleground** зависит от **Units** для инициализации существ (`Piece` создается из `Unit`).
- **AI** (SituationAnalyzer) является кросс-системным модулем, использующим интерфейс `IAIWeightPoint`.
- **UI** зависит от **Battleground** и **Units** для визуализации состояния игры.

- ✅ `Battleground` → `Units`
- ✅ `Piece` → `SituationAnalyzer`
- ❌ `Units` → `Battleground` (нарушение чистоты данных)
- ❌ `AI` → `Piece` (должен работать через интерфейсы)

## Module Boundary Strategy

В проекте используются папки для разделения модулей. Boundary-файлы (`.asmdef`) на данный момент отсутствуют, поэтому зависимости контролируются на уровне неймспейсов (`Units`, `Battleground`, `AI`).

- Публичные API модулей открыты.
- Названия неймспейсов соответствуют структуре папок.
- Рекомендуется внедрение `.asmdef` для жесткого контроля зависимостей при росте проекта.

## Cross-Module Communication

- **Interfaces:** `IAIWeightPoint` позволяет ИИ анализировать любые объекты без прямой зависимости от их классов. `IDamageable` для системы урона.
- **State Machines:** Передача состояния и событий между системами через смену стейтов.
- **Direct References:** Контроллеры (`BattleManager`, `Player`) хранят ссылки на свои подсистемы.

## Key Principles

1. **Composition over Inheritance:** Сущность `Piece` собирается из множества компонентов (`PieceMover`, `PieceAnimator`, `PieceAttributes`).
2. **State-Driven Logic:** Логика поведения вынесена в стейт-машины, что упрощает отладку и расширение.
3. **Data/Logic Separation:** Данные юнитов (`Unit`) отделены от их физического воплощения на поле боя (`Piece`).
4. **Weight-Based AI:** ИИ не использует жесткие условия, а опирается на метрики и веса окружения.

## Anti-Patterns

- ❌ **Fat MonoBehaviour:** Не добавляйте всю логику в один скрипт. Используйте компоненты и Plain C# классы.
- ❌ **Hard Coupling:** Избегайте прямых ссылок между несвязанными модулями (например, `Unit` не должен знать о `UI`).
- ❌ **Async Void:** Всегда используйте `Awaitable` или `UniTaskVoid` (если будет добавлено) для асинхронных операций.

## Detailed Rules

Для получения подробных правил по стилю кода и реализации конкретных систем см.:

- **`.unikit/RULES.md`** — Правила именования и оформления кода (C#).
- **`.unikit/memory/RULES_INDEX.md`** — Индекс всех правил для конкретных фреймворков (uGUI, Input System, URP).
