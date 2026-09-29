using System;
using System.Collections.Generic;
using Godot;

public partial class InteractionController : RayCast2D
{
    [Export]
    public float InteractionDistance { get; set; } = 32.0f;

    [Export]
    public InteractionMenu Menu { get; set; }

    private Node _interactor;
    private IInteractable _currentInteractable;

    public override void _Ready()
    {
        CollideWithAreas = true;
        TargetPosition = Vector2.Down * InteractionDistance;
    }

    public void SetInteractor(Node interactor)
    {
        _interactor = interactor;
    }

    public void UpdateFacingDirection(Vector2 direction)
    {
        if (direction != Vector2.Zero)
        {
            TargetPosition = direction.Normalized() * InteractionDistance;
        }
    }

    public void TryInteract()
    {
        if (_interactor == null)
            return;

        ForceRaycastUpdate();

        if (GetCollider() is not IInteractable interactable)
            return;

        if (!interactable.IsEnabled)
            return;

        if (!interactable.CanInteract(_interactor))
            return;

        IReadOnlyList<string> options = interactable.GetInteractionOptions(_interactor);

        if (options == null || options.Count == 0)
            return;

        _currentInteractable = interactable;

        if (Menu == null)
        {
            GD.PushWarning("InteractionController has no InteractionMenu assigned.");
            return;
        }

        Menu.ShowOptions(options, ExecuteInteraction);
    }

    private void ExecuteInteraction(int optionIndex)
    {
        if (_currentInteractable == null)
            return;

        if (!_currentInteractable.IsEnabled)
        {
            ClearInteraction();
            return;
        }

        if (!_currentInteractable.CanInteract(_interactor))
        {
            ClearInteraction();
            return;
        }

        IReadOnlyList<string> options = _currentInteractable.GetInteractionOptions(_interactor);

        if (optionIndex < 0 || optionIndex >= options.Count)
        {
            ClearInteraction();
            return;
        }

        _currentInteractable.Interact(_interactor, optionIndex);

        ClearInteraction();
    }

    private void ClearInteraction()
    {
        _currentInteractable = null;
    }

    public void CancelInteraction()
    {
        _currentInteractable = null;
        Menu?.HideMenu();
    }
}
