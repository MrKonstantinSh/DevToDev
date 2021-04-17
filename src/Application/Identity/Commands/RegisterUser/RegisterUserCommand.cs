using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DevToDev.Application.Common.Interfaces;
using DevToDev.Domain.Entities.Identity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevToDev.Application.Identity.Commands.RegisterUser
{
    public class RegisterUserCommand : IRequest<int>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string RePassword { get; set; }
    }

    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, int>
    {
        private readonly IAppDbContext _context;
        private readonly ITokenService _tokenService;
        private readonly IDateTimeService _dateTimeService;
        private readonly IHashPasswordService _hashPasswordService;

        public RegisterUserCommandHandler(IAppDbContext context, IHashPasswordService hashPasswordService,
            IDateTimeService dateTimeService, ITokenService tokenService)
        {
            _context = context;

            _dateTimeService = dateTimeService;
            _tokenService = tokenService;
            _hashPasswordService = hashPasswordService;
        }

        public async Task<int> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User
            {
                Username = request.Email.Split('@').First(),
                Email = request.Email,
                PasswordHash = _hashPasswordService.HashPassword(request.Password),
                RegistrationDate = _dateTimeService.UtcNow,
                EmailVerificationToken = _tokenService.GenerateConfirmationToken()
            };

            var userDetails = new UserDetails
            {
                User = user
            };

            await _context.Users.AddAsync(user, cancellationToken);
            await _context.UserDetails.AddAsync(userDetails, cancellationToken);

            var defaultRole = await _context.Roles.FirstOrDefaultAsync(r => r.Name == "User", cancellationToken);

            if (defaultRole != null)
            {
                user.Roles.Add(defaultRole);
            }

            await _context.SaveChangesAsync(cancellationToken);

            // TODO: Send confirmation email.

            return user.Id;
        }
    }
}