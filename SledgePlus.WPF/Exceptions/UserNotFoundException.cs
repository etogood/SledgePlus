using System.Runtime.Serialization;

namespace SledgePlus.WPF.Exceptions;

public class UserNotFoundException : Exception
{
    public UserNotFoundException()
    {
    }

    public UserNotFoundException(string? message) : this()
    {
    }

    public UserNotFoundException(string? message, Exception? innerException) : this()
    {
    }

    protected UserNotFoundException(SerializationInfo info, StreamingContext context)
    {
    }
}