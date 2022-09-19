using Requests.Domain.Models;

namespace Requests.Application.Abstractions
{
    /// <summary>
    /// Репозиторий для пользователей
    /// </summary>
    public interface ILdapUserRepository
    {
        /// <summary>
        /// Метод для получения пользователей по группам
        /// </summary>
        /// <param name="groups">Группы</param>
        /// <param name="token"><see cref="CancellationToken"/></param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public IReadOnlyCollection<LdapUser> GetUsersByGroups(string[] groups, CancellationToken token = default);

        /// <summary>
        /// Метод для получения пользователя
        /// </summary>
        /// <param name="uuid">Guid пользователя</param>
        /// <param name="token"><see cref="CancellationToken"/></param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public LdapUser GetUser(string uuid, CancellationToken token = default);
    }
}
