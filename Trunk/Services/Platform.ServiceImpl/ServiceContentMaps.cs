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
                  .ForMember(d => d.Orientation, opt => opt.MapFrom(s => s.Orientation.Value))
                  .ForMember(d => d.Region, opt => opt.MapFrom(s => s.Region.Name))
                  .ForMember(d => d.Side, opt => opt.MapFrom(s => s.Side.Value));
            Mapper.CreateMap<BodyPart, BodyPartDto>()
                  .ForMember(d => d.PrimaryAreas, opt =>
                      {
                          opt.Condition(s => s.BodyPartMatrix != null);
                          opt.MapFrom(s => s.BodyPartMatrix.Where(p => !p.IsSecondary));
                      })
                  .ForMember(d => d.SecondaryAreas, opt =>
                      {
                          opt.Condition(s => s.BodyPartMatrix != null);
                          opt.MapFrom(s => s.BodyPartMatrix.Where(p => p.IsSecondary));
                      });
            Mapper.CreateMap<BodyPartMatrixItem, SkeletonAreaDto>()
                   .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SkeletonArea.Id))
                   .ForMember(d => d.Orientation, opt => opt.MapFrom(s => s.SkeletonArea.Orientation.Value))
                   .ForMember(d => d.Region, opt => opt.MapFrom(s => s.SkeletonArea.Region.Name))
                   .ForMember(d => d.Side, opt => opt.MapFrom(s => s.SkeletonArea.Side.Value));
            Mapper.CreateMap<BodyPartDto, BodyPart>()
                  .ForMember(d => d.BodyPartMatrix, opt =>
                      {
                          opt.Condition(s => s.PrimaryAreas != null);
                          opt.MapFrom(s => s.PrimaryAreas);
                      })
                  .ForMember(d => d.SecondaryBodyPartMatrix, opt =>
                      {
                          opt.Condition(s => s.SecondaryAreas != null);
                          opt.MapFrom(s => s.SecondaryAreas);
                      });
            Mapper.CreateMap<SkeletonAreaDto, BodyPartMatrixItem>()
                  .ForMember(d => d.SkeletonAreaId, opt => opt.MapFrom(s => s.Id));
            Mapper.CreateMap<BodyRegionDto, BodyRegion>();
            Mapper.CreateMap<BodyRegion, BodyRegionDto>();
            Mapper.CreateMap<Symptom, SymptomDto>()
                  .ForMember(d => d.renderType, opt => opt.MapFrom(s => s.RenderType.RenderType))
                  .ForMember(d => d.renderOptions, opt => opt.MapFrom(s => s.RenderOptions))
                  .ForMember(d => d.renderTemplate, opt => opt.MapFrom(s => s.RenderType.DefaultTemplate));
            Mapper.CreateMap<SkeletonArea, SymptomaticRegionDto>()
                  .ForMember(d => d.Orientation, opt => opt.MapFrom(s => s.Orientation.Value))
                  .ForMember(d => d.Region, opt => opt.MapFrom(s => s.Region.Name))
                  .ForMember(d => d.Side, opt => opt.MapFrom(s => s.Side.Value))
                  .ForMember(d => d.BodyParts, opt => opt.MapFrom(s => s.BodyPartMatrix));
            Mapper.CreateMap<BodyPartMatrixItem, SymptomaticBodyPartDto>()
                  .ForMember(d => d.BodyPartMatrixId,
                             opt => opt.MapFrom(s => s.Id))
                  .ForMember(d => d.PotentialSymptoms,
                             opt => opt.MapFrom(s => s.SymptomMatrixItems))
                   .ForMember(d => d.Id, opt => opt.MapFrom(s => s.BodyPart.Id))
                   .ForMember(d => d.CommonName, opt => opt.MapFrom(s => s.BodyPart.CommonName))
                   .ForMember(d => d.IsSecondary, opt => opt.MapFrom(s => s.IsSecondary))
                   .ForMember(d => d.ScientificName, opt => opt.MapFrom(s => s.BodyPart.ScientificName));
            Mapper.CreateMap<SymptomMatrixItem, PotentialSymptomDto>()
                    .ForMember(d => d.symptomMatrixItemId, opt => opt.MapFrom(s => s.Id))
                   .ForMember(d => d.description, opt => opt.MapFrom(s => s.Symptom.Description))
                   .ForMember(d => d.name, opt => opt.MapFrom(s => s.Symptom.Name))
                  .ForMember(d => d.renderType, opt => opt.MapFrom(s => s.Symptom.RenderType.RenderType))
                  .ForMember(d => d.renderOptions, opt => opt.MapFrom(s => s.Symptom.RenderOptions))
                  .ForMember(d => d.renderTemplate, opt => opt.MapFrom(s => s.Symptom.RenderType.DefaultTemplate))
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
            Mapper.CreateMap<Plan, PlanDto>()
                      .ForMember(d => d.Exercises, opt =>
                      {
                          opt.Condition(s => s.PlanExerciseMatrixItems != null);
                          opt.MapFrom(s => s.PlanExerciseMatrixItems);
                      })
                      .ForMember(d => d.BodyRegions, opt =>
                      {
                          opt.Condition(s => s.PlanBodyRegionMatrixItems != null);
                          opt.MapFrom(s => s.PlanBodyRegionMatrixItems.Select(p => p.BodyRegion));
                      });
            Mapper.CreateMap<PlanDto, Plan>()
                  .ForMember(d => d.PlanExerciseMatrixItems, opt =>
                      {
                          opt.Condition(s => s.Exercises != null);
                          opt.MapFrom(s => s.Exercises);
                      })
                  .ForMember(d => d.PlanBodyRegionMatrixItems, opt =>
                      {
                          opt.Condition(s => s.BodyRegions != null);
                          opt.MapFrom(s => s.BodyRegions);
                      });
            Mapper.CreateMap<BodyRegionDto, PlanBodyRegionMatrixItem>()
                  .ForMember(d => d.BodyRegionId, opt => opt.MapFrom(s => s.id));
            Mapper.CreateMap<PlanExerciseDto, PlanExerciseMatrixItem>()
                  .ForMember(d => d.ExerciseId, opt => opt.MapFrom(s => s.ExerciseId))
                  .ForMember(d => d.Repititions, opt => opt.MapFrom(s => s.Repititions))
                  .ForMember(d => d.Sets, opt => opt.MapFrom(s => s.Sets))
                  .ForMember(d => d.PerWeek, opt => opt.MapFrom(s => s.PerWeek))
                  .ForMember(d => d.PerDay, opt => opt.MapFrom(s => s.PerDay))
                  .ForMember(d => d.HoldFor, opt => opt.MapFrom(s => s.HoldFor));
            Mapper.CreateMap<Exercise, ExerciseDto>()
                  .ForMember(d => d.Repititions, opt => opt.MapFrom(s => s.Repititions))
                  .ForMember(d => d.Sets, opt => opt.MapFrom(s => s.Sets))
                  .ForMember(d => d.PerDay, opt => opt.MapFrom(s => s.PerDay))
                  .ForMember(d => d.PerWeek, opt => opt.MapFrom(s => s.PerWeek))
                  .ForMember(d => d.HoldFor, opt => opt.MapFrom(s => s.HoldFor))
                  .ForMember(d => d.Videos, opt =>
                      {
                          opt.Condition(s => s.ExerciseVideoMatrixItems != null);
                          opt.MapFrom(s => s.ExerciseVideoMatrixItems.Select(p => p.Video));
                      })
                  .ForMember(d => d.Equipment, opt =>
                      {
                          opt.Condition(s => s.ExerciseEquipmentMatrixItems != null);
                          opt.MapFrom(s => s.ExerciseEquipmentMatrixItems.Select(p => p.Equipment));
                      })
                  .ForMember(d => d.BodyRegions, opt =>
                      {
                          opt.Condition(s => s.ExerciseBodyRegionMatrixItems != null);
                          opt.MapFrom(s => s.ExerciseBodyRegionMatrixItems.Select(p => p.BodyRegion));
                      })
                  .ForMember(d => d.BodyParts, opt =>
                      {
                          opt.Condition(s => s.ExerciseBodyPartMatrixItems != null);
                          opt.MapFrom(s => s.ExerciseBodyPartMatrixItems.Select(p => p.BodyPart));
                      });
            Mapper.CreateMap<PlanExerciseMatrixItem, PlanExerciseDto>()
                  .ForMember(d => d.Videos,
                             opt => opt.MapFrom(s => s.Exercise.ExerciseVideoMatrixItems.Select(p => p.Video)))
                  .ForMember(d => d.Equipment,
                             opt => opt.MapFrom(s => s.Exercise.ExerciseEquipmentMatrixItems.Select(p => p.Equipment)))
                  .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Exercise.Name))
                  .ForMember(d => d.MedicalName, opt => opt.MapFrom(s => s.Exercise.MedicalName))
                  .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                  .ForMember(d => d.Difficulty, opt => opt.MapFrom(s => s.Exercise.Difficulty))
                  .ForMember(d => d.Duration, opt => opt.MapFrom(s => s.Exercise.Duration))
                  .ForMember(d => d.Repititions, opt => opt.MapFrom(s => s.Repititions))
                  .ForMember(d => d.Sets, opt => opt.MapFrom(s => s.Sets))
                  .ForMember(d => d.PerDay, opt => opt.MapFrom(s => s.PerDay))
                  .ForMember(d => d.PerWeek, opt => opt.MapFrom(s => s.PerWeek))
                  .ForMember(d => d.HoldFor, opt => opt.MapFrom(s => s.HoldFor))
                  .ForMember(d => d.ExerciseId, opt => opt.MapFrom(s => s.ExerciseId));
            Mapper.CreateMap<ExerciseDto, Exercise>()
                  .ForMember(d => d.Repititions, opt => opt.MapFrom(s => s.Repititions))
                  .ForMember(d => d.Sets, opt => opt.MapFrom(s => s.Sets))
                  .ForMember(d => d.PerDay, opt => opt.MapFrom(s => s.PerDay))
                  .ForMember(d => d.PerWeek, opt => opt.MapFrom(s => s.PerWeek))
                  .ForMember(d => d.HoldFor, opt => opt.MapFrom(s => s.HoldFor))
                  .ForMember(d => d.ExerciseVideoMatrixItems, opt =>
                      {
                          opt.Condition(s => s.Videos != null);
                          opt.MapFrom(s => s.Videos);
                      })
                  .ForMember(d => d.ExerciseEquipmentMatrixItems, opt =>
                      {
                          opt.Condition(s => s.Equipment != null);
                          opt.MapFrom(s => s.Equipment);
                      })
                  .ForMember(d => d.ExerciseBodyRegionMatrixItems, opt =>
                      {
                          opt.Condition(s => s.BodyRegions != null);
                          opt.MapFrom(s => s.BodyRegions);
                      })
                  .ForMember(d => d.ExerciseBodyPartMatrixItems, opt =>
                      {
                          opt.Condition(s => s.BodyParts != null);
                          opt.MapFrom(s => s.BodyParts);
                      });
            Mapper.CreateMap<ExerciseVideoMatrixItem, VideoDto>()
                  .ForMember(d => d.Id, opt => opt.MapFrom(s => s.VideoId));
            Mapper.CreateMap<ExerciseEquipmentMatrixItem, VideoDto>()
                  .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EquipmentId));
            Mapper.CreateMap<Video, VideoDto>();
            Mapper.CreateMap<VideoDto, ExerciseVideoMatrixItem>()
                  .ForMember(d => d.VideoId, opt => opt.MapFrom(s => s.Id));
            Mapper.CreateMap<EquipmentDto, ExerciseEquipmentMatrixItem>()
                  .ForMember(d => d.EquipmentId, opt => opt.MapFrom(s => s.Id));
            Mapper.CreateMap<BodyRegionDto, ExerciseBodyRegionMatrixItem>()
                  .ForMember(d => d.BodyRegionId, opt => opt.MapFrom(s => s.id));
            Mapper.CreateMap<BodyPartDto, ExerciseBodyPartMatrixItem>()
                  .ForMember(d => d.BodyPartId, opt => opt.MapFrom(s => s.Id));
            Mapper.CreateMap<BodyRegionDto, InjuryBodyRegionMatrixItem>()
                  .ForMember(d => d.BodyRegionId, opt => opt.MapFrom(s => s.id));
            Mapper.CreateMap<VideoDto, Video>();
            Mapper.CreateMap<Equipment, EquipmentDto>();
            Mapper.CreateMap<EquipmentDto, Equipment>();
            Mapper.CreateMap<BodyRegion, BodyRegionDto>();
            Mapper.CreateMap<BodyRegionDto, BodyRegion>();
            Mapper.CreateMap<Cause, CauseDto>();
            Mapper.CreateMap<Sign, SignDto>();
            Mapper.CreateMap<CauseDto, Cause>();
            Mapper.CreateMap<SignDto, Sign>();
            Mapper.CreateMap<Injury, InjuryDto>()
                  .ForMember(d => d.plans, opt => opt.MapFrom(s => s.InjuryPlanMatrixItems.Select(p => p.Plan)))
                  .ForMember(d => d.causes, opt => opt.MapFrom(s => s.InjuryCauseMatrixItems.Select(p => p.Cause)))
                  .ForMember(d => d.signs, opt => opt.MapFrom(s => s.InjurySignMatrixItems.Select(p => p.Sign)))
                  .ForMember(d => d.bodyRegions,
                             opt => opt.MapFrom(s => s.InjuryBodyRegionMatrixItems.Select(p => p.BodyRegion)))
                  .ForMember(d => d.InjurySymptoms, opt =>
                      {
                          opt.Condition(s => s.InjurySymptomMatrixItems != null);
                          opt.MapFrom(s => s.InjurySymptomMatrixItems);
                      });
            Mapper.CreateMap<InjurySymptomMatrixItem, InjurySymptomDto>()
                  .ForMember(d => d.ThresholdValue, opt => opt.MapFrom(s => s.ThresholdValue))
                  .ForMember(d => d.SymptomId, opt => opt.MapFrom(s => s.SymptomMatrixItem.SymptomId))
                  .ForMember(d => d.name, opt => opt.MapFrom(s => s.SymptomMatrixItem.Symptom.Name))
                  .ForMember(d => d.description, opt => opt.MapFrom(s => s.SymptomMatrixItem.Symptom.Description))
                  .ForMember(d => d.renderType, opt => opt.MapFrom(s => s.SymptomMatrixItem.Symptom.RenderType.RenderType))
                  .ForMember(d => d.renderOptions, opt => opt.MapFrom(s => s.SymptomMatrixItem.Symptom.RenderOptions))
                  .ForMember(d => d.BodyPartMatrixItemName, opt => opt.MapFrom(s => String.Format("{0} {1} {2}", 
                                                                    s.SymptomMatrixItem.BodyPartMatrixItem.SkeletonArea.Orientation.Value, 
                                                                    s.SymptomMatrixItem.BodyPartMatrixItem.SkeletonArea.Side.Value, 
                                                                    s.SymptomMatrixItem.BodyPartMatrixItem.BodyPart.CommonName)))
                  .ForMember(d => d.BodyPartMatrixItemId, opt => opt.MapFrom(s => s.SymptomMatrixItem.BodyPartMatrixItem.Id));
            Mapper.CreateMap<BodyPartMatrixItem, BodyPartMatrixItemDto>()
                  .ForMember(d => d.SkeletonArea, opt => opt.MapFrom(s => s.SkeletonArea))
                  .ForMember(d => d.BodyPart, opt => opt.MapFrom(s => s.BodyPart));
            Mapper.CreateMap<InjuryDto, Injury>()
                  .ForMember(d => d.InjuryPlanMatrixItems, opt =>
                      {
                          opt.Condition(s => s.plans != null);
                          opt.MapFrom(s => s.plans);
                      })
                  .ForMember(d => d.InjurySignMatrixItems, opt =>
                      {
                          opt.Condition(s => s.signs != null);
                          opt.MapFrom(s => s.signs);
                      })
                  .ForMember(d => d.InjuryBodyRegionMatrixItems, opt =>
                      {
                          opt.Condition(s => s.bodyRegions != null);
                          opt.MapFrom(s => s.bodyRegions);
                      })
                  .ForMember(d => d.InjuryCauseMatrixItems, opt =>
                      {
                          opt.Condition(s => s.causes != null);
                          opt.MapFrom(s => s.causes);
                      })
                  .ForMember(d => d.InjurySymptomMatrixItems, opt =>
                      {
                          opt.Condition(s => s.InjurySymptoms != null);
                          opt.MapFrom(s => s.InjurySymptoms);
                      });
            Mapper.CreateMap<InjurySymptomDto, InjurySymptomMatrixItem>()
                  .ForMember(d => d.ThresholdValue, opt => opt.MapFrom(s => s.ThresholdValue))
                  .ForMember(d => d.SymptomMatrixItem, opt => opt.MapFrom(s => s));
            Mapper.CreateMap<InjurySymptomDto, SymptomMatrixItem>()
                  .ForMember(d => d.BodyPartMatrixItemId, opt => opt.MapFrom(s => s.BodyPartMatrixItemId))
                  .ForMember(d => d.SymptomId, opt => opt.MapFrom(s => s.SymptomId));
            Mapper.CreateMap<PlanDto, InjuryPlanMatrixItem>()
                  .ForMember(d => d.PlanId, opt => opt.MapFrom(s => s.Id));
            Mapper.CreateMap<CauseDto, InjuryCauseMatrixItem>()
                  .ForMember(d => d.CauseId, opt => opt.MapFrom(s => s.id));
            Mapper.CreateMap<SignDto, InjurySignMatrixItem>()
                  .ForMember(d => d.SignId, opt => opt.MapFrom(s => s.id));
            Mapper.CreateMap<BodyRegionDto, InjuryBodyRegionMatrixItem>()
                  .ForMember(d => d.BodyRegionId, opt => opt.MapFrom(s => s.id));
            Mapper.CreateMap<Injury, PotentialInjuryDto>()
                   .ForMember(d => d.plans, opt => opt.MapFrom(s => s.InjuryPlanMatrixItems.Select(p => p.Plan)))
                   .ForMember(d => d.causes, opt => opt.MapFrom(s => s.InjuryCauseMatrixItems.Select(p => p.Cause)))
                   .ForMember(d => d.signs, opt => opt.MapFrom(s => s.InjurySignMatrixItems.Select(p => p.Sign)));

        }
    }
}
