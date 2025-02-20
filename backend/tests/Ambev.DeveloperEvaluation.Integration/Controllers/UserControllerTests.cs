using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;
using MediatR;
using NSubstitute;
using Microsoft.Extensions.DependencyInjection;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Integration.Core;

namespace Ambev.DeveloperEvaluation.Integration.Controllers
{
    public class UserControllerTests : IClassFixture<AmbevWebApplicationFactory>
    {
        private readonly HttpClient _client;
        private readonly IMediator _mediatorMock;

        public UserControllerTests(AmbevWebApplicationFactory factory)
        {
            _client = factory.CreateClient();

            // Obter o mock do IMediator injetado na fábrica
            _mediatorMock = factory.Services.GetRequiredService<IMediator>();
        }

        /// <summary>
        /// Testa a criação de um usuário (POST /api/users)
        /// </summary>
        [Fact]
        public async Task CreateUser_ShouldReturn201()
        {
            // Arrange
            var request = new User
            {
                Username = "João Silva",
                Email = "joao@email.com",
                Phone = "(11) 98765-4321",
                Password = "Senha@123",
                Role = UserRole.Admin,
                Status = UserStatus.Active,
                Name = new Name { Firstname = "João", Lastname = "Silva" },
                Address = new Address
                {
                    City = "São Paulo",
                    Street = "Rua A",
                    Number = 123,
                    Zipcode = "01234-567",
                    Geolocation = new Geolocation { Lat = "-23.5505", Long = "-46.6333" }
                }
            };

            var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/users", content);

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
        }

        /// <summary>
        /// Testa a busca de um usuário pelo ID (GET /api/users/{id})
        /// </summary>
        [Fact]
        public async Task GetUserById_ShouldReturn200_WhenExists()
        {
            // Criar um ID válido para teste
            var userId = Guid.NewGuid();

            // Act
            var response = await _client.GetAsync($"/api/users/{userId}");

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        /// <summary>
        /// Testa a busca de todos os usuários (GET /api/users)
        /// </summary>
        [Fact]
        public async Task GetAllUsers_ShouldReturn200()
        {
            // Act
            var response = await _client.GetAsync("/api/users");

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        /// <summary>
        /// Testa a atualização de um usuário (PUT /api/users/{id})
        /// </summary>
        [Fact]
        public async Task UpdateUser_ShouldReturn200_WhenValid()
        {
            // Criar um ID válido para teste
            var userId = Guid.NewGuid();

            var request = new User
            {
                Username = "João Atualizado",
                Email = "joao.novo@email.com",
                Phone = "(11) 99999-9999",
                Password = "NovaSenha@456",
                Role = UserRole.Manager,
                Status = UserStatus.Inactive
            };

            var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PutAsync($"/api/users/{userId}", content);

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        /// <summary>
        /// Testa a exclusão de um usuário (DELETE /api/users/{id})
        /// </summary>
        [Fact]
        public async Task DeleteUser_ShouldReturn200_WhenExists()
        {
            // Criar um ID válido para teste
            var userId = Guid.NewGuid();

            // Act
            var response = await _client.DeleteAsync($"/api/users/{userId}");

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }
    }
}
