namespace Requests.Infrastructure.Exceptions
{
    /// <summary>
    /// Ответ с сообщением об ошибке
    /// </summary>
    public class NotFoundException : Exception
    {
        private NotFoundException(string message)
            : base(message)
        {
        }

        private NotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        private NotFoundException()
        {
        }

        public static NotFoundException Throw<TKey>(TKey key)
        {
            throw new NotFoundException($"Не удалось найти сущность {key}");
        }
    }
}
