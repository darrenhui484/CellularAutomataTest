public struct FloatState : State {

    public float Value { get; set; }
    public Condition Condition { get; set; }

    public FloatState(float value, Condition condition) {
        Value = value;
        Condition = condition;
    }

    public bool IsSatisfiedBy(State otherState) {
        return ConditionalUtility.Evaluate(Value, ((FloatState)otherState).Value, Condition);
    }

    public void ApplyState(State otherState) {
        Value += ((FloatState)otherState).Value;
    }
}