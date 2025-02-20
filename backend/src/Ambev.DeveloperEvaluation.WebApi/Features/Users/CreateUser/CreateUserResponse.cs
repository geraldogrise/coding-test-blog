using Ambev.DeveloperEvaluation.Application.Users.DTOS;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.WebApi.Features.Users.Requests;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.CreateUser;

/// <summary>
/// API response model for CreateUser operation
/// </summary>
public class CreateUserResponse
{
    /// <summary>
    /// The unique identifier of the created user
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The user's full name
    /// </summary>
    public string username { get; set; } = string.Empty;

    /// <summary>
    /// The user's email address
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// The user's phone number
    /// </summary>
    public string Phone { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the role as a string.
    /// </summary>
    public string Role
    {
        get => UserRole.ToString();
        set => UserRole = Enum.Parse<UserRole>(value, true);
    }

    /// <summary>
    /// The user's role in the system
    /// </summary>
    private UserRole UserRole { get; set; }

    /// <summary>
    /// Gets or sets the status of the user.
    /// </summary>
    public string Status
    {
        get => UserStatus.ToString();
        set => UserStatus = Enum.Parse<UserStatus>(value, true);
    }

    /// <summary>
    /// The current status of the user
    /// </summary>
    private UserStatus UserStatus { get; set; }

    /// <summary>
    /// Gets or sets the name details of the user.
    /// </summary>
    public NameRequest Name { get; set; } = new();

    /// <summary>
    /// Gets or sets the address details of the user.
    /// </summary>
    public AddressRequest Address { get; set; } = new();


}
