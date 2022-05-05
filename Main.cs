using System;
using System.Linq;
using Godot;
using System.Collections.Generic;

public class Main : Node {

	private TileMapTest _tileMapTest;
	private TestUI _testUI;

	public override void _Ready() {
		_tileMapTest = GetNode<TileMapTest>("TileMapTest");
		_testUI = GetNode<TestUI>("TestUI");
		_testUI.Connect("on_generate", this, "_onGenerate");
	}

	public override void _PhysicsProcess(float delta) {
		_testUI.Fps.Text = Godot.Engine.GetFramesPerSecond().ToString();
		ProfilerUtil.PrintMemoryOnChange();
	}

	private void _onGenerate(Dictionary<string, float> sliderValues) {
		_setNoiseParameters(sliderValues, _tileMapTest.Noise);
		_tileMapTest.GenerateNoise();
	}

	private void _setNoiseParameters(Dictionary<string, float> sliderValues, OpenSimplexNoise noise) {
		foreach (var slider in sliderValues) {
			var propertyInfo = noise
				.GetType()
				.GetProperty(slider.Key);
			propertyInfo.SetValue(noise, Convert.ChangeType(slider.Value, propertyInfo.PropertyType));
		}
	}
}
