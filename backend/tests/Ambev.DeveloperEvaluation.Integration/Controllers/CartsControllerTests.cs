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
    public class CartsControllerTests : IClassFixture<AmbevWebApplicationFactory>
    {
        private readonly HttpClient _client;
        private readonly IMediator _mediatorMock;

        public CartsControllerTests(AmbevWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
            _mediatorMock = factory.Services.GetRequiredService<IMediator>();
        }

        /// <summary>
        /// Testa a criação de um carrinho (POST /api/carts)
        /// </summary>
        [Fact]
        public async Task CreateCart_ShouldReturn201()
        {
            var request = new Cart
            {
                Id = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                Items = new List<CartItem>
                {
                    new CartItem { ProductId = Guid.NewGuid(), Quantity = 2 },
                    new CartItem { ProductId = Guid.NewGuid(), Quantity = 2 },
                     new CartItem { ProductId = Guid.NewGuid(), Quantity = 2 }
                },
             };

            var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/api/carts", content);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
        }

        /// <summary>
        /// Testa a busca de um carrinho pelo ID (GET /api/carts/{id})
        /// </summary>
        [Fact]
        public async Task GetCartById_ShouldReturn200_WhenExists()
        {
            var cartId = Guid.NewGuid();
            var response = await _client.GetAsync($"/api/carts/{cartId}");
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        /// <summary>
        /// Testa a busca de todos os carrinhos (GET /api/carts)
        /// </summary>
        [Fact]
        public async Task GetAllCarts_ShouldReturn200()
        {
            var response = await _client.GetAsync("/api/carts");
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        /// <summary>
        /// Testa a atualização de um carrinho (PUT /api/carts/{id})
        /// </summary>
        [Fact]
        public async Task UpdateCart_ShouldReturn200_WhenValid()
        {
            var cartId = Guid.NewGuid();
            var request = new Cart
            {
                Id = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                Items = new List<CartItem>
                {
                    new CartItem { ProductId = Guid.NewGuid(), Quantity = 2 },
                    new CartItem { ProductId = Guid.NewGuid(), Quantity = 2 },
                     new CartItem { ProductId = Guid.NewGuid(), Quantity = 2 }
                },
            };
            var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
            var response = await _client.PutAsync($"/api/carts/{cartId}", content);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        /// <summary>
        /// Testa a exclusão de um carrinho (DELETE /api/carts/{id})
        /// </summary>
        [Fact]
        public async Task DeleteCart_ShouldReturn200_WhenExists()
        {
            var cartId = Guid.NewGuid();
            var response = await _client.DeleteAsync($"/api/carts/{cartId}");
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }
    }
}
