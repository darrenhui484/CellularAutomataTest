using Godot;
using System;

public class MainCamera : Camera2D {

    private float _minZoom = 0.1f;
    private float _maxZoom = 3.0f;
    private float _zoomInterval = 0.1f;
    private float _zoomSpeed;
    private float _targetZoom = 1f;
    private Vector2 _targetPosition;
    private float _moveSpeed = 15f;
    private Vector2 _moveDirection = Vector2.Zero;
    private bool _isSpeedModifierPressed = false;

    public override void _Ready() {
        _targetPosition = Position;
        _zoomSpeed = _zoomInterval * 30;
    }

    public override void _PhysicsProcess(float delta) {
        Vector2 direction = Input.GetVector("left", "right", "up", "down", 0.1f);
        if (direction.Length() > 0.5f) {
            _moveCamera(direction);
        }


        bool isMovementComplete = _handleCameraMovement(delta);
        bool isZoomComplete = _handleCameraZoom(delta);

        // SetPhysicsProcess(isZoomComplete && isMovementComplete);
    }

    public override void _UnhandledInput(InputEvent inputEvent) {
        if (inputEvent.IsActionPressed("shift") && !_isSpeedModifierPressed) {
            _isSpeedModifierPressed = true;
            _moveSpeed *= 5;
        }

        if (inputEvent.IsActionReleased("shift")) {
            _isSpeedModifierPressed = false;
            _moveSpeed /= 5;
        }


        if (inputEvent.IsActionPressed("scroll_up")) {
            _zoomIn();
        } else if (inputEvent.IsActionPressed("scroll_down")) {
            _zoomOut();
        }
    }

    private bool _handleCameraMovement(float delta) {
        float x = Mathf.Lerp(Position.x, _targetPosition.x, _moveSpeed * delta);
        float y = Mathf.Lerp(Position.y, _targetPosition.y, _moveSpeed * delta);
        Position = new Vector2(x, y);
        return Mathf.IsEqualApprox(Position.x, _targetPosition.x)
            && Mathf.IsEqualApprox(Position.y, _targetPosition.y);
    }

    private bool _handleCameraZoom(float delta) {
        float zoomDelta = Mathf.Lerp(Zoom.x, _targetZoom, _zoomSpeed * delta);
        Zoom = new Vector2(zoomDelta, zoomDelta);
        return Mathf.IsEqualApprox(Zoom.x, _targetZoom);
    }

    private void _moveCamera(Vector2 direction) {
        _targetPosition += direction * _moveSpeed;
        SetPhysicsProcess(true);
    }

    private void _zoomIn() {
        _targetZoom = Mathf.Max(_targetZoom - _zoomInterval, _minZoom);
        SetPhysicsProcess(true);
    }

    private void _zoomOut() {
        _targetZoom = Mathf.Min(_targetZoom + _zoomInterval, _maxZoom);
        SetPhysicsProcess(true);
    }
}
