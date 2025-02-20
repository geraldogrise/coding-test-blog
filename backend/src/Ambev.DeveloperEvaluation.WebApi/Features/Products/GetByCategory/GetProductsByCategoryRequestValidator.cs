using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetByCategory
{
    /// <summary>
    /// Validator for GetProductsByCategoryRequest.
    /// </summary>
    public class GetProductsByCategoryRequestValidator : AbstractValidator<GetProductsByCategoryRequest>
    {
        /// <summary>
        /// Initializes validation rules for GetProductsByCategoryRequest.
        /// </summary>
        public GetProductsByCategoryRequestValidator()
        {
            RuleFor(x => x.Category)
                .NotEmpty()
                .WithMessage("Category is required.");

            RuleFor(x => x.Page)
                .GreaterThan(0)
                .WithMessage("Page number must be greater than zero.");

            RuleFor(x => x.Size)
                .GreaterThan(0)
                .WithMessage("Size must be greater than zero.");
        }
    }
}
