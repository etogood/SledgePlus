namespace SledgePlus.WPF.Factories;

public interface IFactory<out T>
{
    public T Get(Type t);
}