namespace Requests.Application.Exceptions
{
    /// <summary>
    /// Исключение слоя Application
    /// </summary>
    public class AppException : Exception
    {
        public AppException(string message)
            : base(message)
        {
        }

        public AppException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public AppException()
        {
        }
    }
}
