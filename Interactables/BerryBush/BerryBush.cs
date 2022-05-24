using Godot;
public class BerryBush : StaticBody2D {

    private const int _emptySprite = 28;
    private const int _fullSprite = 27;
    private int _berryCount = 20;
    private Timer _growthTimer;
    public int BerryCount {
        get => _berryCount;
        set {
            _berryCount = Mathf.Min(value, MaxBerryCount);
        }
    }
    public int MaxBerryCount = 20;

    private Sprite _sprite;
    private Label _info;

    public override void _Ready() {
        _sprite = GetNode<Sprite>("Sprite");
        _sprite.Frame = _fullSprite;


        InputPickable = true;
        this.Connect("mouse_entered", this, "_onMouseEntered");
        this.Connect("mouse_exited", this, "_onMouseExited");


        _growthTimer = new Timer();
        _growthTimer.Autostart = true;
        _growthTimer.WaitTime = 10;
        _growthTimer.Connect("timeout", this, "_onGrowthTimeout");

        _info = GetNode<Label>("Control/Info");
    }

    public Berry interact() {
        BerryCount--;
        if (BerryCount == 0) {
            _removeFromInteractablesLayer();
            _sprite.Frame = _emptySprite;
        }
        return new Berry();
    }

    private void _onMouseEntered() {
        _info.Visible = true;
        _info.Text = "BerryCount: " + BerryCount;
    }

    private void _onMouseExited() {
        _info.Visible = false;
    }

    private void _onGrowthTimeout() {
        BerryCount += 10;
    }

    private void _removeFromInteractablesLayer() {
        SetCollisionLayerBit(3, false);
    }

    private void _addToInteractablesLayer() {
        SetCollisionLayerBit(3, true);
    }
}