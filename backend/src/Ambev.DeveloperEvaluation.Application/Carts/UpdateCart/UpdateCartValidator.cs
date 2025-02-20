using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Carts.UpdateCart
{
    /// <summary>
    /// Validator for UpdateCartCommand.
    /// </summary>
    public class UpdateCartValidator : AbstractValidator<UpdateCartCommand>
    {
        public UpdateCartValidator()
        {
            RuleFor(cart => cart.Id).NotEmpty().WithMessage("Cart ID is required.");
            RuleFor(cart => cart.UserId).NotEmpty().WithMessage("User ID is required.");
            RuleFor(cart => cart.Items).NotNull().WithMessage("Items list cannot be null.");
            RuleForEach(cart => cart.Items).ChildRules(items =>
            {
                items.RuleFor(i => i.ProductId).NotEmpty().WithMessage("Product ID is required.");
                items.RuleFor(i => i.Quantity).GreaterThan(0).WithMessage("Quantity must be greater than zero.");
            });
        }
    }
}
