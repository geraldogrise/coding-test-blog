using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Common.Result;
using Ambev.DeveloperEvaluation.Application.Users.DTOS;

namespace Ambev.DeveloperEvaluation.Application.Users.GetAllUser
{
    /// <summary>
    /// Profile for mapping between User entity and GetAllUsersResult
    /// </summary>
    public class GetAllUsersProfile : Profile
    {
        /// <summary>
        /// Initializes the mappings for GetAllUsers operation
        /// </summary>
        public GetAllUsersProfile()
        {
            CreateMap<PagedResult<User>, GetAllUsersResult>();
            CreateMap<User, UserDto>();
        }
    }
}
