using System.Runtime.Serialization;

namespace SledgePlus.WPF.Exceptions;

public class IncorrectLoginException : Exception
{
    public IncorrectLoginException()
    {
    }

    public IncorrectLoginException(string? message) : this()
    {
    }

    public IncorrectLoginException(string? message, Exception? innerException) : this()
    {
    }

    protected IncorrectLoginException(SerializationInfo info, StreamingContext context)
    {
    }
}