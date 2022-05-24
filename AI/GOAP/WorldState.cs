
using System.Collections.Generic;

namespace Goap;

public class WorldState {

    public Dictionary<string, State> States { get; }

    public State this[string key] {
        get => States[key];
        set => States[key] = value;
    }

    public WorldState() {
        States = new Dictionary<string, State>();
    }

    public WorldState(Dictionary<string, State> states) {
        States = states;
    }

    public void AddState(string stateKey, State value) {
        States.Add(stateKey, value);
    }

    public bool isSatisfiedBy(WorldState otherWorldState) {
        foreach (KeyValuePair<string, State> keyValuePair in States) {
            string stateKey = keyValuePair.Key;
            State state = keyValuePair.Value;
            if (!otherWorldState.States.ContainsKey(stateKey)) return false;
            if (!state.IsSatisfiedBy(otherWorldState[stateKey])) return false;
        }
        return true;
    }

    public WorldState DeepCopy() {
        Dictionary<string, State> copyDict = new Dictionary<string, State>();
        foreach (KeyValuePair<string, State> kvp in States) {
            copyDict.Add(kvp.Key, kvp.Value);
        }
        return new WorldState(copyDict);
    }

    public void ApplyEffects(Action action) {
        foreach (KeyValuePair<string, State> kvp in action.Effects) {
            string key = kvp.Key;
            State newState = kvp.Value;
            if (States.ContainsKey(key)) {
                States[key].ApplyState(newState);
            } else {
                States.Add(key, newState);
            }
        }
    }
}