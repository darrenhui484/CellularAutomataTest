using Godot;
using System;

public class TileMapTest : TileMap {

    [Export] int TileSize = 32;
    [Export] public int Width;
    [Export] public int Height;
    public const int TileGranularity = 1000;

    public OpenSimplexNoise Noise = new OpenSimplexNoise();
    public RandomNumberGenerator rng = new RandomNumberGenerator();

    public override void _Ready() {
        _generateTiles();

        rng.Seed = 1;

        Noise.Seed = (int)rng.Randi();
        Noise.Octaves = 2;
        Noise.Period = 64;
        Noise.Lacunarity = 2;
        Noise.Persistence = 0.5f;

        GenerateNoise();
    }

    public void GenerateNoise() {
        Noise.Seed = (int)rng.Randi();

        for (int i = 0; i < Width; i++) {
            for (int j = 0; j < Height; j++) {
                float noiseVal = Noise.GetNoise2d(i, j);
                int tileId = Mathf.RoundToInt(_mapRange(noiseVal, -1, 1, 0, TileGranularity));
                SetCell(i, j, tileId);
            }
        }
    }

    private float _mapRange(float value, float start, float end, float refStart, float refEnd) {
        return refStart + ((refEnd - refStart) * ((value - start) / (end - start)));
    }

    private void _generateTiles() {
        ImageTexture image = ImageLoader.LoadImage("res://Assets/white.png");

        for (int i = 0; i < TileGranularity; i++) {
            this.TileSet.CreateTile(i);
            this.TileSet.TileSetTexture(i, image);
            float colorValue = i / (float)TileGranularity;
            this.TileSet.TileSetModulate(i, new Color(colorValue, colorValue, colorValue, 1));
        }
    }
}
