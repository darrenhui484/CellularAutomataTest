using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Godot;
public class Grid : Node2D {

    public int TileSize;
    private Tile[] _grid;
    private int _rows;
    private int _columns;
    IEnumerator CurrentJob;
    private bool _isProcessing = false;

    public Grid(int rows, int columns, int tileSize) {
        _grid = new Tile[rows * columns];
        _rows = rows;
        _columns = columns;
        TileSize = tileSize;
    }

    public override void _PhysicsProcess(float delta) {
        if (!_isProcessing) {
            return;
        }

        if (CurrentJob != null && !CurrentJob.MoveNext()) {
            _isProcessing = false;
        }
    }

    public void AddTile(GridPosition gridPosition, Texture texture) {
        Tile tile = new Tile(gridPosition);
        tile.Texture = texture;
        tile.Position = new Vector2(TileSize * gridPosition.Row, TileSize * gridPosition.Column);
        _grid[_getIndex(gridPosition)] = tile;
        AddChild(tile);
    }

    private void _startJob<T>(Func<T, IEnumerable> batchingFunction, T tileFunction) where T : Delegate {
        SetPhysicsProcess(true);
        _isProcessing = true;
        CurrentJob = batchingFunction.Invoke(tileFunction).GetEnumerator();
    }

    private void _startJob<T>(int batchSize, Func<int, T, IEnumerable> batchingFunction, T tileFunction) where T : Delegate {
        SetPhysicsProcess(true);
        _isProcessing = true;
        CurrentJob = batchingFunction.Invoke(batchSize, tileFunction).GetEnumerator();
    }

    public void StartJobApplyToAllTiles(Action<Tile> function) {
        _startJob(_applyToAllTiles, function);
    }

    public void StartJobApplyToAllTiles(Action<int, int> function) {
        _startJob(_applyToAllTiles, function);
    }

    public void StartJobApplyToAllTiles(int batchSize, Action<Tile> function) {
        _startJob(batchSize, _applyToAllTiles, function);
    }

    public void StartJobApplyToAllTiles(int batchSize, Action<int, int> function) {
        _startJob(batchSize, _applyToAllTiles, function);
    }

    private IEnumerable _applyToAllTiles(Action<Tile> function) {
        for (int i = 0; i < _grid.Length; i++) {
            function.Invoke(_grid[i]);
            yield return 0;
        }
    }

    private IEnumerable _applyToAllTiles(Action<int, int> function) {
        for (int i = 0; i < _grid.Length; i++) {
            function.Invoke(_getColumn(i), _getRow(i));
            yield return 0;
        }
    }

    private IEnumerable _applyToAllTiles(int batchSize, Action<Tile> function) {
        int batchCount = Mathf.CeilToInt(_grid.Length / batchSize);
        for (int batchIndex = 0; batchIndex < batchCount; batchIndex++) {
            for (int gridIndex = batchIndex * batchSize; gridIndex < _grid.Length; gridIndex++) {
                function.Invoke(_grid[gridIndex]);
            }
            yield return 0;
        }
    }

    private IEnumerable _applyToAllTiles(int batchSize, Action<int, int> function) {
        int batchCount = Mathf.CeilToInt(_grid.Length / (float)batchSize);
        for (int batchIndex = 0; batchIndex < batchCount; batchIndex++) {
            int startingIndex = batchIndex * batchSize;
            int end = Mathf.Min(_grid.Length, startingIndex + batchSize);
            for (int gridIndex = startingIndex; gridIndex < end; gridIndex++) {
                function.Invoke(_getColumn(gridIndex), _getRow(gridIndex));
            }
            yield return 0;
        }
    }

    private GridPosition _getGridPosition(int index) {
        return new GridPosition(_getColumn(index), _getRow(index));
    }

    private int _getColumn(int index) {
        return index % _columns;
    }

    private int _getRow(int index) {
        return index / _rows;
    }

    private int _getIndex(int column, int row) {
        return row * _columns + column;
    }

    private int _getIndex(GridPosition gridPosition) {
        return _getIndex(gridPosition.Column, gridPosition.Row);
    }


    public Tile GetTile(int column, int row) {
        return _grid[_getIndex(column, row)];
    }

    public Tile GetTile(GridPosition gridPosition) {
        return GetTile(gridPosition.Column, gridPosition.Row);
    }
}