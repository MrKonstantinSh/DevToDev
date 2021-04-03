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

            RuleFor(c => c.Username)
                .NotEmpty().WithMessage("Username is required.")
                .MaximumLength(25).WithMessage("Username must not exceed 25 characters.")
                .MustAsync(IsUniqueUsername).WithMessage("The specified username already taken.");

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

            RuleFor(c => c.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MinimumLength(2).WithMessage("First name must be at least 2 characters long.")
                .MaximumLength(255).WithMessage("First name must not exceed 255 characters.");

            RuleFor(c => c.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MinimumLength(2).WithMessage("Last name must be at least 2 characters long.")
                .MaximumLength(255).WithMessage("Last name must not exceed 255 characters.");
        }

        public async Task<bool> IsUniqueUsername(string username, CancellationToken cancellationToken)
        {
            return await _context.Users
                .AllAsync(u => u.Username != username, cancellationToken: cancellationToken);
        }

        public async Task<bool> IsUniqueEmail(string email, CancellationToken cancellationToken)
        {
            return await _context.Users
                .AllAsync(u => u.Email != email, cancellationToken: cancellationToken);
        }
    }
}