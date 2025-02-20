using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct
     {/// <summary>
     /// Validator for CreateProductCommand
     /// </summary>
    public class CreateProductValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductValidator()
        {
            RuleFor(x => x.Title)
                           .NotEmpty().WithMessage("Title is required.")
                           .Length(3, 100).WithMessage("Title must be between 3 and 100 characters.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.")
                .Length(5, 500).WithMessage("Description must be between 5 and 500 characters.");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Price must be greater than zero.");

            RuleFor(x => x.Category)
                .NotEmpty().WithMessage("Category is required.");

            RuleFor(x => x.Image)
                .NotEmpty().WithMessage("Image URL is required.")
                .Matches(@"^(https?:\/\/.*\.(?:png|jpg|jpeg|gif|svg))$").WithMessage("Invalid image URL format.");

            RuleFor(x => x.Rating.Rate)
                .InclusiveBetween(0, 5).WithMessage("Rating must be between 0 and 5.");

            RuleFor(x => x.Rating.Count)
                .GreaterThanOrEqualTo(0).WithMessage("Rating count must be zero or greater.");
        }
    }
}
