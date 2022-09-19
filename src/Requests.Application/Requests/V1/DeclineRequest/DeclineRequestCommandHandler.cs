using MediatR;
using Requests.Application.Abstractions;
using Requests.Application.Exceptions;

namespace Requests.Application.Requests.V1.DeclineRequest
{
    public class DeclineRequestCommandHandler : IRequestHandler<DeclineRequestCommand, Unit>
    {
        private readonly IRequestRepository _requestRepository;

        public DeclineRequestCommandHandler(IRequestRepository requestRepository)
        {
            _requestRepository = requestRepository ?? throw new ArgumentNullException(nameof(requestRepository));
        }

        public async Task<Unit> Handle(DeclineRequestCommand request, CancellationToken cancellationToken)
        {
            var userRequest = await _requestRepository.GetByIdAsync(request.Id, cancellationToken);

            if (!Guid.TryParse(request.LdapUserId, out var userId))
            {
                throw new AppException("Не удалось распознать LdapUserId как Guid");
            }

            if (userRequest.PrimaryApprovers.Any(r => r.LdapUserId == userId))
            {
                var primaryApprover = userRequest.PrimaryApprovers
                    .FirstOrDefault(r => r.LdapUserId == userId);

                primaryApprover.Approved = false;
                await _requestRepository.UpdateRequest(userRequest, cancellationToken);
            }

            if (userRequest.SecondaryApprovers.Any(r => r.LdapUserId == userId))
            {
                var secondaryApprover = userRequest.SecondaryApprovers
                    .FirstOrDefault(r => r.LdapUserId == userId);

                secondaryApprover.Approved = false;
                await _requestRepository.UpdateRequest(userRequest, cancellationToken);
            }

            if (!userRequest.PrimaryApprovers.Any(r => r.LdapUserId == userId) && !userRequest.SecondaryApprovers.Any(r => r.LdapUserId == userId))
            {
                throw new AppException("Вы не можете согласовать данную заявку");
            }

            return Unit.Value;
        }
    }
}
