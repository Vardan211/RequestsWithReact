namespace Requests.Application.Requests.V1.GetRequests
{
    /// <summary>
    /// Своя заявка
    /// </summary>
    public class RequestResponse
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
        /// Статус согласования заявки
        /// </summary>
        public bool? IsApproved { get; set; }
    }
}
