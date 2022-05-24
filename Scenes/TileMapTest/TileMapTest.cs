using Godot;
using System;

public class TileMapTest : TileMap {

    [Export] int TileSize = 32;
    [Export] public int Width = 300;
    [Export] public int Height = 300;
    public const int TileGranularity = 1000;

    public OpenSimplexNoise Noise = new OpenSimplexNoise();
    public RandomNumberGenerator rng = new RandomNumberGenerator();

    private const string _baseTilePath = "res://Assets/white.png";

    public enum Tiles {
        Grass,
        Water,
        Dirt,
        Tree
    }

    private const Tuple<int, int>[] _neighbors = [(0, 1), (0, -1), (-1, 0), (1, 0), (1, 1), (1, -1), (-1, 1), (-1, -1)];

    public override void _Ready() {
        // _generateTiles(_createGrayscaleTileset, TileGranularity);
        _generateTiles(_createCustomTileSet);

        rng.Randomize();

        Noise.Seed = (int)rng.Randi();
        Noise.Octaves = 2;
        Noise.Period = 64;
        Noise.Lacunarity = 2;
        Noise.Persistence = 0.5f;

        GenerateNoise();
    }

    public void GenerateNoise() {
        Noise.Seed = (int)rng.Randi();

        // _applyGrayscaleNoiseRules();
        _applyCustomNoiseRules();

    }

    private void _applyCustomNoiseRules() {
        _applyToAllTiles((x, y) => {
            float noiseVal = Noise.GetNoise2d(x, y);
            int tileId = _mapNoiseToTile(noiseVal);
            SetCell(x, y, tileId);
        });
    }

    private int _mapNoiseToTile(float noiseValue) {

        if (noiseValue > -0.2f && noiseValue < 0.2f) {
            return (int)Tiles.Water;
        } else if (noiseValue >= 0.4f || noiseValue <= -0.4f) {
            if (rng.Randi() % 5 == 1) return (int)Tiles.Tree;

            return (int)Tiles.Grass;
        } else {
            return (int)Tiles.Dirt;
        }
    }

    private void _applyGrayscaleNoiseRules() {
        _applyToAllTiles((x, y) => {
            float noiseVal = Noise.GetNoise2d(x, y);
            int tileId = Mathf.RoundToInt(_mapRange(noiseVal, -1, 1, 0, TileGranularity));
            SetCell(x, y, tileId);
        });
    }

    private void _applyToAllTiles(Action<int, int> function) {
        for (int i = 0; i < Width; i++) {
            for (int j = 0; j < Height; j++) {
                function.Invoke(i, j);
            }
        }
    }

    private float _mapRange(float value, float start, float end, float refStart, float refEnd) {
        return refStart + ((refEnd - refStart) * ((value - start) / (end - start)));
    }

    private void _createGrayscaleTileset(ImageTexture image, int granularity) {
        for (int i = 0; i < TileGranularity; i++) {
            this.TileSet.CreateTile(i);
            this.TileSet.TileSetTexture(i, image);
            float colorValue = i / (float)TileGranularity;
            this.TileSet.TileSetModulate(i, new Color(colorValue, colorValue, colorValue, 1));
        }
    }

    private void _createCustomTileSet(ImageTexture image) {
        _createGeneratedTile((int)Tiles.Grass, Colors.Green, image);
        _createGeneratedTile((int)Tiles.Water, Colors.Blue, image);
        _createGeneratedTile((int)Tiles.Dirt, Colors.Brown, image);
        _createGeneratedTile((int)Tiles.Tree, Colors.Red, image);
    }

    private void _createGeneratedTile(int id, Color color, ImageTexture image) {
        this.TileSet.CreateTile(id);
        this.TileSet.TileSetTexture(id, image);
        this.TileSet.TileSetModulate(id, color);
    }

    private void _generateTiles(Action<ImageTexture> createTileSet) {
        ImageTexture image = LoaderUtil.LoadImage(_baseTilePath);
        createTileSet.Invoke(image);
    }

    private void _generateTiles(Action<ImageTexture, int> createTileSet, int granularity) {
        ImageTexture image = LoaderUtil.LoadImage(_baseTilePath);

        createTileSet.Invoke(image, granularity);
    }

    // **** cellular automata

    private void _tick() {

    }

    private void _updateCells() {

    }


    private int _getCellState(int x, int y, int emptyTileId, int livingTileId) {
        int tileId = GetCell(x, y);

        int numAdjCells = _getLivingAdjacentCellCount(x, y, emptyTileId);

        if (tileId == emptyTileId) {
            if (numAdjCells < 3) {
                return emptyTileId;
            } else {
                return livingTileId;
            }
        } else {
            if (numAdjCells < 3 || numAdjCells > 7) {
                return emptyTileId;
            } else {
                return livingTileId;
            }
        }
    }

    private int _getLivingAdjacentCellCount(int x, int y, int emptyTileId) {
        int livingAdjacentCells = 0;
        foreach (Tuple<int, int> delta in _neighbors) {
            int newX = x + delta.Item1;
            int newY = y + delta.Item2;
            if (_isOutOfBounds(newX, newY)) continue;
            if (GetCell(newX, newY) != emptyTileId) livingAdjacentCells++;
        }
        return livingAdjacentCells;
    }

    private bool _isOutOfBounds(int x, int y) {
        return x < 0 && x > Width && y < 0 && y > Height;
    }


}
