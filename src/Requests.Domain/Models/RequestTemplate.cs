namespace Requests.Domain.Models
{
    /// <summary>
    /// Щаблон заявки
    /// </summary>
    public class RequestTemplate
    {
        /// <summary>
        /// Идентификатор шаблона
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Шаблон
        /// </summary>
        public string Template { get; set; }

        public ICollection<Request> Requests { get; set; }
    }
}
