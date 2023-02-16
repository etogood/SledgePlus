using System.Runtime.Serialization;

namespace SledgePlus.WPF.Exceptions;

public class DuplicateException : Exception
{
    public DuplicateException()
    {
    }

    public DuplicateException(string? message) : this()
    {
    }

    public DuplicateException(string? message, Exception? innerException) : this()
    {
    }

    protected DuplicateException(SerializationInfo info, StreamingContext context)
    {
    }
}