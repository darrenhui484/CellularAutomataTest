using System;
public struct BoolState : State {

    public bool Value { get; set; }

    public BoolState(bool value) {
        Value = value;
    }

    public bool IsSatisfiedBy(State otherState) {
        return Value == ((BoolState)otherState).Value;
    }

    public void ApplyState(State otherState) {
        Value = ((BoolState)otherState).Value;
    }
}