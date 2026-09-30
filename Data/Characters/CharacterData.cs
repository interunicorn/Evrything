using Godot;

[GlobalClass]
public partial class CharacterData : Resource
{
    [Export]
    public string Name { get; set; }

    [Export]
    public float BaseSpeed { get; set; } = 200.0f;

    [Export]
    public float MaxHealth { get; set; } = 100.0f;

    [Export]
    public float MaxHygiene { get; set; } = 100.0f;

    [Export]
    public Godot.Collections.Array<BodyPartData> BodyParts { get; set; }
}
