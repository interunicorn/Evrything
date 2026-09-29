using Godot;

public partial class InteractableComponent : Area2D, IInteractable
{
    [Signal]
    public delegate void InteractedEventHandler(Node interactor);

    [Export]
    public bool IsEnabled { get; set; } = true;

    [Export]
    public string[] Options { get; set; } = ["Interact"];

    public bool CanInteract(Node interactor) => IsEnabled;

    public void Interact(Node interactor)
    {
        if (CanInteract(interactor))
        {
            EmitSignal(SignalName.Interacted, interactor);
        }
    }
}
