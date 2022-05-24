using Godot;
public class Actor : KinematicBody2D {

    private int _hunger = 30;
    public int Hunger {
        get => _hunger;
        set {
            _hunger = value;
            if (_hunger <= 0) {
                QueueFree();
            }
        }
    }
}