using System;
using System.Collections.Generic;
using Godot;

public partial class InteractionMenu : Container
{
    [Export]
    public ItemList List { get; set; }

    private Action<int> _onSelected;

    public override void _Ready()
    {
        Hide();

        if (List != null)
        {
            List.ItemActivated += OnOptionSelected;
        }
    }

    public override void _ExitTree()
    {
        if (List != null)
        {
            List.ItemActivated -= OnOptionSelected;
        }

        _onSelected = null;
    }

    public void ShowOptions(IReadOnlyList<string> options, Action<int> onSelected)
    {
        if (List == null)
        {
            GD.PushError("InteractionMenu has no ItemList assigned.");
            return;
        }

        List.Clear();

        if (options == null || options.Count == 0)
        {
            HideMenu();
            return;
        }

        _onSelected = onSelected;

        foreach (string option in options)
        {
            List.AddItem(option);
        }

        Show();

        List.GrabFocus();
        List.Select(0);
    }

    public void HideMenu()
    {
        _onSelected = null;

        if (List != null)
        {
            List.ReleaseFocus();
            List.Clear();
        }

        Hide();
    }

    private void OnOptionSelected(long index)
    {
        Action<int> callback = _onSelected;

        _onSelected = null;

        Hide();

        if (List != null)
        {
            List.ReleaseFocus();
        }

        callback?.Invoke((int)index);
    }
}
