using Ambev.DeveloperEvaluation.WebApi.Features.CartItems.CreateCartItem;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart
{

    /// <summary>
    /// Validator for CreateCartRequest that defines validation rules for cart creation.
    /// </summary>
    public class CreateCartRequestValidator : AbstractValidator<CreateCartRequest>
    {
        /// <summary>
        /// Initializes a new instance of the CreateCartRequestValidator with defined validation rules.
        /// </summary>
        public CreateCartRequestValidator()
        {
            RuleFor(cart => cart.UserId)
                .NotEmpty().WithMessage("User ID is required.");

            RuleFor(cart => cart.Items)
                .NotEmpty().WithMessage("Cart must contain at least one item.");

            RuleForEach(cart => cart.Items)
                .SetValidator(new CreateCartItemRequestValidator());
        }
    }

    /// <summary>
    /// Validator for CreateCartItemRequest that defines validation rules for cart items.
    /// </summary>
    public class CreateCartItemRequestValidator : AbstractValidator<CreateCartItemRequest>
    {
        /// <summary>
        /// Initializes a new instance of the CreateCartItemRequestValidator with defined validation rules.
        /// </summary>
        public CreateCartItemRequestValidator()
        {
            RuleFor(item => item.ProductId)
                .NotEmpty().WithMessage("Product ID is required.");

            RuleFor(item => item.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be greater than zero.");
        }
    }
}
