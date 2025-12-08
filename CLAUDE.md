# MergeBirds - AI Assistant Guide

## Project Overview

**MergeBirds** is a Unity-based 2D merge game where players merge different types of birds to progress through the game. The project uses Unity 6000.2.6f2 with the Universal Render Pipeline (URP) for 2D graphics.

### Game Concept
- Merge-style puzzle game mechanic (similar to merge games like 2048)
- Players drag and drop birds on a grid
- Merging two identical birds creates a higher-level bird
- Grid-based cell system for item placement

## Technology Stack

### Unity Version
- **Unity Editor**: 6000.2.6f2
- **Platform**: 2D Game (URP 2D)
- **Render Pipeline**: Universal Render Pipeline 17.2.0

### Key Unity Packages
- `com.unity.2d.animation` (12.0.2) - 2D sprite animation
- `com.unity.inputsystem` (1.14.2) - New Unity Input System
- `com.unity.render-pipelines.universal` (17.2.0) - URP rendering
- `com.unity.ai.assistant` (1.0.0-pre.12) - Unity AI Assistant
- `com.unity.ai.generators` (1.0.0-pre.20) - AI asset generation
- `com.unity.ai.inference` (2.4.1) - AI inference capabilities
- `com.unity.ugui` (2.0.0) - Unity UI system

## Project Structure

```
MergeBirds/
├── Assets/
│   ├── Animation/           # Animator controllers and animation clips
│   │   ├── Bird.controller  # Main bird animator controller
│   │   ├── Pegeon.anim      # Pigeon animation
│   │   └── Sparrow.anim     # Sparrow animation
│   ├── Art/                 # Sprites and visual assets
│   │   ├── Animations/      # Sprite sheets for animations
│   │   ├── Parrot.png
│   │   ├── Pegeon.png
│   │   ├── sparrow.png
│   │   └── card.png
│   ├── Birds/               # ScriptableObject bird definitions
│   │   └── Pegeon.asset     # Example bird data
│   ├── Prefabs/             # Reusable game objects
│   │   └── Cell.prefab      # Grid cell prefab
│   ├── Scenes/              # Unity scenes
│   │   ├── Game.unity       # Main game scene
│   │   └── SampleScene.unity
│   ├── Scripts/             # C# source code (see below)
│   └── Settings/            # URP and rendering settings
├── GeneratedAssets/         # AI-generated assets storage
├── Packages/                # Unity package management
└── ProjectSettings/         # Unity project configuration
```

## Code Architecture

### Core Scripts (`Assets/Scripts/`)

#### 1. **Singleton.cs**
- Generic singleton pattern implementation
- Thread-safe with proper lifecycle management
- Handles DontDestroyOnLoad for persistence
- **Usage**: Base class for manager classes that need single instance

```csharp
public class MyManager : Singleton<MyManager>
{
    protected override void Awake()
    {
        base.Awake(); // Always call base.Awake() first!
        // Your initialization code
    }
}
```

#### 2. **ItemInfo.cs**
- ScriptableObject for defining bird/item data
- **Properties**:
  - `icon`: Sprite for visual representation
  - `income`: Value/currency earned from this bird
  - `number`: Bird level/tier identifier
  - `soundOnSpawn`: Audio clip when bird appears
- **Location**: Create new birds via `Create > Scriptable Objects > ItemInfo`

#### 3. **Bird.cs**
- MonoBehaviour for bird visual representation
- **Methods**:
  - `SetBird(int id)`: Initializes bird with specific ID
  - `Animate()`: Plays bird animation
  - `TurnOffImage()`: Hides bird icon
- **Components**: Requires Image and Animator components

#### 4. **Cell.cs**
- Grid cell that holds birds
- Implements drag & drop interfaces: `IBeginDragHandler`, `IDragHandler`, `IEndDragHandler`, `IDropHandler`
- **Key Methods**:
  - `IsFree()`: Returns true if cell is empty
  - `SetNewItem(ItemInfo)`: Places a new bird in the cell
  - `Clear()`: Removes bird from cell
- **Drag Logic**: Delegates to DragManager for visual feedback

