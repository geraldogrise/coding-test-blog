using Ambev.DeveloperEvaluation.Application.CartsItems.CreateCartItem;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Carts.CreateCart
{
    /// <summary>
    /// Command for creating a new cart.
    /// </summary>
    public class CreateCartCommand : IRequest<CreateCartResult>
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the user ID associated with the cart.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// The list of items in the cart.
        /// </summary>
        public List<CreateCartItemCommand> Items { get; set; } = new();
    }
}
