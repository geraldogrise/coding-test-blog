using Ambev.DeveloperEvaluation.Application.Users.GetAllUser;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.GetAllUser
{
    // <summary>
    /// Profile for mapping GetAllUsers feature requests to commands.
    /// </summary>
    public class GetAllUsersProfile : Profile
    {
        /// <summary>
        /// Initializes the mappings for GetAllUsers feature.
        /// </summary>
        public GetAllUsersProfile()
        {
            CreateMap<GetAllUsersRequest, GetAllUsersCommand>();
        }
    }
}
