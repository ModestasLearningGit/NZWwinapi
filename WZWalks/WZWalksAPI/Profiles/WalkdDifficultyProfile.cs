using AutoMapper;

namespace WZWalksAPI.Profiles
{
    public class WalkdDifficultyProfile : Profile
    {
        public WalkdDifficultyProfile()
        {
            CreateMap<Models.Domain.WalkDifficulty, Models.DTO.WalkDifficulty>()
                .ReverseMap();
        }
    }
}
