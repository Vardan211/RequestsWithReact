using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Requests.Application.Abstractions;
using Requests.Domain.Models;
using Requests.Infrastructure.Exceptions;

namespace Requests.Infrastructure.Repositories
{
    /// <inheritdoc/>
    internal class RequestRepository : IRequestRepository
    {
        private readonly RequestsContext _context;

        public RequestRepository(RequestsContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <inheritdoc/>
        public async Task<int> CreateAsync(Request request, CancellationToken token = default)
        {
            var data = (JObject)JsonConvert.DeserializeObject(request.RequestData);
            var authorName = data["controls"][0]["value"].Value<string>();

            request.AuthorName = authorName;
            request.CreateDate = DateTime.UtcNow;

            await _context.Requests
                .AddAsync(request, token);

            await _context
                .SaveChangesAsync(token);

            return request.Id;
        }

        /// <inheritdoc/>
        public async Task<IReadOnlyList<Request>> GetAllRequestsAsync(CancellationToken token = default)
        {
            var requests = await _context.Requests
                .Include(r => r.RequestTemplate)
                .Include(r => r.PrimaryApprovers)
                .Include(r => r.SecondaryApprovers).ToListAsync(token);

            return requests;
        }

        /// <inheritdoc/>
        public async Task<Request> GetByIdAsync(int id, CancellationToken token = default)
        {
            var request = await _context.Requests
               .Include(r => r.PrimaryApprovers)
               .Include(r => r.SecondaryApprovers)
              .FirstOrDefaultAsync(r => r.Id == id, token);

            if (request == null)
            {
                NotFoundException.Throw(id);
            }

            return request;
        }

        /// <inheritdoc/>
        public async Task<IReadOnlyList<Request>> GetRequestsByUserIdAsync(Guid userId, CancellationToken token = default)
        {
            var requests = await _context.Requests
                 .Include(x => x.PrimaryApprovers)
                 .Include(x => x.SecondaryApprovers)
                 .Include(x => x.RequestTemplate)
                 .Where(x => x.LdapUserId == userId ||
                     x.PrimaryApprovers.Any(pa => pa.LdapUserId == userId) ||
                     x.SecondaryApprovers.Any(sa => sa.LdapUserId == userId))
                 .ToListAsync(token);

            return requests;
        }

        /// <inheritdoc/>
        public async Task UpdateRequest(Request request, CancellationToken token = default)
        {
            _context.Entry(request).State = EntityState.Modified;
            await _context.SaveChangesAsync(token);
        }

        /// <inheritdoc/>
        public async Task DeleteRequest(Request request, CancellationToken token = default)
        {
            _context.Entry(request).State = EntityState.Deleted;
            await _context.SaveChangesAsync(token);
        }
    }
}
