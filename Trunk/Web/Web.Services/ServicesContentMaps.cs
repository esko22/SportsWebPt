﻿using System;
using AutoMapper;

using SportsWebPt.Common.Utilities;
using SportsWebPt.Platform.ServiceContracts.Models;
using SportsWebPt.Platform.Web.Core;

namespace SportsWebPt.Platform.Web.Services
{
    public class ServicesContentMaps : ICreateContentMaps
    {
        public void CreateContentMaps()
        {
            Mapper.CreateMap<User, UserDto>();
            Mapper.CreateMap<UserDto, User>();


            //Mapper.CreateMap<ContentRequestItemEntity, UserRegionWorkRequestItem>()
            //   .ForMember(dest => dest.ContentRequestItemId, source => source.MapFrom(s => s.content_request_item_id))
            //   .ForMember(dest => dest.ProjectId, source => source.MapFrom(s => s.ContentRequest.project_id))
            //   .ForMember(dest => dest.Targets,
            //              source =>
            //              source.MapFrom(
            //                  s =>
            //                  {
            //                      var targets = new List<Target>();
            //                      s.ContentRequestItemTargetDetails.ForEach(
            //                          p => targets.Add(
            //                              new Target(p.target_detail_id, p.content_request_item_target_detail_id,
            //                                  p.TargetDetail.starting_coordinate, p.TargetDetail.ending_coordinate ?? 0, p.is_selected)));
            //                      return targets;
            //                  }));

        }
    }
}