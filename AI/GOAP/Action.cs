namespace Goap;

using System;
using System.Collections.Generic;
public abstract class Action {
    public List<KeyValuePair<string, State>> Preconditions;
    public List<KeyValuePair<string, State>> Effects;

    public abstract int GetCost();
    public abstract void Perform();
    public abstract bool IsValid();
}