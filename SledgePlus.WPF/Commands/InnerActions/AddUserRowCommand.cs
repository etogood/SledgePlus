namespace SledgePlus.WPF.Commands.InnerActions;

public class AddUserRowCommand : Command
{
    private readonly IHost _host;
    public AddUserRowCommand(IHost host)
    {
        _host = host;
    }

    public override bool CanExecute(object? parameter) => true;

    public override void Execute(object? parameter)
    {
        
    }
}