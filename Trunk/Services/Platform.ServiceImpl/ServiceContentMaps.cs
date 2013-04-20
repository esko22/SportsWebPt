using AutoMapper;

using SportsWebPt.Platform.Core.Models;
using SportsWebPt.Platform.ServiceModels;

namespace SportsWebPt.Platform.ServiceImpl
{
    public static class ServiceContentMaps
    {
        public static void CreateContentMaps()
        {
            Mapper.CreateMap<User, UserDto>();
            Mapper.CreateMap<UserDto, User>();
            Mapper.CreateMap<SkeletonArea, SkeletonAreaDto>()
                  .ForMember(d => d.orientation, opt => opt.MapFrom(s => s.Orientation.Value))
                  .ForMember(d => d.region, opt => opt.MapFrom(s => s.Region.Name))
                  .ForMember(d => d.side, opt => opt.MapFrom(s => s.Side.Value));
            Mapper.CreateMap<AreaComponent, AreaComponentDto>();

        }
    }
}
