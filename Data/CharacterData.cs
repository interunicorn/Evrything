using Godot;

[GlobalClass]
public partial class CharacterData : Resource
{
    [Export]
    public string Name { get; set; }

    [Export]
    public float Speed { get; set; }

    [Export]
    public Godot.Collections.Array<BodyPartData> BodyParts { get; set; }
}
