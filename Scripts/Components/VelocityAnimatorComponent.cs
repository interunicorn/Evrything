using Godot;

public partial class VelocityAnimatorComponent : AnimatedSprite2D
{
    [Export]
    public CharacterBody2D Character { get; set; }

    [Export]
    public float SpeedThreshold { get; set; } = 10.0f;

    private Vector2 _lastDirection = Vector2.Down;
    private string _currentAnimation = "";

    public override void _Ready()
    {
        Character ??= GetParent<CharacterBody2D>();
    }

    public override void _Process(double delta)
    {
        if (Character == null)
            return;

        Vector2 velocity = Character.Velocity;

        bool isMoving = velocity.LengthSquared() > SpeedThreshold * SpeedThreshold;

        if (isMoving)
        {
            _lastDirection = GetCardinalDirection(velocity);
        }

        string state = isMoving ? "Walk" : "Idle";
        string direction = DirectionToString(_lastDirection);

        string newAnimation = $"{state} {direction}";

        if (_currentAnimation == newAnimation)
            return;

        _currentAnimation = newAnimation;
        Play(_currentAnimation);
    }

    private Vector2 GetCardinalDirection(Vector2 velocity)
    {
        if (Mathf.Abs(velocity.X) > Mathf.Abs(velocity.Y))
        {
            return velocity.X > 0 ? Vector2.Right : Vector2.Left;
        }

        return velocity.Y > 0 ? Vector2.Down : Vector2.Up;
    }

    private string DirectionToString(Vector2 direction)
    {
        if (direction == Vector2.Left)
            return "Left";

        if (direction == Vector2.Right)
            return "Right";

        if (direction == Vector2.Up)
            return "Up";

        return "Down";
    }
}
