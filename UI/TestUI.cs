using System.Collections.Generic;
using System.Linq;
using Godot;
public class TestUI : Control {

    public Label Fps;
    private Button _generate;
    private List<HSliderLineEdit> _sliders;
    private Dictionary<string, float> _sliderValues = new Dictionary<string, float>();
    [Signal] public delegate void on_generate(Dictionary<string, float> sliderValues);

    public override void _Ready() {
        Fps = GetNode<Label>("CanvasLayer/TestUIContainer/FPS");
        _generate = GetNode<Button>("CanvasLayer/TestUIContainer/Generate");
        _generate.Connect("pressed", this, "_onGenerate");
        _sliders = GetNode<VBoxContainer>("CanvasLayer/TestUIContainer/Sliders")
            .GetChildren()
            .Cast<HSliderLineEdit>()
            .ToList();
        _sliders.ForEach((slider) => {
            _sliderValues.Add(slider.Name, slider.Value);
            slider.Connect("value_changed", this, "_onGenerate");
        });
    }

    public Dictionary<string, float> GetSliderValues() {
        _sliders.ForEach((slider) => _sliderValues[slider.Name] = slider.Value);
        return _sliderValues;
    }

    private void _onGenerate() {
        _sliders.ForEach((slider) => _sliderValues[slider.Name] = slider.Value);
        EmitSignal("on_generate", _sliderValues);
    }
}