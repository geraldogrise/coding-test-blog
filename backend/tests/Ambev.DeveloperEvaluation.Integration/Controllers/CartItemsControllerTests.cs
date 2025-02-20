using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Integration.Core;
using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace Ambev.DeveloperEvaluation.Integration.Controllers
{
    public class CartItemsControllerTests : IClassFixture<AmbevWebApplicationFactory>
    {
        private readonly HttpClient _client;
        private readonly IMediator _mediatorMock;

        public CartItemsControllerTests(AmbevWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
            _mediatorMock = factory.Services.GetRequiredService<IMediator>();
        }

        [Fact]
        public async Task CreateCartItem_ShouldReturn201()
        {
            var request = new CartItem
            {
                Id = Guid.NewGuid(),
                CartId = Guid.NewGuid(),
                ProductId = Guid.NewGuid(),
                Quantity = 2
            };

            var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/api/cartitems", content);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
        }

        [Fact]
        public async Task GetCartItemById_ShouldReturn200_WhenExists()
        {
            var cartItemId = Guid.NewGuid();
            var response = await _client.GetAsync($"/api/cartitems/{cartItemId}");
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetAllCartItems_ShouldReturn200()
        {
            var response = await _client.GetAsync("/api/cartitems");
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        [Fact]
        public async Task UpdateCartItem_ShouldReturn200_WhenValid()
        {
            var cartItemId = Guid.NewGuid();
            var request = new CartItem
            {
                Id = cartItemId,
                CartId = Guid.NewGuid(),
                ProductId = Guid.NewGuid(),
                Quantity = 5
            };

            var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
            var response = await _client.PutAsync($"/api/cartitems/{cartItemId}", content);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        [Fact]
        public async Task DeleteCartItem_ShouldReturn200_WhenExists()
        {
            var cartItemId = Guid.NewGuid();
            var response = await _client.DeleteAsync($"/api/cartitems/{cartItemId}");
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }
    }
}
