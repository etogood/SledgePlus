using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SledgePlus.WPF.ViewModels.UserControls;

namespace SledgePlus.WPF.Factories;

public class ViewModelFactory : IFactory<ViewModel>
{
    private readonly IHost _host;

    public ViewModelFactory(IHost host)
    {
        _host = host;
    }

    public ViewModel Get(Type t)
    {
        if (t == typeof(AuthenticationViewModel)) return _host.Services.GetRequiredService<AuthenticationViewModel>();

        throw new NotImplementedException("ViewModel Type");
        
    }

}
