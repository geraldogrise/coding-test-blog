using Ambev.DeveloperEvaluation.Common.Result;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.CartsItems.GetAllCartItem
{
    /// <summary>
    /// Command for retrieving all cart items.
    /// </summary>
    public record GetAllCartItemsCommand(int Page, int Size, string Order)
        : IRequest<GetAllCartItemsResult>;
}