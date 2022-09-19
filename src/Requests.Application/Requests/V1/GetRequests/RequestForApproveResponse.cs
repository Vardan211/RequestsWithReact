namespace Requests.Application.Requests.V1.GetRequests
{
    /// <summary>
    /// Назначенная на согласование заявка
    /// </summary>
    public class RequestForApproveResponse
    {
        /// <summary>
        /// ID заявки
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название заявки
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// ФИО автора заявки
        /// </summary>
        public string AuthorName { get; set; }

        /// <summary>
        /// Статус согласования заявки
        /// </summary>
        public bool? IsApproved { get; set; }
    }
}
