using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Products.GetAllProduct
{
    public class GetAllProductsValidator : AbstractValidator<GetAllProductsCommand>
    {
        public GetAllProductsValidator()
        {
            RuleFor(x => x.Page)
                .GreaterThan(0)
                .WithMessage("Page must be greater than 0.");

            RuleFor(x => x.Size)
                .GreaterThan(0)
                .WithMessage("Size must be greater than 0.");
        }
    }
}
