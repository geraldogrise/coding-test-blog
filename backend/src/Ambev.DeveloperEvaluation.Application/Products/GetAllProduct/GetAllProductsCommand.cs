using Ambev.DeveloperEvaluation.Application.Products.GetProduct;
using Ambev.DeveloperEvaluation.Common.Result;
using MediatR;



namespace Ambev.DeveloperEvaluation.Application.Products.GetAllProduct
{
    public record GetAllProductsCommand(int Page, int Size, string Order) : IRequest<GetAllProductResult>;
}
