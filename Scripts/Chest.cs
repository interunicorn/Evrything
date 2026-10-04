using System.Collections.Generic;
using Godot;

public partial class Chest : StaticBody2D, IInteractableHandler
{
    [Export]
    public AnimatedSprite2D Sprite { get; set; }

    private bool _isOpened = false;

    public IReadOnlyList<string> GetInteractionOptions(Node interactor)
    {
        return _isOpened ? new[] { "Close" } : new[] { "Open" };
    }

    public void Interact(Node interactor, int optionIndex)
    {
        if (Sprite == null)
        {
            GD.PushError("Chest has no AnimatedSprite2D assigned.");

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
}
