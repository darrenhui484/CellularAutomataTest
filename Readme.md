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
Performance gets worse with increased number of objects on screen. Seems to be no perceivable change in performance when modifying size of each sprite.