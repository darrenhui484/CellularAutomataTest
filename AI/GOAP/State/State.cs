using System;
public interface State {
    public bool IsSatisfiedBy(State otherState);
    public void ApplyState(State otherState);
}