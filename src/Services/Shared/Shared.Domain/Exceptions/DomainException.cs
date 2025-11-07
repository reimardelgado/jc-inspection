using System.Net;

namespace Shared.Domain.Exceptions;

public class DomainException : Exception
{
    public HttpStatusCode? HttpStatusCode { get; private set; }

    public DomainException()
    {
    }

    public DomainException(string message)
        : base(message)
    {
    }

    public DomainException(string message, HttpStatusCode? httpStatusCode)
        : base(message)
    {
        HttpStatusCode = httpStatusCode;
    }

    public DomainException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public DomainException(string message, HttpStatusCode? httpStatusCode, Exception innerException)
        : base(message, innerException)
    {
        HttpStatusCode = httpStatusCode;
    }
}