using FluentValidation;

namespace DevToDev.Application.Article.Commands.UpdateArticleCommand
{
    public class UpdateArticleCommandValidator : AbstractValidator<UpdateArticleCommand>
    {
        public UpdateArticleCommandValidator()
        {
            RuleFor(c => c.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(100).WithMessage("Title must not exceed 100 characters.");

            RuleFor(c => c.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(254).WithMessage("Description must not exceed 254 characters.");

            RuleFor(c => c.Content)
                .NotEmpty().WithMessage("Content is required.");
        }
    }
}