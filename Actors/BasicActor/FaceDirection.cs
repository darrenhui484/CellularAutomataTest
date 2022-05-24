using Godot;
using System;

public class FaceDirection : Node2D {

    public Vector2 Direction { get; set; }
    public int Magnitude { get; set; }

    public override void _Ready() {
        Direction = Vector2.Zero;
    }

    public override void _PhysicsProcess(float delta) {
        Update();
    }

    public override void _Draw() {
        DrawLine(Position, Position + Direction * Magnitude, Colors.Red);
    }
}
