using System;
using System.Linq;
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
            Mapper.CreateMap<BodyPart, BodyPartDto>();
            Mapper.CreateMap<BodyPartDto, BodyPart>();
            Mapper.CreateMap<BodyRegionDto, BodyRegion>();
            Mapper.CreateMap<BodyRegion, BodyRegionDto>();
            Mapper.CreateMap<Symptom, SymptomDto>()
                  .ForMember(d => d.renderType, opt => opt.MapFrom(s => s.RenderType.ToString()));
            Mapper.CreateMap<SkeletonArea, SymptomaticRegionDto>()
                  .ForMember(d => d.orientation, opt => opt.MapFrom(s => s.Orientation.Value))
                  .ForMember(d => d.region, opt => opt.MapFrom(s => s.Region.Name))
                  .ForMember(d => d.side, opt => opt.MapFrom(s => s.Side.Value))
                  .ForMember(d => d.BodyParts, opt => opt.MapFrom(s => s.BodyPartMatrix));
            Mapper.CreateMap<BodyPartMatrixItem, SymptomaticBodyPartDto>()
                  .ForMember(d => d.bodyPartMatrixId,
                             opt => opt.MapFrom(s => s.Id))
                  .ForMember(d => d.potentialSymptoms,
                             opt => opt.MapFrom(s => s.SymptomMatrixItems))
                   .ForMember(d => d.id, opt => opt.MapFrom(s => s.BodyPart.Id))
                   .ForMember(d => d.commonName, opt => opt.MapFrom(s => s.BodyPart.CommonName))
                   .ForMember(d => d.scientificName, opt => opt.MapFrom(s => s.BodyPart.ScientificName));
            Mapper.CreateMap<SymptomMatrixItem, PotentialSymptomDto>()
                    .ForMember(d => d.symptomMatrixItemId, opt => opt.MapFrom(s => s.Id))
                   .ForMember(d => d.description, opt => opt.MapFrom(s => s.Symptom.Description))
                   .ForMember(d => d.name, opt => opt.MapFrom(s => s.Symptom.Name))
                   .ForMember(d => d.renderType, opt => opt.MapFrom(s => s.Symptom.RenderType))
                   .ForMember(d => d.id, opt => opt.MapFrom(s => s.Symptom.Id));
            Mapper.CreateMap<DifferentialDiagnosisDto, DifferentialDiagnosis>()
                   .ForMember(d => d.SumbittedOn, opt => opt.UseValue(DateTime.Now));
            Mapper.CreateMap<DifferentialDiagnosis, DifferentialDiagnosisDto>()
                   .ForMember(d => d.id, opt => opt.MapFrom(s => s.Id))
                   .ForMember(d => d.reviewedOn, opt => opt.MapFrom(s => s.ReviewedOn))
                   .ForMember(d => d.submittedOn, opt => opt.MapFrom(s => s.SumbittedOn));
            Mapper.CreateMap<DifferentialDiagnosis, DiagnosisReportDto>()
                   .ForMember(d => d.id, opt => opt.MapFrom(s => s.Id))
                   .ForMember(d => d.reviewedOn, opt => opt.MapFrom(s => s.ReviewedOn))
                   .ForMember(d => d.submittedOn, opt => opt.MapFrom(s => s.SumbittedOn));
            Mapper.CreateMap<SymptomDetail, PotentialSymptomDto>()
                  .ForMember(d => d.name, opt => opt.MapFrom(s => s.SymptomMatrixItem.Symptom.Name))
                  .ForMember(d => d.bodyPart, opt => opt.MapFrom(s => s.SymptomMatrixItem.BodyPartMatrixItem.BodyPart.CommonName));
            Mapper.CreateMap<PotentialSymptomDto, SymptomDetail>();
            Mapper.CreateMap<Workout, WorkoutDto>();
            Mapper.CreateMap<Exercise, ExerciseDto>()
                  .ForMember(d => d.videos, opt =>
                      {
                          opt.Condition(s => s.ExerciseVideoMatrixItems != null);
                          opt.MapFrom(s => s.ExerciseVideoMatrixItems.Select(p => p.Video));
                      })
                  .ForMember(d => d.equipment, opt =>
                      {
                          opt.Condition(s => s.ExerciseEquipmentMatrixItems != null);
                          opt.MapFrom(s => s.ExerciseEquipmentMatrixItems.Select(p => p.Equipment));
                      })
                  .ForMember(d => d.bodyRegions, opt =>
                      {
                          opt.Condition(s => s.ExerciseBodyRegionMatrixItems != null);
                          opt.MapFrom(s => s.ExerciseBodyRegionMatrixItems.Select(p => p.BodyRegion));
                      });
            Mapper.CreateMap<ExerciseDto, Exercise>()
                      .ForMember(d => d.ExerciseVideoMatrixItems, opt =>
                      {
                          opt.Condition(s => s.videos != null);
                          opt.MapFrom(s => s.videos);
                      })
                      .ForMember(d => d.ExerciseEquipmentMatrixItems, opt =>
                      {
                          opt.Condition(s => s.equipment != null);
                          opt.MapFrom(s => s.equipment);
                      })
                      .ForMember(d => d.ExerciseBodyRegionMatrixItems, opt =>
                      {
                          opt.Condition(s => s.bodyRegions != null);
                          opt.MapFrom(s => s.bodyRegions);
                      });
            Mapper.CreateMap<ExerciseVideoMatrixItem, VideoDto>()
                  .ForMember(d => d.id, opt => opt.MapFrom(s => s.VideoId));
            Mapper.CreateMap<ExerciseEquipmentMatrixItem, VideoDto>()
                  .ForMember(d => d.id, opt => opt.MapFrom(s => s.EquipmentId));
            Mapper.CreateMap<Video, VideoDto>();
            Mapper.CreateMap<VideoDto, ExerciseVideoMatrixItem>()
                  .ForMember(d => d.VideoId, opt => opt.MapFrom(s => s.id));
            Mapper.CreateMap<EquipmentDto, ExerciseEquipmentMatrixItem>()
                  .ForMember(d => d.EquipmentId, opt => opt.MapFrom(s => s.id));
            Mapper.CreateMap<BodyRegionDto, ExerciseBodyRegionMatrixItem>()
                  .ForMember(d => d.BodyRegionId, opt => opt.MapFrom(s => s.id));
            Mapper.CreateMap<VideoDto, Video>();
            Mapper.CreateMap<Equipment, EquipmentDto>();
            Mapper.CreateMap<EquipmentDto, Equipment>();
            Mapper.CreateMap<BodyRegion, BodyRegionDto>();
            Mapper.CreateMap<BodyRegionDto, BodyRegion>();
            Mapper.CreateMap<Cause, CauseDto>();
            Mapper.CreateMap<Sign, SignDto>();
            Mapper.CreateMap<Injury, InjuryDto>()
                   .ForMember(d => d.workouts, opt => opt.MapFrom(s => s.InjuryWorkoutMatrixItems.Select(p => p.Workout)))
                   .ForMember(d => d.causes, opt => opt.MapFrom(s => s.InjuryCauseMatrixItems.Select(p => p.Cause)))
                   .ForMember(d => d.signs, opt => opt.MapFrom(s => s.InjurySignMatrixItems.Select(p => p.Sign)));
            Mapper.CreateMap<Injury, PotentialInjuryDto>()
                   .ForMember(d => d.workouts, opt => opt.MapFrom(s => s.InjuryWorkoutMatrixItems.Select(p => p.Workout)))
                   .ForMember(d => d.causes, opt => opt.MapFrom(s => s.InjuryCauseMatrixItems.Select(p => p.Cause)))
                   .ForMember(d => d.signs, opt => opt.MapFrom(s => s.InjurySignMatrixItems.Select(p => p.Sign)));

        }
    }
}
