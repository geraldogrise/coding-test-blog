using Ambev.DeveloperEvaluation.Application.Carts.GetAllCarts;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetAllCart
{
    /// <summary>
    /// Validator for GetAllCartsCommand
    /// </summary>
    public class GetAllCartsValidator : AbstractValidator<GetAllCartsCommand>
    {
        /// <summary>
        /// Initializes validation rules for GetAllCartsCommand
        /// </summary>
        public GetAllCartsValidator()
        {
            RuleFor(x => x.Page)
                .GreaterThan(0)
                .WithMessage("Page number must be greater than 0");

            RuleFor(x => x.Size)
                .GreaterThan(0)
                .WithMessage("Page size must be greater than 0");
        }
    }
}
