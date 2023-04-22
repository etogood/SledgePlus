﻿namespace SledgePlus.WPF.Commands.Base;

public class CloseApplicationCommand : Command
{
    public override bool CanExecute(object? parameter) => true;

    public override void Execute(object? parameter)
    {
        Application.Current.Shutdown();
    }
}