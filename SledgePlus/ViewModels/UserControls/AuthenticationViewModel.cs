using Microsoft.Extensions.Hosting;
using SledgePlus.WPF.ViewModels.Base;

namespace SledgePlus.WPF.ViewModels.UserControls
{
    internal sealed class AuthenticationViewModel : ViewModel
    {
        public AuthenticationViewModel(IHost host)
        {
            Height = 360;
            Width  = 200;
        }
    }
}