#### 5. **DragManager.cs**
- Singleton that manages drag & drop operations
- Shows visual feedback during dragging
- **Methods**:
  - `FillInfo(ItemInfo)`: Starts drag operation with item info
  - `Move(Vector2)`: Updates drag icon position
  - `Off()`: Ends drag operation and clears state

#### 6. **ItemsManager.cs**
- Singleton that manages all items/birds in the game
- **Key Method**:
  - `MergeItems(ItemInfo)`: Returns the next level bird when merging
- Maintains list of all available items (`allItems`)

#### 7. **BirdUIManager.cs**
- Singleton for UI management (currently empty stub)
- Reserved for future UI-related functionality

#### 8. **Item.cs**
- Empty MonoBehaviour stub
- Reserved for future item functionality

## Design Patterns & Conventions

### 1. Singleton Pattern
- Used for managers: `DragManager`, `ItemsManager`, `BirdUIManager`
- **IMPORTANT**: Always call `base.Awake()` in derived classes
- Access via: `ManagerName.Instance.Method()`

### 2. ScriptableObject Data Pattern
- All bird definitions stored as ScriptableObjects
- Stored in `Assets/Birds/`
- Create new via: Right-click > Create > Scriptable Objects > ItemInfo

### 3. Component-Based Architecture
- Following Unity's component model
- Birds use Image + Animator components
- Cells use event system interfaces for interactions

### 4. Event-Driven UI
- Uses Unity's EventSystem for drag & drop
- Implements `IPointerHandler` interfaces
- Physics-independent UI interaction

## Development Workflow

### Adding New Birds

1. **Create Bird Data**:
   ```
   Right-click in Assets/Birds/ > Create > Scriptable Objects > ItemInfo
   ```
2. **Configure Properties**:
   - Assign sprite to `icon`
   - Set `number` (tier/level)
   - Set `income` value
   - (Optional) Assign `soundOnSpawn`
3. **Add to ItemsManager**:
   - Select ItemsManager in scene
   - Add new ItemInfo to `allItems` list
   - Ensure correct order (index matches bird number)

### Creating Animations

1. **Animation Clips**: Store in `Assets/Animation/`
2. **Naming Convention**: `[BirdName].anim` (e.g., `Pegeon.anim`, `Sparrow.anim`)
3. **Animator Controller**: Use `Bird.controller` for all birds
4. **Animation Parameter**: Use `birdId` integer parameter to switch animations

### Working with Cells

- **Cell Prefab**: Located at `Assets/Prefabs/Cell.prefab`
- Each cell must have:
  - Bird component (child object)
  - Event trigger components for drag/drop
- Grid layout typically managed by Unity's Grid Layout Group

### Scene Structure

- **Main Scene**: `Assets/Scenes/Game.unity`
- **Test Scene**: `Assets/Scenes/SampleScene.unity`
- Ensure singleton managers exist in scene or marked as DontDestroyOnLoad

## Important Conventions

### Coding Standards

1. **Naming**:
   - Classes: PascalCase (`DragManager`, `ItemInfo`)
   - Private fields: camelCase with `_` prefix or `[SerializeField]`
   - Public methods: PascalCase
   - Constants: UPPER_SNAKE_CASE

2. **Serialization**:
   - Use `[SerializeField]` for private fields that need Inspector exposure
   - Prefer private fields with public properties over public fields

3. **Comments**:
   - Remove or update commented-out code (found in `Cell.cs` lines 8, 34, 43-44)
   - Use `Debug.Log()` sparingly; remove before production

4. **Typo Alert**:
   - Class name is `Singletone.cs` but should be `Singleton.cs`
   - **Note**: The class itself is correctly named `Singleton<T>`, only the file has typo

### File Organization

- **Scripts**: All in `Assets/Scripts/` (no subdirectories currently)
- **Art**: Separate animation sprites in `Assets/Art/Animations/`
- **Prefabs**: Reusable objects in `Assets/Prefabs/`
- **Data**: ScriptableObjects in type-specific folders (e.g., `Assets/Birds/`)

### Unity-Specific

