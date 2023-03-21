using SledgePlus.WPF.ViewModels.UserControls.UserPanels;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SledgePlus.WPF.ViewModels.Base;

public class ViewModel : INotifyPropertyChanged
{
    #region INotifyPropertyChanged implementation

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string name = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

    protected bool Set<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }

    #endregion INotifyPropertyChanged implementation

    #region Default properties

    private uint _width;

    public uint Width
    {
        get => _width;
        set => Set(ref _width, value);
    }

    private uint _height;

    public uint Height
    {
        get => _height;
        set => Set(ref _height, value);
    }

    #endregion Default properties
}