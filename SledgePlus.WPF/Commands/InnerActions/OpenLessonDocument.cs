using System.Diagnostics;

namespace SledgePlus.WPF.Commands.InnerActions;

public class OpenLessonDocument : Command
{
    private readonly string _document;

    public OpenLessonDocument(IHost host, string document)
    {
        _document = document;
    }

    public override bool CanExecute(object? parameter) => true;

    public override void Execute(object? parameter)
    {
        try
        {
            var p = new Process()
            {
                StartInfo = new ProcessStartInfo(_document)
                {
                    UseShellExecute = true
                }
            };
            p.Start();
        }
        catch (Exception)
        {
            //TODO: Add to the error vm
        }
    }
}