1. **Meta Files**: Always commit `.meta` files with assets
2. **Scenes**: Save scenes before testing
3. **Prefab Workflow**: Modify prefabs, not scene instances
4. **Input System**: Project uses new Input System (InputSystem_Actions.inputactions)

## Git Workflow

### Branch Strategy
- Work on feature branches with `claude/` prefix
- Example: `claude/claude-md-mixa8bszu3o2om40-01CuzrxH26TqJXNW3C3QCJMR`
- Always push to designated branch, never directly to main

### Ignored Files (.gitignore)
Key ignored items:
- `/Library/` - Unity's cache
- `/Temp/`, `/Obj/`, `/Build/` - Build artifacts
- `*.csproj`, `*.sln` - Auto-generated project files
- `/UserSettings/` - Personal Unity settings
- `.vs/` - Visual Studio cache

### Committed Files
- All `Assets/` (except generated/cached items)
- `ProjectSettings/` configuration
- `Packages/manifest.json` and `packages-lock.json`
- `.meta` files for all assets

## Testing & Debugging

### Common Issues

1. **Null Reference on Singleton**:
   - Ensure singleton exists in scene
   - Check `base.Awake()` is called
   - Verify no duplicate instances

2. **Drag & Drop Not Working**:
   - Check EventSystem exists in scene
   - Verify GraphicRaycaster on Canvas
   - Ensure cells have proper colliders

3. **Animation Not Playing**:
   - Verify `birdId` parameter in Animator
   - Check animation transitions in Bird.controller
   - Ensure Animator component is assigned

### Debug Logs
- `Cell.OnDrag()`: Logs "Dragging" (line 61)
- `Cell.OnEndDrag()`: Logs "End" (line 67)
- Singleton: Logs instance creation and destruction warnings

## AI Integration Features

### Unity AI Packages
The project includes Unity's AI tools:
- **AI Assistant** (1.0.0-pre.12): In-editor AI assistance
- **AI Generators** (1.0.0-pre.20): Asset generation capabilities
- **AI Inference** (2.4.1): Runtime AI features

### Generated Assets
- Stored in `GeneratedAssets/` directory
- Organized by hash-based subdirectories
- Include both image files and metadata (.json)
- Not ignored by git (currently committed)

## Future Enhancements

### Current Gaps
1. **BirdUIManager**: Empty stub, needs implementation for UI features
2. **Item.cs**: Empty stub, purpose unclear
3. **Merge Logic**: `ItemsManager.MergeItems()` exists but merge validation incomplete
4. **Drop Handler**: `Cell.OnDrop()` not implemented
5. **Audio**: `soundOnSpawn` defined but no audio system implemented
6. **Income System**: `income` property defined but not used

### Suggested Improvements
1. Implement proper merge validation in `Cell.OnDrop()`
2. Add audio manager for bird sounds
3. Create score/currency system using bird `income` values
4. Add particle effects for merges
5. Implement save/load system
6. Add tutorial/onboarding flow
7. Clean up commented code in `Cell.cs`

## Quick Reference

### Essential Classes
- **Singleton<T>**: Base for all managers
- **ItemInfo**: ScriptableObject for bird data
- **Cell**: Grid cell with drag/drop logic
- **DragManager**: Handles drag operations
- **ItemsManager**: Manages bird merging logic

### Common Operations

**Access a Singleton**:
```csharp
DragManager.Instance.FillInfo(itemInfo);
ItemsManager.Instance.MergeItems(currentItem);
```

**Create New Bird**:
```csharp
cell.SetNewItem(itemInfo);
```

**Check Cell State**:
```csharp
if (cell.IsFree()) {
    // Cell is empty
}
```

**Merge Birds**:
```csharp
ItemInfo nextLevel = ItemsManager.Instance.MergeItems(currentItem);
```

## Contact & Resources

- Unity Documentation: https://docs.unity3d.com/
- URP Documentation: https://docs.unity3d.com/Packages/com.unity.render-pipelines.universal@latest
- Input System: https://docs.unity3d.com/Packages/com.unity.inputsystem@latest

---

**Last Updated**: 2025-12-08
**Unity Version**: 6000.2.6f2
**Project Status**: Active Development
