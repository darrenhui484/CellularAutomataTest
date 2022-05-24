namespace Goap;

using System.Collections.Generic;

public abstract class Goal {
    public abstract bool IsValid();
    public abstract int GetPriority();
    public abstract WorldState TargetState();
}
