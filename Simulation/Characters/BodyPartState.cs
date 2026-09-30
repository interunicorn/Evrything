public class BodyPartState
{
    public BodyPartData Data { get; }
    public float Health { get; set; }

    public BodyPartState(BodyPartData data)
    {
        Data = data;
        Health = data.MaxHealth;
    }
}
