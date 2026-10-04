public class Character
{
    public CharacterData Data { get; }
    public CharacterState State { get; }

    public Character(CharacterData data)
    {
        Data = data;
        State = new CharacterState { Health = data.MaxHealth, Hygiene = data.MaxHygiene };
    }

    public float GetSpeed()
    {
        return Data.BaseSpeed * State.SpeedModifier;
    }
}
