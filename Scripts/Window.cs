using System;
using System.Collections.Generic;
using Godot;

public partial class Window : Area2D, IInteractableHandler
{
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

    public IReadOnlyList<string> GetInteractionOptions(Node interactor)
    {
        return _isOpened ? new[] { "Close" } : new[] { "Open" };
    }

    public void Interact(Node interactor, int optionIndex)
    {
        if (Sprite == null)
        {
            GD.PushError("Window has no AnimatedSprite2D assigned.");
            return;
        }

        if (optionIndex != 0)
            return;

        if (_isOpened)
            Close();
        else
            Open();
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
