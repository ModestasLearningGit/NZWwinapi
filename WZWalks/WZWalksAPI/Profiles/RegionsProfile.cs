using AutoMapper;

namespace WZWalksAPI.Profiles
{
    public class RegionsProfile : Profile
    {
        public RegionsProfile()
        {
            CreateMap<Models.Domain.Region, Models.DTO.Region>();
            // to map dest to src (Map properties)
                /*.ForMember(dest => dest.Id, options => options.MapFrom(src => src.Id));*/
        }
    }
}
