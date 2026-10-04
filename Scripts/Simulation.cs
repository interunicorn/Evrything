using Godot;

public partial class Simulation : Node
{
    public static Simulation Instance { get; private set; }

    [Export]
    public double TickInterval { get; set; } = 0.5;

    public long TickCount { get; private set; }

    private double _elapsed;

    public override void _EnterTree()
    {
        Instance = this;
    }

    public override void _ExitTree()
    {
        if (Instance == this)
            Instance = null;
    }

    public override void _Process(double delta)
    {
        _elapsed += delta;

        while (_elapsed >= TickInterval)
        {
            _elapsed -= TickInterval;
            Tick();
        }
    }

    private void Tick()
    {
        TickCount++;

        GD.Print($"Simulation tick: {TickCount}");

        // Later:
        // NeedsSystem.Tick();
        // JobSystem.Tick();
        // ProductionSystem.Tick();
    }
}
