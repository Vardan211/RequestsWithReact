namespace Requests.Application.Requests.V1.GetRequest
{
    /// <summary>
    /// Ответ на запрос заявки
    /// </summary>
    public class GetRequestResponse
    {
        /// <summary>
        /// ID заявки
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// JSON с данными заявки
        /// </summary>
        public string RequestData { get; set; }

        /// <summary>
        /// ID автора заявки LDAP
        /// </summary>
        public string LdapUserId { get; set; }

        /// <summary>
        /// ФИО автора заявки
        /// </summary>
        public string AuthorName { get; set; }

        /// <summary>
        /// Подтверждение
        /// </summary>
        public bool? IsApproved { get; set; }

        /// <summary>
        /// ID апруверов уровня approverGroups
        /// </summary>
        public LdapUserResponse[] PrimaryApproverGroupLdapUsers { get; set; }

        /// <summary>
        /// ID апруверов уровня solutionGroups
        /// </summary>
        public LdapUserResponse[] SecondaryApproverGroupLdapUsers { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetRequestResponse"/> class
        /// </summary>
        public GetRequestResponse()
        {
            PrimaryApproverGroupLdapUsers = Array.Empty<LdapUserResponse>();
            SecondaryApproverGroupLdapUsers = Array.Empty<LdapUserResponse>();
        }
    }
}
