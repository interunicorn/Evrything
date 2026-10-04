using System;
using System.Collections.Generic;
using Godot;

public partial class InteractableComponent : Node, IInteractable
{
    [Export]
    public bool IsEnabled { get; set; } = true;

    private IInteractableHandler _handler;

    public override void _Ready()
    {
        _handler = GetParent() as IInteractableHandler;

        if (_handler == null)
        {
            GD.PushError($"{GetPath()}: Parent must implement IInteractableHandler.");
        }
    }

    public bool CanInteract(Node interactor)
    {
        return IsEnabled && _handler != null;
    }

    public IReadOnlyList<string> GetInteractionOptions(Node interactor)
    {
        if (!CanInteract(interactor))
            return Array.Empty<string>();

        return _handler.GetInteractionOptions(interactor);
    }

    public void Interact(Node interactor, int optionIndex)
    {
        if (!CanInteract(interactor))
            return;

        _handler.Interact(interactor, optionIndex);
    }
}
