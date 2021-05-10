using System.Threading;
using System.Threading.Tasks;
using DevToDev.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace DevToDev.Application.Article.Queries.GetArticleById
{
    public class GetArticleByIdQueryValidator : AbstractValidator<GetArticleByIdQuery>
    {
        private readonly IAppDbContext _context;

        public GetArticleByIdQueryValidator(IAppDbContext context)
        {
            _context = context;

            RuleFor(q => q.Id)
                .NotEmpty().WithMessage("Id is required.")
                .MustAsync(IsArticleWithIdExist).WithMessage("The article with specified id was not found.");
        }

        public async Task<bool> IsArticleWithIdExist(int id, CancellationToken cancellationToken)
        {
            var article = await _context.Articles.FirstOrDefaultAsync(a => a.Id == id, cancellationToken);

            return article != null;
        }
    }
}