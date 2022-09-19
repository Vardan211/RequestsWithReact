using Microsoft.EntityFrameworkCore;
using Requests.Application.Abstractions;
using Requests.Domain.Models;
using Requests.Infrastructure.Exceptions;

namespace Requests.Infrastructure.Repositories
{
    /// <inheritdoc/>
    internal class SettingRepository : ISettingRepository
    {
        private readonly RequestsContext _context;

        public SettingRepository(RequestsContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <inheritdoc/>
        public async Task CreateSettingAsync(Setting setting, CancellationToken token = default)
        {
            await _context.Settings
                .AddAsync(setting, token);

            await _context
                .SaveChangesAsync(token);
        }

        /// <inheritdoc/>
        public async Task DeleteSettingAsync(Setting setting, CancellationToken token = default)
        {
            _context.Entry(setting).State = EntityState.Deleted;
            await _context.SaveChangesAsync(token);
        }

        /// <inheritdoc/>
        public async Task<Setting> GetSettingByKeyAsync(string key, CancellationToken token = default)
        {
            var setting = await _context.Settings
              .FirstOrDefaultAsync(r => r.Key == key, token);

            if (setting == null)
            {
                NotFoundException.Throw(key);
            }

            return setting;
        }

        /// <inheritdoc/>
        public async Task<IReadOnlyList<Setting>> GetAllSettingsAsync(CancellationToken token = default)
        {
            var settings = await _context.Settings.ToListAsync(token);
            return settings;
        }

        /// <inheritdoc/>
        public async Task UpdateSettingAsync(Setting setting, CancellationToken token = default)
        {
            _context.Entry(setting).State = EntityState.Modified;
            await _context.SaveChangesAsync(token);
        }
    }
}
