using Godot;

public partial class Player : CharacterBody2D
{
    [Export]
    public float Speed { get; set; } = 200.0f;

    [Export]
    public InteractionController Interaction { get; set; }

    public override void _Ready()
    {
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
        if (@event.IsActionPressed("interact"))
        {
            Interaction?.TryInteract();
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        Vector2 inputDirection = Input.GetVector("left", "right", "up", "down");

        Velocity = inputDirection * Speed;

        if (inputDirection != Vector2.Zero)
        {
            Interaction?.UpdateFacingDirection(inputDirection);
        }

        MoveAndSlide();
    }
}
