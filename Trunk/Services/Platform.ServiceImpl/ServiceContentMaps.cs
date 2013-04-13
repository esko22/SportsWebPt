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
            Mapper.CreateMap<SkeletonHotspot, SkeletorHotspotDto>()
                  .ForMember(d => d.Orientation, opt => opt.MapFrom(s => s.Orientation.Value))
                  .ForMember(d => d.Region, opt => opt.MapFrom(s => s.Region.Name))
                  .ForMember(d => d.Side, opt => opt.MapFrom(s => s.Side.Value));
        }
    }
}
