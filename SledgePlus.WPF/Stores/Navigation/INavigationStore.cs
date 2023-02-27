namespace SledgePlus.WPF.Stores.Navigation;

public interface INavigationStore
{
    ViewModel CurrentViewModel { get; set; }

    event Action CurrentViewModelChanged;
}