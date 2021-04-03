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
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string RePassword { get; set; }
    }

    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, int>
    {
        private readonly IAppDbContext _context;
        private readonly IHashService _hashService;
        private readonly IDateTimeService _dateTimeService;
        private readonly IConfirmationTokenService _confirmationTokenService;

        public RegisterUserCommandHandler(IAppDbContext context, IHashService hashService,
            IDateTimeService dateTimeService, IConfirmationTokenService confirmationTokenService)
        {
            _context = context;

            _hashService = hashService;
            _dateTimeService = dateTimeService;
            _confirmationTokenService = confirmationTokenService;
        }

        public async Task<int> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User
            {
                Username = request.Username,
                Email = request.Email,
                PasswordHash = _hashService.Hash(request.Password),
                RegistrationDate = _dateTimeService.UtcNow,
                EmailVerificationToken = _confirmationTokenService.GenerateToken(255)
            };

            var userDetails = new UserDetails
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
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