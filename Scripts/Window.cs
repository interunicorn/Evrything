using System.Collections.Generic;
using System.Threading.Tasks;
using Godot;

public partial class Window : Area2D, IInteractable
{
    [Export]
    public bool IsEnabled { get; set; } = true;

    [Export]
    public AnimatedSprite2D Sprite { get; set; }

    [Export]
    public RayCast2D RayCast { get; set; }

    [Export]
    public CollisionShape2D AreaCollision { get; set; }

    [Export]
    public float PaddingDown { get; set; } = 8.0f;

    private bool _isOpened = false;

    public override async void _Ready()
    {
        await ToSignal(GetTree(), SceneTree.SignalName.ProcessFrame);
        SnapToFloor();
    }

    public bool CanInteract(Node interactor)
    {
        return IsEnabled;
    }

    public IReadOnlyList<string> GetInteractionOptions(Node interactor)
    {
        if (_isOpened)
        {
            return new[] { "Close" };
        }

        return new[] { "Open" };
    }

    public void Interact(Node interactor, int optionIndex)
    {
        if (!CanInteract(interactor))
            return;

        if (Sprite == null)
        {
            GD.PushError("Window has no AnimatedSprite2D assigned.");
            return;
        }

        if (optionIndex == 0)
        {
            if (_isOpened)
            {
                Close();
            }
            else
            {
                Open();
            }
        }
    }

    private void Open()
    {
        Sprite.Play("Open");
        _isOpened = true;
    }

    private void Close()
    {
        Sprite.Play("Close");
        _isOpened = false;
    }

    private void SnapToFloor()
    {
        if (RayCast == null || AreaCollision == null)
        {
            GD.PushError("Window has no RayCast2D/CollisionShape2D");
            return;
        }

        RayCast.ForceRaycastUpdate();
        if (RayCast.IsColliding())
        {
            float floorY = ToLocal(RayCast.GetCollisionPoint()).Y;
            AreaCollision.Position = new Vector2(AreaCollision.Position.X, floorY + PaddingDown);
            RayCast.Enabled = false;
        }
    }
}
