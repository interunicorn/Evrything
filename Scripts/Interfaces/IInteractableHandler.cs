using System.Collections.Generic;
using Godot;

public interface IInteractableHandler
{
    IReadOnlyList<string> GetInteractionOptions(Node interactor);

    void Interact(Node interactor, int optionIndex);
}
