﻿using System.Windows.Input;

namespace Waf.NewsReader.Applications.DataModels;

public class NavigationItem : Model
{
    private string title;
    private ICommand? command;
    private bool isCommandEnabled;
    private string iconGlyph;

    public NavigationItem(string title, string iconGlyph)
    {
        this.title = title;
        this.iconGlyph = iconGlyph;
    }

    public string Title
    {
        get => title;
        set => SetProperty(ref title, value);
    }

    public string IconGlyph
    {
        get => iconGlyph;
        set => SetProperty(ref iconGlyph, value);
    }

    public ICommand? Command
    {
        get => command;
        set
        {
            var oldCommand = command;
            if (SetProperty(ref command, value))
            {
                if (oldCommand != null) oldCommand.CanExecuteChanged -= CommandCanExecuteChanged;
                if (command != null) command.CanExecuteChanged += CommandCanExecuteChanged;
                IsCommandEnabled = command?.CanExecute(null) ?? false;
            }
        }
    }

    public bool IsCommandEnabled
    {
        get => isCommandEnabled;
        set => SetProperty(ref isCommandEnabled, value);
    }

    private void CommandCanExecuteChanged(object? sender, EventArgs e) => IsCommandEnabled = command?.CanExecute(null) ?? false;
}
