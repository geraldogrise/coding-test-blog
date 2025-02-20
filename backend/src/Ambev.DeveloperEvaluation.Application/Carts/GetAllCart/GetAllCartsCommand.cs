using Ambev.DeveloperEvaluation.Application.Carts.GetAllCart;
using MediatR;
using System;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetAllCarts
{
    /// <summary>
    /// Command for retrieving all carts with pagination and ordering
    /// </summary>
    public record GetAllCartsCommand(int Page, int Size, string Order) : IRequest<GetAllCartsResult>;
}