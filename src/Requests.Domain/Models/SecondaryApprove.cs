namespace Requests.Domain.Models
{
    /// <summary>
    /// Вторичное одобрение
    /// </summary>
    public class SecondaryApprove : IApprovable
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Идентификатор согласующего
        /// </summary>
        public Guid LdapUserId { get; set; }

        /// <summary>
        /// Идентификатор заявки
        /// </summary>
        public int RequestId { get; set; }

        /// <summary>
        /// Статус согласования
        /// </summary>
        public bool? Approved { get; set; }

        /// <summary>
        /// Имя группы согласующего
        /// </summary>
        public string GroupName { get; set; }

        public Request Request { get; set; }
    }
}
