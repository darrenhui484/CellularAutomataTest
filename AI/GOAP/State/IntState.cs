public struct IntState : State {

    public int Value { get; set; }
    public Condition Condition { get; set; }

    public IntState(int value, Condition condition) {
        Value = value;
        Condition = condition;
    }

    public bool IsSatisfiedBy(State otherState) {
        return ConditionalUtility.Evaluate(Value, ((IntState)otherState).Value, Condition);
    }
    public void ApplyState(State otherState) {
        Value += ((IntState)otherState).Value;
    }
}