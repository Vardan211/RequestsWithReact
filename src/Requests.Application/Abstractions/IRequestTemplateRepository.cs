using Requests.Domain.Models;

namespace Requests.Application.Abstractions
{
    /// <summary>
    /// Репозиторий для шаблонов заявок
    /// </summary>
    public interface IRequestTemplateRepository
    {
        /// <summary>
        /// Метод для получения шаблона по идентификатору
        /// </summary>
        /// <param name="id">Индентификатор шаблона</param>
        /// <param name="token"><see cref="CancellationToken"/></param>
        /// <returns></returns>
        public Task<RequestTemplate> GetRequestTemplateByIdAsync(int id, CancellationToken token = default);

        /// <summary>
        /// Метод для получения всех шаблонов
        /// </summary>
        /// <param name="token"><see cref="CancellationToken"/></param>
        /// <returns></returns>
        public Task<IReadOnlyList<RequestTemplate>> GetRequestTemplateListAsync(CancellationToken token = default);
    }
}
