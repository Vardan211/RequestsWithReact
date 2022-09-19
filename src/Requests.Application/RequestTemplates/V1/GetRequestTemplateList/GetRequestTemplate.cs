namespace Requests.Application.RequestTemplates.V1.GetRequestTemplateList
{
    /// <summary>
    /// Сущность представляющая собой модель шаблона заявок
    /// </summary>
    public class GetRequestTemplate
    {
        /// <summary>
        /// Идентификатор шаблона
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; }
    }
}
