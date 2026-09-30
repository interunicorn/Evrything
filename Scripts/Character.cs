using Godot;

public partial class Character : CharacterBody2D
{
    [Export]
    public InteractionController Interaction { get; set; }

    [Export]
    public CharacterData Data { get; set; }

    public CharacterState State { get; private set; }

    public override void _Ready()
    {
        State = new CharacterState { Health = Data.MaxHealth, Hygiene = Data.MaxHygiene };

        Interaction ??= GetNodeOrNull<InteractionController>("Interaction");

        if (Interaction != null)
        {
            Interaction.SetInteractor(this);
        }
        else
        {
            GD.PushError("Player could not find InteractionController at 'Interaction'.");
        }
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event.IsActionPressed("Interact"))
        {
            Interaction?.TryInteract();
        }

        if (@event.IsActionPressed("CtrlModifier"))
        {
            State.SpeedModifier = 0.5f;
        }
        else if (@event.IsActionReleased("CtrlModifier"))
        {
            State.SpeedModifier = 1.0f;
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        Vector2 inputDirection = Input.GetVector("Left", "Right", "Up", "Down");

        Velocity = inputDirection * GetSpeed();

        if (inputDirection != Vector2.Zero)
        {
            Interaction?.UpdateFacingDirection(inputDirection);
        }

        MoveAndSlide();
    }

    public float GetSpeed()
    {
        return Data.BaseSpeed * State.SpeedModifier;
    }
}
