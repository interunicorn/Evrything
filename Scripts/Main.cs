using Godot;

public partial class Main : Control
{
    [Export]
    public Player Player { get; set; }

    [Export]
    public UI UI { get; set; }

    public override void _Ready()
    {
        if (Player == null)
        {
            GD.PushError("Main: Player reference is missing.");
            return;
        }

        if (UI == null)
        {
            GD.PushError("Main: UI reference is missing.");
            return;
        }

        if (Player.Interaction == null)
        {
            GD.PushError("Main: Player is missing InteractionController.");
            return;
        }

        Player.Interaction.Menu = UI.InteractionMenu;
    }
}
