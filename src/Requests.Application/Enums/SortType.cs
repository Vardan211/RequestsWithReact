using System.Runtime.Serialization;

namespace Requests.Application.Enums
{
    /// <summary>
    /// Тип сортировки
    /// </summary>
    public enum SortType
    {
        /// <summary>
        /// По возрастанию
        /// </summary>
        [EnumMember(Value = @"ASC")]
        Asc = 0,

        /// <summary>
        /// По убыванию
        /// </summary>
        [EnumMember(Value = @"DESC")]
        Desc = 1,
    }
}
