namespace Goap;

using System.Collections.Generic;
using Godot;
public class GoapAgent {
    public List<Goal> Goals;
    public Goal CurrentGoal;

    public GoapAgent(List<Goal> goals) {
        Goals = goals;
    }

    public Goal SelectHighestPriorityGoal() {
        Goal currentHighestPriorityGoal = null;
        foreach (Goal goal in Goals) {
            if (goal.IsValid() && (currentHighestPriorityGoal == null || goal.GetPriority() < currentHighestPriorityGoal.GetPriority())) {
                currentHighestPriorityGoal = goal;
            }
        }
        return currentHighestPriorityGoal;
    }

    public void UpdateGoal() {
        CurrentGoal = SelectHighestPriorityGoal();
    }
}