using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Products.GetByCategory
{
    public class GetByCategoryValidator : AbstractValidator<GetByCategoryCommand>
    {
        public GetByCategoryValidator()
        {
            RuleFor(x => x.Category).NotEmpty().WithMessage("Category is required");
            RuleFor(x => x.Page).GreaterThan(0).WithMessage("Page number must be greater than 0");
            RuleFor(x => x.Size).GreaterThan(0).WithMessage("Size must be greater than 0");
        }
    }
}
