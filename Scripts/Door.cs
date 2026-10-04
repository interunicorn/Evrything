using System.Collections.Generic;
using Godot;

public partial class Door : StaticBody2D, IInteractableHandler
{
    [Export]
    public AnimatedSprite2D Sprite { get; set; }

    [Export]
    public CollisionShape2D Collision { get; set; }

    private bool _isOpened = false;

    public IReadOnlyList<string> GetInteractionOptions(Node interactor)
    {
        return _isOpened ? new[] { "Close" } : new[] { "Open" };
    }

    public void Interact(Node interactor, int optionIndex)
    {
        if (Sprite == null || Collision == null)
        {
            GD.PushError("Door has no AnimatedSprite2D/CollisionShape2D assigned.");

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
        Collision.Disabled = true;
    }

    private void Close()
    {
        Sprite.Play("Close");
        _isOpened = false;
        Collision.Disabled = false;
    }
}
