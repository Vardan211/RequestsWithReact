using Microsoft.EntityFrameworkCore;
using Requests.Application.Abstractions;
using Requests.Domain.Models;
using Requests.Infrastructure.Exceptions;

namespace Requests.Infrastructure.Repositories
{
    /// <inheritdoc/>
    internal class RequestTemplateRepository : IRequestTemplateRepository
    {
        private readonly RequestsContext _context;

        public RequestTemplateRepository(RequestsContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <inheritdoc/>
        public async Task<RequestTemplate> GetRequestTemplateByIdAsync(int id, CancellationToken token = default)
        {
            var template = await _context.RequestTemplates
                .FirstOrDefaultAsync(r => r.Id == id, token);

            if (template == null)
            {
                NotFoundException.Throw(id);
            }

            return template;
        }

        /// <inheritdoc/>
        public async Task<IReadOnlyList<RequestTemplate>> GetRequestTemplateListAsync(CancellationToken token = default)
        {
            return await _context.RequestTemplates
                .ToListAsync(token);
        }
    }
}
