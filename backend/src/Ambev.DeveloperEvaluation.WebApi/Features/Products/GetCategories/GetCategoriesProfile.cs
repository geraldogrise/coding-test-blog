using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetCategories
{
    /// <summary>
    /// Profile for mapping GetCategories feature responses.
    /// </summary>
    public class GetCategoriesProfile : Profile
    {
        /// <summary>
        /// Initializes the mappings for GetCategories feature.
        /// </summary>
        public GetCategoriesProfile()
        {
            CreateMap<List<string>, GetCategoriesResponse>()
                .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src));
        }
    }
}
