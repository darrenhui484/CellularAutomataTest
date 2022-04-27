using System;
using System.Linq;
using Godot;
using System.Collections.Generic;

public class Main : Node {

    private TestScene _testScene;
    private TestUI _testUI;

    public override void _Ready() {
        _testScene = GetNode<TestScene>("TestScene");
        _testUI = GetNode<TestUI>("TestUI");
        _testUI.Connect("on_generate", this, "_onGenerate");
    }

    public override void _PhysicsProcess(float delta) {
        _testUI.Fps.Text = Godot.Engine.GetFramesPerSecond().ToString();
        ProfilerUtil.PrintMemoryOnChange();
    }

    private void _onGenerate(Dictionary<string, float> sliderValues) {
        _setNoiseParameters(sliderValues);
        _testScene.GenerateNoise();
    }

    private void _setNoiseParameters(Dictionary<string, float> sliderValues) {
        foreach (var slider in sliderValues) {
            var propertyInfo = _testScene.Noise
                .GetType()
                .GetProperty(slider.Key);
            propertyInfo.SetValue(_testScene.Noise, Convert.ChangeType(slider.Value, propertyInfo.PropertyType));
        }
    }
}
