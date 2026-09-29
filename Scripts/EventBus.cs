using Godot;

public partial class EventBus : Node
{
    public static EventBus Instance { get; private set; }

    [Signal]
    public delegate void InteractionPromptEventHandler(string[] options, Node requester);

    [Signal]
    public delegate void InteractionPromptOptionSelectedEventHandler(int index, Node requester);

    [Signal]
    public delegate void InteractionPromptClearedEventHandler();

    public override void _Ready()
    {
        Instance = this;
    }
}
