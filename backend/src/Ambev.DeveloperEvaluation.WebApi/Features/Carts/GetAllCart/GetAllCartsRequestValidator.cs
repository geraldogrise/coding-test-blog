using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetAllCart
{
    /// <summary>
    /// Validator for GetAllCartsRequest
    /// </summary>
    public class GetAllCartsRequestValidator : AbstractValidator<GetAllCartsRequest>
    {
        /// <summary>
        /// Initializes validation rules for GetAllCartsRequest
        /// </summary>
        public GetAllCartsRequestValidator()
        {
            RuleFor(x => x.Page)
                .GreaterThan(0)
                .WithMessage("Page number must be greater than zero.");

            RuleFor(x => x.Size)
                .GreaterThan(0)
                .WithMessage("Page size must be greater than zero.");
        }
    }
}