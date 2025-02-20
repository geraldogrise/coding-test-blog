using System;
using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.Application.Carts.UpdateCart
{
    /// <summary>
    /// Represents the response returned after updating a cart.
    /// </summary>
    public class UpdateCartResult
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public List<UpdateCartItemDto> Items { get; set; } = new();
    }
}
