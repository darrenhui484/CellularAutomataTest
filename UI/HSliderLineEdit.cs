using Godot;
using System;

public class HSliderLineEdit : HBoxContainer {

    private Label _name;
    private HSlider _hSlider;
    private LineEdit _lineEdit;

    [Export] public float MaxValue = 100;
    [Export] public float MinValue = 0;
    [Export]
    public float Value { get; private set; }

    public override void _Ready() {
        _name = GetNode<Label>("Name");
        _name.Text = Name;

        _hSlider = GetNode<HSlider>("HSlider");
        _hSlider.MaxValue = MaxValue;
        _hSlider.MinValue = MinValue;
        _hSlider.Value = Value;

        _lineEdit = GetNode<LineEdit>("LineEdit");
        _lineEdit.Text = Value.ToString();

        // order matters, connect signals at the end to avoid triggering signal 
        // when setting values
        _hSlider.Connect("value_changed", this, "_onSliderValueChanged");
        _lineEdit.Connect("text_entered", this, "_onTextEntered");
        _lineEdit.Connect("focus_exited", this, "_onFocusExited");
    }

    public override void _Input(InputEvent inputEvent) {
        if (inputEvent is InputEventMouseButton && _lineEdit.HasFocus()) {
            ReleaseFocus();
        }
    }

    private void _onSliderValueChanged(float value) {
        _lineEdit.Text = value.ToString();
        Value = value;
    }

    private void _onFocusExited() {
        _onTextEntered(_lineEdit.Text);
    }

    private void _onTextEntered(string newText) {
        try {
            double val = Convert.ToDouble(newText);
            Value = (float)val;
            _hSlider.Disconnect("value_changed", this, "_onSliderValueChanged");
            _hSlider.Value = val;
            _hSlider.Connect("value_changed", this, "_onSliderValueChanged");
        } catch (Exception e) {

        }
    }
}
