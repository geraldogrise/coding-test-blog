using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetCategories
{
    /// <summary>
    /// Validator for GetCategoriesRequest.
    /// </summary>
    public class GetCategoriesRequestValidator : AbstractValidator<GetCategoriesRequest>
    {
        /// <summary>
        /// Initializes validation rules for GetCategoriesRequest.
        /// </summary>
        public GetCategoriesRequestValidator()
        {
            // Nenhuma validação necessária, mas o arquivo está aqui para manter o padrão do projeto.
        }
    }
}
