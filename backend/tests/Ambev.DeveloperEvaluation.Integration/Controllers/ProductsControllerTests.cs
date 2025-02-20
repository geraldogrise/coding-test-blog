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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Ambev.DeveloperEvaluation.Integration.Controllers
{
    public class ProductsControllerTests : IClassFixture<AmbevWebApplicationFactory>
    {
        private readonly HttpClient _client;
        private readonly IMediator _mediatorMock;

        public ProductsControllerTests(AmbevWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
            _mediatorMock = factory.Services.GetRequiredService<IMediator>();
        }

        /// <summary>
        /// Testa a criação de um produto (POST /api/products)
        /// </summary>
        [Fact]
        public async Task CreateProduct_ShouldReturn201()
        {
            var request = new Product
            {
                Id = Guid.NewGuid(),
                Title = "Title",
                Price = 100,
                Description = "Description",
                Category = "Category",
                Image = "image",
                Rating = new RatttingDto
                {
                    Rate = 1,
                    Count = 10
                }
            };

            var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/api/products", content);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
        }

        /// <summary>
        /// Testa a busca de um produto pelo ID (GET /api/products/{id})
        /// </summary>
        [Fact]
        public async Task GetProductById_ShouldReturn200_WhenExists()
        {
            var productId = Guid.NewGuid();
            var response = await _client.GetAsync($"/api/products/{productId}");
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        /// <summary>
        /// Testa a busca de todos os produtos (GET /api/products)
        /// </summary>
        [Fact]
        public async Task GetAllProducts_ShouldReturn200()
        {
            var response = await _client.GetAsync("/api/products");
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        /// <summary>
        /// Testa a atualização de um produto (PUT /api/products/{id})
        /// </summary>
        [Fact]
        public async Task UpdateProduct_ShouldReturn200_WhenValid()
        {
            var productId = Guid.NewGuid();
            var request = new Product
            {
                Id = Guid.NewGuid(),
                Title = "Title",
                Price = 100,
                Description = "Description",
                Category = "Category",
                Image = "image",
                Rating = new RatttingDto
                {
                    Rate = 1,
                    Count = 10
                }
            };

            var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
            var response = await _client.PutAsync($"/api/products/{productId}", content);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        /// <summary>
        /// Testa a exclusão de um produto (DELETE /api/products/{id})
        /// </summary>
        [Fact]
        public async Task DeleteProduct_ShouldReturn200_WhenExists()
        {
            var productId = Guid.NewGuid();
            var response = await _client.DeleteAsync($"/api/products/{productId}");
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }
    }
}
