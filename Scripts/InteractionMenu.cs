using Godot;

public partial class InteractionMenu : Container
{
	[Export]
	public ItemList List { get; set; }

	private Node _requester;

	public override void _Ready()
	{
		Hide();
		if (EventBus.Instance != null)
		{
			EventBus.Instance.InteractionPrompt += OnInteractionPrompt;
		}

		if (List != null)
		{
			List.ItemActivated += OnOptionSelected;
		}
	}

	public override void _ExitTree()
	{
		if (EventBus.Instance != null)
		{
			EventBus.Instance.InteractionPrompt -= OnInteractionPrompt;
		}

		if (List != null)
		{
			List.ItemActivated -= OnOptionSelected;
		}
	}

	private void OnInteractionPrompt(string[] options, Node requester)
	{
		List.Clear();

		if (options == null || options.Length == 0)
		{
			_requester = null;
			Hide();
			return;
		}

		_requester = requester;

		foreach (string option in options)
		{
			List.AddItem(option);
		}

		Show();
		List.GrabFocus();

		if (List.ItemCount > 0)
			List.Select(0);
	}

	private void OnOptionSelected(long index)
	{
		List.ReleaseFocus();
		Hide();
		EventBus.Instance.EmitSignal(
			EventBus.SignalName.InteractionPromptOptionSelected,
			(int)index,
			_requester
		);
	}
}
