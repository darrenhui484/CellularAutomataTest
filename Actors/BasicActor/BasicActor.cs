using Godot;
using System;
using Stateless;
public class BasicActor : Actor {

    [Export] public float WanderStrength = 0.5f;
    private float _moveSpeed = 200;

    private RandomNumberGenerator rng = new RandomNumberGenerator();
    private AnimationPlayer _animationPlayer;
    private Area2D _detectionZone;
    private Area2D _interactableZone;

    private Vector2 _desiredDirection = Vector2.Zero;
    private FaceDirection _faceDirection;
    public enum State {
        DecideNext,
        FindFood
    }
    public enum Trigger {
        NeedTriggered
    }
    public override void _Ready() {

        var stateMachine = new StateMachine<State, Trigger>(State.DecideNext);



        rng.Randomize();

        _animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");

        _detectionZone = GetNode<Area2D>("DetectionZone");
        _detectionZone.Connect("area_entered", this, "_onDetection");

        _interactableZone = GetNode<Area2D>("InteractableZone");
        _interactableZone.Connect("area_entered", this, "_onNextToInteractable");

        _faceDirection = GetNode<FaceDirection>("FaceDirection");
        _faceDirection.Magnitude = 100;

    }

    public override void _PhysicsProcess(float delta) {



        _desiredDirection = (_desiredDirection + MathUtil.RandomInsideUnitCircle(rng) * WanderStrength).Normalized();
        Vector2 movement = _desiredDirection * _moveSpeed;

        // _animateWalk(movement);

        MoveAndSlide(movement);
        _faceDirection.Direction = _desiredDirection;
    }

    private void _onDetection(Area2D area2D) {

    }

    private void _onNextToInteractable(Area2D area2D) {

    }

    private void _animateWalk(Vector2 movement) {
        if (movement.Length() > 0) {
            var degrees = Mathf.Rad2Deg(movement.Angle());

            if ((degrees < 45 && degrees >= 0) || (degrees > 315 && degrees <= 360)) {
                _animationPlayer.Play("WalkRight");
            } else if (degrees >= 45 && degrees < 135) {
                _animationPlayer.Play("WalkDown");
            } else if (degrees >= 135 && degrees < 225) {
                _animationPlayer.Play("WalkLeft");
            } else {
                _animationPlayer.Play("WalkUp");
            }
        } else {
            //idle
        }
    }
}
