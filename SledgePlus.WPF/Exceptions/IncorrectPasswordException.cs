using System.Runtime.Serialization;

namespace SledgePlus.WPF.Exceptions;

public class IncorrectPasswordException : Exception
{
    public IncorrectPasswordException()
    {
    }

    public IncorrectPasswordException(string? message) : this()
    {
    }

    public IncorrectPasswordException(string? message, Exception? innerException) : this()
    {
    }

    protected IncorrectPasswordException(SerializationInfo info, StreamingContext context)
    {
    }
}