using AutoMapper;
using Blog.Application.ViewModels;
using Blog.Domain.Aggreagates.Posts;
using Blog.Domain.Aggreagates.Users;

namespace Blog.Application.AutoMapper
{

    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<User, UserModel>();
            CreateMap<Post, PostModel>();
            CreateMap<Login, LoginModel>();
        }
    }
}
