using Godot;
using System;
using System.Collections;

public class FoodTest : Node2D {

    private Spawner _basicActorSpawner;
    private Spawner _berryBushSpawner;
    private Timer _hungerTimer;
    private Timer _plantGrowthTimer;
    private Node _actors;
    private Node _interactables;

    private Label _actorCountLabel;
    public override void _Ready() {
        _actors = new Node();
        AddChild(_actors);

        _interactables = new Node();
        AddChild(_interactables);

        _hungerTimer = _initializeTimer("_onHungerTimeout");
        AddChild(_hungerTimer);

        _plantGrowthTimer = _initializeTimer("_onPlantGrowthTimeout");
        AddChild(_plantGrowthTimer);

        _berryBushSpawner = GetNode<Spawner>("BerryBushSpawner");
        for (int i = 0; i < 10; i++) {
            var instance = _berryBushSpawner.CreateInstanceRandom();
            _interactables.AddChild(instance);
        }

        _basicActorSpawner = GetNode<Spawner>("ActorSpawner");
        for (int i = 0; i < 50; i++) {
            var instance = _basicActorSpawner.CreateInstanceRandom();
            _actors.AddChild(instance);
        }

        _actorCountLabel = GetNode<Label>("Control/CanvasLayer/ActorCount");
    }

    public override void _PhysicsProcess(float delta) {
        _actorCountLabel.Text = "Actor Count: " + _actors.GetChildren().Count;
    }

    public void _onHungerTimeout() {
        IEnumerator actorsIterator = _actors.GetChildren().GetEnumerator();
        while (actorsIterator.MoveNext()) {
            Actor currentActor = actorsIterator.Current as Actor;
            currentActor.Hunger--;
        }
    }

    public void _onPlantGrowthTimeout() {
        
    }

    private Timer _initializeTimer(string callback) {
        Timer timer = new Timer();
        timer.Autostart = true;
        timer.WaitTime = 1;
        timer.Connect("timeout", this, callback);
        return timer;
    }

}
