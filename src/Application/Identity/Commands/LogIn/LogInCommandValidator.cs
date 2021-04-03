using System.Threading;
using System.Threading.Tasks;
using DevToDev.Application.Common.Interfaces;
using DevToDev.Domain.Entities.Identity;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace DevToDev.Application.Identity.Commands.LogIn
{
    public class LogInCommandValidator : AbstractValidator<LogInCommand>
    {
        private readonly IAppDbContext _context;
        private readonly IHashPasswordService _hashPasswordService;

        private User _user;

        public LogInCommandValidator(IAppDbContext context, IHashPasswordService hashPasswordService)
        {
            _context = context;
            _hashPasswordService = hashPasswordService;

            RuleFor(c => c.UsernameOrEmail)
                .MustAsync(IsExistUsernameOrEmail).WithMessage("Username or email is incorrect.");

            RuleFor(c => c.Password)
                .Must(IsPasswordCorrect).WithMessage("Password is incorrect.");

            RuleFor(c => c.Fingerprint)
                .NotEmpty().WithMessage("Fingerprint is required.")
                .MaximumLength(60).WithMessage("Fingerprint must not exceed 60 characters.");
        }

        public async Task<bool> IsExistUsernameOrEmail(string usernameOrEmail, CancellationToken cancellationToken)
        {
            _user = await _context.Users.SingleOrDefaultAsync(u => u.Username == usernameOrEmail, cancellationToken) ??
                    await _context.Users.SingleOrDefaultAsync(u => u.Email == usernameOrEmail, cancellationToken);

            return _user != null;
        }

        public bool IsPasswordCorrect(string password)
        {
            return _user != null && _hashPasswordService.VerifyPassword(password, _user.PasswordHash);
        }
    }
}