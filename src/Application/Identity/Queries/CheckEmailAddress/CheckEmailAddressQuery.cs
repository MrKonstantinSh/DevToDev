using System.Threading;
using System.Threading.Tasks;
using DevToDev.Application.Common.Interfaces;
using DevToDev.Application.Identity.Dtos;
using MediatR;

namespace DevToDev.Application.Identity.Queries.CheckEmailAddress
{
    public class CheckEmailAddressQuery : IRequest<EmailStatusDto>
    {
        public string Email { get; set; }
    }

    public class CheckEmailAddressQueryHandler : IRequestHandler<CheckEmailAddressQuery, EmailStatusDto>
    {
        private readonly IIdentityService _identityService;

        public CheckEmailAddressQueryHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<EmailStatusDto> Handle(CheckEmailAddressQuery request, CancellationToken cancellationToken)
        {
            bool isEmailAlreadyTaken = await _identityService.IsEmailAlreadyTaken(request.Email);

            return new EmailStatusDto
            {
                IsEmailAlreadyTaken = isEmailAlreadyTaken
            };
        }
    }
}