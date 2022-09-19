namespace Requests.Application.RequestTemplates.V1.GetRequestTemplateList
{
    /// <summary>
    /// Сущность представляющая собой cписок шаблонов заявок
    /// </summary>
    public class GetRequestTemplateListResponse
    {
        /// <summary>
        /// Список шаблонов
        /// </summary>
        public IReadOnlyCollection<GetRequestTemplate> Templates { get; set; }
    }
}
