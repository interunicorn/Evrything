using Godot;

public partial class Chest : StaticBody2D, IInteractable
{
    [Export]
    public bool IsEnabled { get; set; } = true;

    [Export]
    public AnimatedSprite2D Sprite { get; set; }

    public string[] Options { get; set; }

    private bool _isOpened = false;

    public override void _Ready()
    {
        if (EventBus.Instance != null)
        {
            EventBus.Instance.InteractionPromptOptionSelected += OnOptionSelected;
        }
    }

    public bool CanInteract(Node interactor) => IsEnabled;

    public void Interact(Node interactor)
    {
        if (!CanInteract(interactor))
            return;

        if (_isOpened)
        {
            Options = ["Close"];
            EventBus.Instance.EmitSignal(EventBus.SignalName.InteractionPrompt, Options, this);
        }
        else
        {
            Options = ["Open"];
            EventBus.Instance.EmitSignal(EventBus.SignalName.InteractionPrompt, Options, this);
        }
    }

    public void OnOptionSelected(int index, Node requester)
    {
        string option = Options[index];

        if (option == "Open")
        {
            Sprite.Play("Open");
            _isOpened = true;
        }
        else if (option == "Close")
        {
            Sprite.Play("Close");
            _isOpened = false;
        }
    }
}
