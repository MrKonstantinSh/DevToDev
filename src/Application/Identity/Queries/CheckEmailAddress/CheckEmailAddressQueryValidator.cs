using FluentValidation;

namespace DevToDev.Application.Identity.Queries.CheckEmailAddress
{
    public class CheckEmailAddressQueryValidator : AbstractValidator<CheckEmailAddressQuery>
    {
        public CheckEmailAddressQueryValidator()
        {
            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Email must be correct.")
                .MaximumLength(254).WithMessage("Email must not exceed 254 characters.");
        }
    }
}