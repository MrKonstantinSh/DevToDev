using System.Threading;
using System.Threading.Tasks;
using DevToDev.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace DevToDev.Application.Identity.Commands.RegisterUser
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        private readonly IAppDbContext _context;

        public RegisterUserCommandValidator(IAppDbContext context)
        {
            _context = context;

            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Email must be correct.")
                .MaximumLength(254).WithMessage("Email must not exceed 254 characters.")
                .MustAsync(IsUniqueEmail).WithMessage("The specified email already taken.");

            RuleFor(c => c.Password)
                .NotEmpty().WithMessage("Password is required.")
                .Equal(c => c.RePassword).WithMessage("Password and RePassword must match.");

            RuleFor(c => c.RePassword)
                .NotEmpty().WithMessage("RePassword is required.");
        }

        public async Task<bool> IsUniqueEmail(string email, CancellationToken cancellationToken)
        {
            return await _context.Users
                .AllAsync(u => u.Email != email, cancellationToken: cancellationToken);
        }
    }
}