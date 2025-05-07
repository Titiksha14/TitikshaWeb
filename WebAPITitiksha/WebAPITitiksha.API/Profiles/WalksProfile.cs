using System.Runtime.InteropServices;
using AutoMapper;

namespace WebAPITitiksha.API.Profiles
{
    public class WalksProfile:Profile
    {
        public WalksProfile()
        {
            CreateMap<Models.Domain.Walk, Models.DTO.WalkDto>()
             .ReverseMap();

            CreateMap<Models.Domain.WalkDifficulty,Models.DTO.WalkDifficultyDto>()
                .ReverseMap();

        }

    }
}
