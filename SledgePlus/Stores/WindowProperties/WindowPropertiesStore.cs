namespace SledgePlus.WPF.Stores.WindowProperties;

public class WindowPropertiesStore : IWindowPropertiesStore
{
    private uint _height;

    public uint Height
    {
        get => _height;
        set
        {
            _height = value;
            HeightChanged?.Invoke();
        }
    }

    public event Action? HeightChanged;


    private uint _width;

    public uint Width
    {
        get => _width;
        set
        {
            _width = value;
            WidthChanged?.Invoke();
        }
    }
    public event Action? WidthChanged;
}