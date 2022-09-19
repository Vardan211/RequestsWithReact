namespace Requests.Application.RequestTemplates.V1.GetRequestTemplateById
{
    /// <summary>
    /// Сущность представляющая собой модель шаблона заявок
    /// </summary>
    public class GetRequestTemplateByIdResponse
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
    }
}
