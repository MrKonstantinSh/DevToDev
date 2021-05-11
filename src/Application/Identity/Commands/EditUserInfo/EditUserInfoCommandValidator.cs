using System.Threading;
using System.Threading.Tasks;
using DevToDev.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace DevToDev.Application.Identity.Commands.EditUserInfo
{
    public class EditUserInfoCommandValidator : AbstractValidator<EditUserInfoCommand>
    {
        private readonly IAppDbContext _context;

        public EditUserInfoCommandValidator(IAppDbContext context)
        {
            _context = context;

            RuleFor(c => c.Username)
                .NotEmpty().WithMessage("Username is required.")
                .MaximumLength(25).WithMessage("Username must not exceed 25 characters.")
                .MustAsync(IsUniqueUsername).WithMessage("The specified username already taken.");

            RuleFor(c => c.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MaximumLength(255).WithMessage("First name must not exceed 255 characters.");

            RuleFor(c => c.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(255).WithMessage("Last name must not exceed 255 characters.");
        }

        public async Task<bool> IsUniqueUsername(string username, CancellationToken cancellationToken)
        {
            return await _context.Users
                .AllAsync(u => u.Username != username, cancellationToken);
        }
    }
}