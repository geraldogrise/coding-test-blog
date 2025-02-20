using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.CartItems.GetAllCartItem
{
    /// <summary>
    /// Validator for GetAllCartItemsRequest.
    /// </summary>
    public class GetAllCartItemsRequestValidator : AbstractValidator<GetAllCartItemsRequest>
    {
        /// <summary>
        /// Initializes validation rules for GetAllCartItemsRequest.
        /// </summary>
        public GetAllCartItemsRequestValidator()
        {
            RuleFor(x => x.Page)
                .GreaterThan(0)
                .WithMessage("Page number must be greater than zero.");

            RuleFor(x => x.Size)
                .InclusiveBetween(1, 100)
                .WithMessage("Size must be between 1 and 100.");
        }
    }
}
