
using Godot;
public static class MathUtil {
    public static Vector2 RandomInsideUnitCircle(RandomNumberGenerator rng) {
        return RandomInsideCircle(rng, 1);
    }

    public static Vector2 RandomInsideCircle(RandomNumberGenerator rng, float radius) {
        var angle = rng.RandfRange(-Mathf.Pi, Mathf.Pi);
        var magnitude = rng.RandfRange(0, radius);
        var x = Mathf.Cos(angle) * magnitude;
        var y = Mathf.Sin(angle) * magnitude;
        return new Vector2(x, y);
    }
}