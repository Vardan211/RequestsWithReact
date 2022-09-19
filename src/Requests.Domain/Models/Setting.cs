namespace Requests.Domain.Models
{
    /// <summary>
    /// Настройки
    /// </summary>
    public class Setting
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Ключ
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Значение
        /// </summary>
        public string Value { get; set; }
     }
}
