using System;
using AutoMapper;

using SportsWebPt.Platform.ServiceModels;
using SportsWebPt.Platform.Web.Core;

namespace SportsWebPt.Platform.Web.Services
{
    public static class ServicesContentMaps
    {
        public static void CreateContentMaps()
        {
            Mapper.CreateMap<User, UserDto>();
            Mapper.CreateMap<UserDto, User>();
            Mapper.CreateMap<SkeletonAreaDto, SkeletonArea>();
            Mapper.CreateMap<BodyPartDto, BodyPart>();
            Mapper.CreateMap<SymptomDto, Symptom>();
            Mapper.CreateMap<SymptomaticRegionDto, SymptomaticRegion>()
                  .ForMember(d => d.orientation, opt => opt.MapFrom(s => s.orientation))
                  .ForMember(d => d.region, opt => opt.MapFrom(s => s.region))
                  .ForMember(d => d.side, opt => opt.MapFrom(s => s.side))
                  .ForMember(d => d.bodyParts, opt => opt.MapFrom(s => s.BodyParts));
            Mapper.CreateMap<SymptomaticBodyPartDto, SymptomaticBodyPart>()
                  .ForMember(d => d.potentialSymptoms, opt => opt.MapFrom(s => s.potentialSymptoms));
            Mapper.CreateMap<PotentialSymptomDto, PotentialSymptom>();
            Mapper.CreateMap<PotentialSymptom, PotentialSymptomDto>();
            Mapper.CreateMap<DifferentialDiagnosis, DifferentialDiagnosisDto>()
                  .ForMember(d => d.submittedFor, opt => opt.MapFrom(s => s.submittedFor));
            Mapper.CreateMap<DiagnosisReportDto, DiagnosisReport>();
            Mapper.CreateMap<InjuryDto, Injury>()
                  .ForMember(d => d.workouts, opt => opt.MapFrom(s =>s.workouts))
                  .ForMember(d => d.causes, opt => opt.MapFrom(s =>s.causes))                  
                  .ForMember(d => d.signs, opt => opt.MapFrom(s =>s.signs));
            Mapper.CreateMap<PotentialInjuryDto, PotentialInjury>()
                  .ForMember(d => d.workouts, opt => opt.MapFrom(s => s.workouts))
                  .ForMember(d => d.causes, opt => opt.MapFrom(s => s.causes))
                  .ForMember(d => d.signs, opt => opt.MapFrom(s => s.signs))
                  .ForMember(d => d.givenSymptoms, opt => opt.MapFrom(s => s.givenSymptoms));
            Mapper.CreateMap<WorkoutDto, Workout>();
            Mapper.CreateMap<ExerciseDto, Exercise>();
            Mapper.CreateMap<Exercise, ExerciseDto>();
            Mapper.CreateMap<EquipmentDto, Equipment>();
            Mapper.CreateMap<Equipment, EquipmentDto>();
            Mapper.CreateMap<VideoDto, Video>();
            Mapper.CreateMap<Video, VideoDto>();
            Mapper.CreateMap<CauseDto, Cause>();
            Mapper.CreateMap<SignDto, Sign>();
            Mapper.CreateMap<BodyRegion, BodyRegionDto>();
            Mapper.CreateMap<BodyRegionDto, BodyRegion>();



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
