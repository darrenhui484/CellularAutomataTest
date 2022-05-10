using System.Collections.Generic;
using System.Linq;
using System.Collections;
using Godot;
using System;

public class TestScene : Node2D {

    [Export] int TileSize;
    [Export] public int Width;
    [Export] public int Height;

    [Export] public string TileImagePath;
    private int _widthPx;
    private int _heightPx;

    private int _batchSize = 10000;

    private Grid _grid;

    public OpenSimplexNoise Noise = new OpenSimplexNoise();
    public RandomNumberGenerator rng = new RandomNumberGenerator();


    public override void _Ready() {
        _widthPx = Width * TileSize;
        _heightPx = Height * TileSize;

        ImageTexture imageTexture = LoaderUtil.LoadImage(TileImagePath);

        _grid = new Grid(Height, Width, TileSize);
        AddChild(_grid);

        _grid.StartJobApplyToAllTiles(_batchSize, (i, j) => {
            _grid.AddTile(new GridPosition(i, j), imageTexture);
        });

        rng.Seed = 1;

        Noise.Seed = (int)rng.Randi();
        Noise.Octaves = 2;
        Noise.Period = 64;
        Noise.Lacunarity = 2;
        Noise.Persistence = 0.5f;
    }

    private float _mapRange(float value, float start, float end, float refStart, float refEnd) {
        return refStart + ((refEnd - refStart) * ((value - start) / (end - start)));
    }

    public void GenerateNoise() {
        Noise.Seed = (int)rng.Randi();

        _grid.StartJobApplyToAllTiles(_batchSize, (i, j) => {
            float noiseVal = Noise.GetNoise2d(i, j);
            float colorValue = _mapRange(noiseVal, -1, 1, 0, 1);
            _grid.GetTile(i, j).Modulate = new Color(colorValue, colorValue, colorValue, 1);
        });

        var output = String.Format("octaves:{0} period:{1} lacunarity:{2} persistence:{3}", Noise.Octaves, Noise.Period, Noise.Lacunarity, Noise.Persistence);
        GD.Print(output);
    }
}
