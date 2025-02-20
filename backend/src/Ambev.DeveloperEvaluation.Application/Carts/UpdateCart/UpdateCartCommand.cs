using MediatR;
using System;
using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.Application.Carts.UpdateCart
{
    // <summary>
    /// Command for updating an existing cart.
    /// </summary>
    public class UpdateCartCommand : IRequest<UpdateCartResult>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }

        public DateTime Date { get; set; }
        public List<UpdateCartItemDto> Items { get; set; } = new();
    }

    public class UpdateCartItemDto
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }

    }
}
