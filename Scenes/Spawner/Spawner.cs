using Godot;

public class Spawner : Position2D {

    [Export] public PackedScene Actor;
    [Export] public int Radius = 50;
    private RandomNumberGenerator rng = new RandomNumberGenerator();

    public override void _Ready() {

    }

    public static Node2D CreateInstance(PackedScene scene, Vector2 position) {
        Node2D instance = (Node2D)scene.Instance();
        instance.Position = position;
        return instance;
    }

    public Node2D CreateInstance() {
        Node2D instance = (Node2D)Actor.Instance();
        instance.Position = Position;
        return instance;
    }

    public Node2D CreateInstanceRandom() {
        Node2D instance = (Node2D)Actor.Instance();
        instance.Position = Position + MathUtil.RandomInsideCircle(rng, Radius);
        return instance;
    }
}