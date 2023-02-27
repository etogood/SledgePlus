namespace SledgePlus.WPF.Stores.WindowProperties;

public interface IWindowPropertiesStore
{
    public uint Height { get; set; }

    event Action HeightChanged;

    public uint Width { get; set; }

    event Action WidthChanged;
}