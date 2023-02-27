using Microsoft.Extensions.DependencyInjection;
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
        if (t == typeof(SignInViewModel)) return _host.Services.GetRequiredService<SignInViewModel>();
        if (t == typeof(IDEViewModel)) return _host.Services.GetRequiredService<IDEViewModel>();
        if (t == typeof(LearningMenuViewModel)) return _host.Services.GetRequiredService<LearningMenuViewModel>();
        if (t == typeof(PersonalAccountViewModel)) return _host.Services.GetRequiredService<PersonalAccountViewModel>();
        if (t == typeof(UserMenuViewModel)) return _host.Services.GetRequiredService<UserMenuViewModel>();

        throw new NotImplementedException("ViewModel Type");
    }
}