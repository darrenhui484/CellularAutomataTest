using Godot;
public class Tile : Sprite {

    public GridPosition GridPosition { get; set; }

    public Tile(GridPosition position) {
        GridPosition = position;
    }
}