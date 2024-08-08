using System.Runtime.Serialization;

namespace StickerFactory.API.AWS.Cognito.Service.Models;

public class ExternalServiceException: Exception
{
    public ExternalServiceException()
    {
    }

    protected ExternalServiceException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public ExternalServiceException(string? message) : base(message)
    {
    }

    public ExternalServiceException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}