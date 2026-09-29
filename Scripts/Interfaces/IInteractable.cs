using Godot;

public interface IInteractable
{
    bool IsEnabled { get; }
    string[] Options { get; set; }

    bool CanInteract(Node interactor);
    void Interact(Node interactor);
}
