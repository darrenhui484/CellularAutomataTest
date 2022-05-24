### Setup Godot for C#
1. Add C# Extension, Godot extension
2. Create C# Script file from Godot Editor to generate `.sln`
3. Add C# language version `<LangVersion>10.0</LangVersion>` to `.csproj`
   
### Setup Debug
1. Add Godot Extension
2. Select `Run and Debug` in `VS Code`
3. Create launch settings with preset of `C# Godot`
4. Modify path to Godot executable if necessary

### VS Code Styling
1. Add `omnisharp.json` to root of project
2. Reload with `Ctrl-Shift-P` and select `Developer: Reload Window`

### Notes
## Custom Grid with sprites for each tile
Performance gets worse with increased number of objects on screen. Seems to be no perceivable change in performance when modifying size of each sprite.

## Godot Tilemap and Tileset
Performance outperforms custom implementation by a lot. No additional nodes per tile used. No need to batch process.

### Features

- Survive
    - hunger, thirst
- Build
- Town
    - Quest Board
        - 

Actor:
    Traits
    Stats:
        Health
        Hunger
    Equipment
    Inventory

Actions:
    Actor:
        - Interact(Interactable)
        - Use(Item)
        - Gather(ResourceNode)
        - Store(StorageNode)
        - Trade(Actor)
    Combat
        - 
        - Attack
        - Use Skill
    


### Goap Notes
- Plan across multiple frames while executing previous plan. Replace current plan if new goal is decided.
- Sensors detect world state, any change to world state will prompt recalculating of goals.

Goal
    WState:     HungerDecrease: True

Goal
    WState:     BerryCount:     (20, >)

Eat():
    Effect:     HungerDecrease: True
    Precond:    HasFood:        True

Loot(Item x):
    Effect:     f(x)
    Precond:    null

Harvest(Item x):
    Effect:     f(x)
    Precond:    f(x)

Craft(Item x, int amt):
    Effect:     f(x, amt)
    Precond:    f(x, amt)

Build(Structure x):
    Effect:     f(x)
    Precond:    f(x)

### Smart Objects
Objects can advertise what reward an agent can obtained

### Tagging
Can tag agents or objects to prefilter before checking preconditions, i.e. Carriable, Carrier

### References
astar: https://www.redblobgames.com/pathfinding/a-star/introduction.html
sims AI: http://www.zubek.net/robert/publications/Needs-based-AI-draft.pdf
htn planner: https://www.gameaipro.com/GameAIPro/GameAIPro_Chapter12_Exploring_HTN_Planners_through_Example.pdf
goap: https://alumni.media.mit.edu/~jorkin/GOAP_draft_AIWisdom2_2003.pdf