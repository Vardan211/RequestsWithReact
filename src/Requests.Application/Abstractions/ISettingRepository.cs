using Requests.Domain.Models;

namespace Requests.Application.Abstractions
{
    /// <summary>
    /// Репозиторий для настроек
    /// </summary>
    public interface ISettingRepository
    {
        /// <summary>
        /// Получить настройку по ключу
        /// </summary>
        /// <param name="key">Ключ</param>
        /// <param name="token"><see cref="CancellationToken"/></param>
        /// <returns></returns>
        public Task<Setting> GetSettingByKeyAsync(string key, CancellationToken token = default);

        /// <summary>
        /// Получить все настройки
        /// </summary>
        /// <param name="token"><see cref="CancellationToken"/></param>
        /// <returns></returns>
        public Task<IReadOnlyList<Setting>> GetAllSettingsAsync(CancellationToken token = default);

        /// <summary>
        /// Создать настройку
        /// </summary>
        /// <param name="setting"><see cref="Setting"/></param>
        /// <param name="token"><see cref="CancellationToken"/></param>
        /// <returns></returns>
        public Task CreateSettingAsync(Setting setting, CancellationToken token = default);

        /// <summary>
        /// Изменить настройку
        /// </summary>
        /// <param name="setting"><see cref="Setting"/></param>
        /// <param name="token"><see cref="CancellationToken"/></param>
        /// <returns></returns>
        public Task UpdateSettingAsync(Setting setting, CancellationToken token = default);

        /// <summary>
        /// Удалить настройку
        /// </summary>
        /// <param name="setting"><see cref="Setting"/></param>
        /// <param name="token"><see cref="CancellationToken"/></param>
        /// <returns></returns>
        public Task DeleteSettingAsync(Setting setting, CancellationToken token = default);
    }
}
