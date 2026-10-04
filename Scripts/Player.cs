using Godot;

public partial class Player : CharacterBody2D
{
    [Export]
    public CharacterData Data { get; set; }

    [Export]
    public InteractionController Interaction { get; set; }

    public Character Character { get; private set; }

    public override void _Ready()
    {
        Interaction ??= GetNodeOrNull<InteractionController>("Interaction");

        if (Interaction != null)
            Interaction.SetInteractor(this);
        else
            GD.PushError("Missing InteractionController.");

        if (Data == null)
        {
            GD.PushError("Player is missing CharacterData");
        }
        Character = new Character(Data);
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event.IsActionPressed("Interact"))
            Interaction?.TryInteract();

        if (@event.IsActionPressed("CtrlModifier"))
            Character.State.SpeedModifier = 0.5f;
        else if (@event.IsActionReleased("CtrlModifier"))
            Character.State.SpeedModifier = 1.0f;
    }

    public override void _PhysicsProcess(double delta)
    {
        Vector2 direction = Input.GetVector("Left", "Right", "Up", "Down");

        Velocity = direction * Character.GetSpeed();

        if (direction != Vector2.Zero)
            Interaction?.UpdateFacingDirection(direction);

        MoveAndSlide();
    }
}
