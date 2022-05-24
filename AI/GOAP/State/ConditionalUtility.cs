using System;

public static class ConditionalUtility {

    private static Func<int, int, bool>[] _evaluateIntConditions = new Func<int, int, bool>[6] {
        (v1, v2) => true,
        (v1, v2) => v2 == v1,
        (v1, v2) => v2 > v1,
        (v1, v2) => v2 < v1,
        (v1, v2) => v2 >= v1,
        (v1, v2) => v2 <= v1,
    };

    private static Func<float, float, bool>[] _evaluateFloatConditions = new Func<float, float, bool>[6] {
        (v1, v2) => true,
        (v1, v2) => Math.Abs(Math.Abs(v2) - Math.Abs(v1)) < 0.0001f,
        (v1, v2) => v2 > v1,
        (v1, v2) => v2 < v1,
        (v1, v2) => v2 >= v1,
        (v1, v2) => v2 <= v1,
    };

    public static bool Evaluate(int val1, int val2, Condition condition) {
        return _evaluateIntConditions[(int)condition].Invoke(val1, val2);
    }

    public static bool Evaluate(float val1, float val2, Condition condition) {
        return _evaluateFloatConditions[(int)condition].Invoke(val1, val2);
    }
}

public enum Condition {
    None,
    EqualTo,
    GreaterThan,
    LessThan,
    GreaterThanOrEqualTo,
    LessThanOrEqualTo
}