namespace Goap;

using System;
using System.Collections.Generic;
using System.Linq;
public class Planner {

    // private Dictionary<string, List<Action>> _actionsBucketedByEffect;

    // public Planner(List<Action> actions) {
    //     foreach (Action action in actions)
    //         AddAction(action);
    // }

    // public Queue<Action> GetPlan(Goal goal) {
    //     return _buildPlan(goal.TargetState(), _actionsBucketedByEffect);
    // }

    // private Queue<Action> _buildPlan(WorldState targetState, Dictionary<string, List<Action>> actionsBucketedByEffect) {
    //     // sort actions by cost in each bucket
    //     foreach (KeyValuePair<string, List<Action>> keyValuePair in actionsBucketedByEffect)
    //         keyValuePair.Value.Sort((Action x, Action y) => x.GetCost().CompareTo(y.GetCost()));

    //     return _searchActionSpace(targetState, actionsBucketedByEffect);
    // }

    // private Queue<Action> _searchActionSpace(WorldState targetState, Dictionary<string, List<Action>> actionsBucketedByEffectSortedByCost) {
    //     Queue<Action> actionQueue = new Queue<Action>();

    //     //TODO implement search
    //     // heuristic: start search from nodes closest to goal effect with lowest cost

    //     Action minCostAction = null;
    //     foreach (string targetStateKey in targetState.States.Keys) {
    //         if (!actionsBucketedByEffectSortedByCost.ContainsKey(targetStateKey)) return null;

    //         Action currentAction = actionsBucketedByEffectSortedByCost[targetStateKey][0];
    //         if (minCostAction != null || minCostAction.GetCost() > currentAction.GetCost())
    //             minCostAction = currentAction;
    //     }

    //     return actionQueue;
    // }

    // public void AddAction(Action action) {
    //     foreach (KeyValuePair<string, State> item in action.Effects) {
    //         if (!_actionsBucketedByEffect.ContainsKey(item.Key)) {
    //             _actionsBucketedByEffect.Add(item.Key, new List<Action>());
    //         }
    //         _actionsBucketedByEffect[item.Key].Add(action);
    //     }
    // }


    // ------------------ unoptimized ---------------------



    public Planner() {

    }

    // public Queue<Action> GetPlan(Goal goal, List<Action> actions) {
    //     return SearchActionSpace(goal.TargetState(), actions);
    // }

    // public Queue<Action> SearchActionSpace(WorldState targetState, List<Action> actions) {
    //     var pQueue = new PriorityQueue<Action>();

    //     Search(pQueue, actions, targetState, _getCurrentWorldState());
    // }

    // TODO change list of actions to bucketed sets of actions
    public void Search(PriorityQueue<Action> pQueue, List<Action> actions, WorldState targetState, WorldState currentWorldState) {
        // add valid actions to PQ(cost) if it doesn't already have it
        foreach (Action action in actions) {
            foreach (KeyValuePair<string, State> kvp in action.Effects) {
                if (!pQueue.Contains(action) && targetState.States.ContainsKey(kvp.Key) && action.IsValid()) {
                    pQueue.Enqueue(action, action.GetCost());
                }
            }
        }

        Action currentAction = pQueue.Dequeue();


        // WorldState workingState = _getRelevantWorldState();

        // update current world state with effects of current action
        // check if target world state is satisfied, exit if true
        // recurse and deep copy world state with current action effects


        // OPEN = priority queue containing START
        // CLOSED = empty set
        // while lowest rank in OPEN is not the GOAL:
        //   current = remove lowest rank item from OPEN
        //   add current to CLOSED
        //   for neighbors of current:
        //     cost = g(current) + movementcost(current, neighbor)
        //     if neighbor in OPEN and cost less than g(neighbor):
        //       remove neighbor from OPEN, because new path is better
        //     if neighbor in CLOSED and cost less than g(neighbor): ⁽²⁾
        //       remove neighbor from CLOSED
        //     if neighbor not in OPEN and neighbor not in CLOSED:
        //       set g(neighbor) to cost
        //       add neighbor to OPEN
        //       set priority queue rank to g(neighbor) + h(neighbor)
        //       set neighbor's parent to current

        // reconstruct reverse path from goal to start
        // by following parent pointers


    }

    // private int calculateHeuristic(Action action, WorldState currentState, WorldState targetState) {
    //     // apply action effects on current state
    //     currentState.ApplyEffects(action);
    //     // calculate distance from target

    //     // return actions cost + distance to target
    // }

    // private int _calculateDistance(WorldState currentState, WorldState targetState) {
    //     foreach (KeyValuePair<string, State> kvp in targetState.States) {
    //         string key = kvp.Key;
    //         if (currentState.States.ContainsKey(key)) {

    //         }
    //     }
    // }

    // private WorldState _getRelevantWorldState(List<string> states) {
    //     // query for relevant states to merge into working world state
    // }

    // private WorldState _getRelevantWorldState(WorldState targetWorldState) {

    //     foreach (string stateKey in targetWorldState.States.Keys) {
    //         // query relevant data to get local world state
    //     }

    // }
}


