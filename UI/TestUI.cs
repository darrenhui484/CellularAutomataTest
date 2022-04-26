using System.Collections.Generic;
using System.Linq;
using Godot;
public class TestUI : Control {

    public Label Info;
    private Button _generate;
    private List<HSliderLineEdit> _sliders;
    private Dictionary<string, float> _sliderValues = new Dictionary<string, float>();
    [Signal] public delegate void on_generate(Dictionary<string, float> sliderValues);

    public override void _Ready() {
        Info = GetNode<Label>("CanvasLayer/TestUIContainer/Info");
        _generate = GetNode<Button>("CanvasLayer/TestUIContainer/Generate");
        _generate.Connect("pressed", this, "_onGeneratePressed");
        _sliders = GetNode<VBoxContainer>("CanvasLayer/TestUIContainer/Sliders")
            .GetChildren()
            .Cast<HSliderLineEdit>()
            .ToList();
        _sliders.ForEach((slider) => _sliderValues.Add(slider.Name, slider.Value));
    }

    public Dictionary<string, float> GetSliderValues() {
        _sliders.ForEach((slider) => _sliderValues[slider.Name] = slider.Value);
        return _sliderValues;
    }

    private void _onGeneratePressed() {
        _sliders.ForEach((slider) => _sliderValues[slider.Name] = slider.Value);
        EmitSignal("on_generate", _sliderValues);
    }
}