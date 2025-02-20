using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.CartsItems.GetAllCartItem
{
    /// <summary>
    /// Validator for GetAllCartItemsCommand.
    /// </summary>
    public class GetAllCartItemsValidator : AbstractValidator<GetAllCartItemsCommand>
    {
        public GetAllCartItemsValidator()
        {
        }
    }
}
