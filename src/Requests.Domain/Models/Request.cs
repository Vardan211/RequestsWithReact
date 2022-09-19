using System.Text.Json;
using Requests.Domain.Models.RequestStructure;

namespace Requests.Domain.Models
{
    /// <summary>
    /// Заявка
    /// </summary>
    public class Request
    {
        /// <summary>
        /// Идентификатор заявки
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Данные заявки
        /// </summary>
        public string RequestData { get; set; }

        /// <summary>
        /// ФИО автора заявки
        /// </summary>
        public string AuthorName { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// Внешный ключ для RequestTemplate
        /// </summary>
        public int RequestTemplateId { get; set; }

        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public Guid LdapUserId { get; set; }

        public RequestTemplate RequestTemplate { get; set; }

        public ICollection<PrimaryApprove> PrimaryApprovers { get; set; }

        public ICollection<SecondaryApprove> SecondaryApprovers { get; set; }

        public RequestData RequestDataToObject(string requestsDataJson)
        {
            var requestData = JsonSerializer.Deserialize<RequestData>(requestsDataJson);
            return requestData;
        }
    }
}
