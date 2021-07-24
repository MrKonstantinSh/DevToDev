using System.Threading;
using System.Threading.Tasks;
using DevToDev.Application.Common.Interfaces;
using DevToDev.Application.Common.Security;
using DevToDev.Application.Identity.Dtos;
using MediatR;

namespace DevToDev.Application.Identity.Queries.GetCurrentUserInfo
{
    [Authorize(Roles = "User")]
    public class GetCurrentUserInfoQuery : IRequest<UserDto>
    {
    }

    public class GetCurrentUserQueryHandler : IRequestHandler<GetCurrentUserInfoQuery, UserDto>
    {
        private readonly ICurrentUserService _currentUserService;

        public GetCurrentUserQueryHandler(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }

        public async Task<UserDto> Handle(GetCurrentUserInfoQuery request, CancellationToken cancellationToken)
        {
            var currentUserInfo = new UserDto
            {
                Email = _currentUserService.Email,
                Username = _currentUserService.Username,
                FirstName = _currentUserService.FirstName,
                LastName = _currentUserService.LastName
            };

            return await Task.FromResult(currentUserInfo);
        }
    }
}