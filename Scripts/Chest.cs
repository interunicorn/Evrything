using System.Collections.Generic;
using Godot;

public partial class Chest : StaticBody2D, IInteractable
{
    [Export]
    public bool IsEnabled { get; set; } = true;

    [Export]
    public AnimatedSprite2D Sprite { get; set; }

    private bool _isOpened = false;

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
            GD.PushError("Chest has no AnimatedSprite2D assigned.");
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
}
