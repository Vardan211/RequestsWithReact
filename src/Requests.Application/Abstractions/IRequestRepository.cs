using Requests.Application.Enums;
using Requests.Domain.Models;

namespace Requests.Application.Abstractions
{
    /// <summary>
    /// Репозиторий для заявок
    /// </summary>
    public interface IRequestRepository
    {
        /// <summary>
        /// Метод для создания заявки
        /// </summary>
        /// <param name="request"><see cref="Request"/></param>
        /// <param name="token"><see cref="CancellationToken"/></param>
        /// <returns></returns>
        public Task<int> CreateAsync(Request request, CancellationToken token = default);

        /// <summary>
        /// Метод для получения заявки
        /// </summary>
        /// <param name="id">Идентификатор Заявки</param>
        /// <param name="token"><see cref="CancellationToken"/></param>
        /// <returns></returns>
        public Task<Request> GetByIdAsync(int id, CancellationToken token = default);

        /// <summary>
        /// Метод для получения всех заявок по Id
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <param name="token"><see cref="CancellationToken"/></param>
        /// <returns></returns>
        public Task<IReadOnlyList<Request>> GetRequestsByUserIdAsync(Guid userId, CancellationToken token = default);

        /// <summary>
        /// Метод для получения всех заявок
        /// </summary>
        /// <param name="token"><see cref="CancellationToken"/></param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public Task<IReadOnlyList<Request>> GetAllRequestsAsync(CancellationToken token = default);

        /// <summary>
        /// Метод для обновления заявки
        /// </summary>
        /// <param name="request"><see cref="Request"/></param>
        /// <param name="token"><see cref="CancellationToken"/></param>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        public Task UpdateRequest(Request request, CancellationToken token = default);

        /// <summary>
        /// Метод для удаления заявки
        /// </summary>
        /// <param name="request"><see cref="Request"/></param>
        /// <param name="token"><see cref="CancellationToken"/></param>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        public Task DeleteRequest(Request request, CancellationToken token = default);
    }
}
