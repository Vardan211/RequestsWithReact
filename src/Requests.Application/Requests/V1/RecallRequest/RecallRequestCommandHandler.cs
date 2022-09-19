using MediatR;
using Requests.Application.Abstractions;
using Requests.Application.Exceptions;

namespace Requests.Application.Requests.V1.RecallRequest
{
    public class RecallRequestCommandHandler : IRequestHandler<RecallRequestCommand, Unit>
    {
        private readonly IRequestRepository _requestRepository;

        public RecallRequestCommandHandler(IRequestRepository requestRepository)
        {
            _requestRepository = requestRepository ?? throw new ArgumentNullException(nameof(requestRepository));
        }

        public async Task<Unit> Handle(RecallRequestCommand request, CancellationToken cancellationToken)
        {
            var userRequest = await _requestRepository.GetByIdAsync(request.Id, cancellationToken);

            if (!Guid.TryParse(request.LdapUserId, out var userId))
            {
                throw new AppException("Не удалось распознать LdapUserId как Guid");
            }

            if (userRequest.LdapUserId == userId)
            {
                await _requestRepository.DeleteRequest(userRequest, cancellationToken);
                return Unit.Value;
            }

            throw new AppException("Вы не можете отозвать данную заявку");
        }
    }
}
