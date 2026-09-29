using Godot;

public partial class InteractionComponent : RayCast2D
{
    [Export]
    public float InteractionDistance { get; set; } = 32.0f;

    public override void _Ready()
    {
        CollideWithAreas = true;
        TargetPosition = new Vector2(0, InteractionDistance);
    }

    public void UpdateFacingDirection(Vector2 direction)
    {
        if (direction != Vector2.Zero)
        {
            TargetPosition = direction.Normalized() * InteractionDistance;
        }
    }

    public bool TryInteract(Node interactorNode)
    {
        ForceRaycastUpdate();

        if (IsColliding() && GetCollider() is IInteractable interactable)
        {
            if (interactable.IsEnabled)
            {
                interactable.Interact(interactorNode);
                return true;
            }
        }

        return false;
    }
}
