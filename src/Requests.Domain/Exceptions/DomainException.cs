namespace Requests.Domain.Exceptions
{
    /// <summary>
    /// Исключение слоя Domain
    /// </summary>
    public class DomainException : Exception
    {
        public DomainException(string message)
            : base(message)
        {
        }

        public DomainException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public DomainException()
        {
        }
    }
}
