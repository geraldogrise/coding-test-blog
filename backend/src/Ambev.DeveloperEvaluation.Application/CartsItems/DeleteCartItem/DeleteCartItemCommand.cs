using Ambev.DeveloperEvaluation.Application.CartsItems.GetCartItem;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.CartsItems.DeleteCartItem
{

    /// <summary>
    /// Command for deleting a cart item
    /// </summary>
    public record DeleteCartItemCommand : IRequest<GetCartItemResult>
    {
        /// <summary>
        /// The unique identifier of the cart item to delete
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Initializes a new instance of DeleteCartItemCommand
        /// </summary>
        /// <param name="id">The ID of the cart item to delete</param>
        public DeleteCartItemCommand(Guid id)
        {
            Id = id;
        }
    }
}
