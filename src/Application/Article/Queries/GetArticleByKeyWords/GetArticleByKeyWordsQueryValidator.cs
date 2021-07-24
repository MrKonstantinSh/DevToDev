using FluentValidation;

namespace DevToDev.Application.Article.Queries.GetArticleByKeyWords
{
    public class GetArticleByKeyWordsQueryValidator : AbstractValidator<GetArticleByKeyWordsQuery>
    {
        public GetArticleByKeyWordsQueryValidator()
        {
            RuleFor(q => q.SearchString)
                .NotEmpty().WithMessage("SearchString is required.");
        }
    }
}