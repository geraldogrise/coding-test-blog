using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct
{
    /// <summary>
    /// Validator for CreateProductRequest that defines validation rules for product creation.
    /// </summary>
    public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
    {
        /// <summary>
        /// Initializes a new instance of the CreateProductRequestValidator with defined validation rules.
        /// </summary>
        public CreateProductRequestValidator()
        {
            RuleFor(product => product.Title)
                .NotEmpty().WithMessage("Product name is required.")
                .Length(3, 100).WithMessage("Product name must be between 3 and 100 characters.");

            RuleFor(product => product.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(500).WithMessage("Description must be at most 500 characters.");

            RuleFor(product => product.Price)
                .GreaterThan(0).WithMessage("Price must be greater than zero.");

           
        }
    }
}
