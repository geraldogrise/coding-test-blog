using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct
{
    public class UpdateProductValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Product ID is required");

            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("Product name is required")
                .MaximumLength(255)
                .WithMessage("Product name cannot exceed 255 characters");

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Product description is required");

            RuleFor(x => x.Price)
                .GreaterThan(0)
                .WithMessage("Price must be greater than 0");
        }
    }
}
