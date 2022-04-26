using Godot;
using System;

public class TestScene : TileMap {

    private int _tileSize;
    [Export] public int Width;
    [Export] public int Height;

    private int _widthPx;
    private int _heightPx;

    public OpenSimplexNoise Noise = new OpenSimplexNoise();
    public RandomNumberGenerator rng = new RandomNumberGenerator();

    public enum Tiles {
        Empty,
        LightGreen,
        Green,
        Blue,
        Brown
    }

    public override void _Ready() {
        _validateSquareTile();

        _tileSize = (int)CellSize.x;
        _widthPx = Width * _tileSize;
        _heightPx = Height * _tileSize;

        _applyToAllCells((i, j) => SetCell(i, j, (int)Tiles.Empty));

        rng.Seed = 1;

        Noise.Seed = (int)rng.Randi();
        Noise.Octaves = 2;
        Noise.Period = 64;
        Noise.Lacunarity = 2;
        Noise.Persistence = 0.5f;
    }

    public override void _PhysicsProcess(float delta) {
        // GenerateNoise();
    }

    public void GenerateNoise() {
        Noise.Seed = (int)rng.Randi();

        _applyToAllCells((i, j) => {
            float noiseVal = Noise.GetNoise2d(i, j);
            if (noiseVal > 0) {
                SetCell(i, j, (int)Tiles.LightGreen);
            } else {
                SetCell(i, j, (int)Tiles.Empty);
            }
        });

        var output = String.Format("octaves:{0} period:{1} lacunarity:{2} persistence:{3}", Noise.Octaves, Noise.Period, Noise.Lacunarity, Noise.Persistence);
        GD.Print(output);
    }

    private void _applyToAllCells(Action<int, int> function) {
        for (int i = 0; i < Height; i++) {
            for (int j = 0; j < Width; j++) {
                function.Invoke(i, j);
            }
        }
    }

    private void _validateSquareTile() {
        if (CellSize.x != CellSize.y) {
            throw new InvalidOperationException("CellSize is not square in tilemap");
        }
    }
}
