namespace Requests.Application.Requests.V1.GetRequests
{
    /// <summary>
    /// Ответ на запрос заявкок для пользователя LDAP
    /// </summary>
    public class GetRequestsResponse
    {
        /// <summary>
        /// Список своих заявок
        /// </summary>
        public RequestResponse[] Requests { get; set; }

        /// <summary>
        /// Список назначенных на согласование заявок
        /// </summary>
        public RequestForApproveResponse[] RequestForApprove { get; set; }

        /// <summary>
        /// Список всех заявок (для супер пользователя)
        /// </summary>
        public RequestForApproveResponse[] AllRequests { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetRequestsResponse"/> class
        /// </summary>
        public GetRequestsResponse()
        {
            Requests = Array.Empty<RequestResponse>();
            RequestForApprove = Array.Empty<RequestForApproveResponse>();
            AllRequests = Array.Empty<RequestForApproveResponse>();
        }
    }
}
