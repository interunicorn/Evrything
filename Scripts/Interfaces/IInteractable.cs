using System.Collections.Generic;
using Godot;

public interface IInteractable
{
    bool IsEnabled { get; }

    bool CanInteract(Node interactor);

    IReadOnlyList<string> GetInteractionOptions(Node interactor);

    void Interact(Node interactor, int optionIndex);
}
