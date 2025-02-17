using AutoMapper;
using Blog.Application.ViewModels;
using Blog.Domain.Aggreagates.Posts;
using Blog.Domain.Aggreagates.Users;

namespace Blog.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<UserModel, User>();
            CreateMap<PostModel, Post>();
            CreateMap<LoginModel, Login>();
        }
    }
}
