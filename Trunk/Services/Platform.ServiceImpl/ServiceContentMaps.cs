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
            Mapper.CreateMap<User, UserDto>()
                  .ForMember(d => d.VideoFavorites, opt =>
                      {
                          opt.Condition(s => s.VideoFavorites != null);
                          opt.MapFrom(s => s.VideoFavorites);
                      })
                  .ForMember(d => d.PlanFavorites, opt =>
                      {
                          opt.Condition(s => s.PlanFavorites != null);
                          opt.MapFrom(s => s.PlanFavorites);
                      })
                  .ForMember(d => d.InjuryFavorites, opt =>
                  {
                      opt.Condition(s => s.InjuryFavorites != null);
                      opt.MapFrom(s => s.InjuryFavorites);
                  })
                  .ForMember(d => d.ExerciseFavorites, opt =>
                  {
                      opt.Condition(s => s.ExerciseFavorites != null);
                      opt.MapFrom(s => s.ExerciseFavorites);
                  })
                  .ForMember(d => d.Hash, opt => opt.Ignore());

            Mapper.CreateMap<Plan, FavoriteDto>()
                  .ForMember(d => d.Entity, opt => opt.UseValue(FavoriteTypeDto.Plan))
                  .ForMember(d => d.EntityName, opt => opt.MapFrom(s => s.RoutineName))
                  .ForMember(d => d.EntityId, opt => opt.MapFrom(s => s.Id));
            Mapper.CreateMap<Video,FavoriteDto>()
                  .ForMember(d => d.Entity, opt => opt.UseValue(FavoriteTypeDto.Video ))
                  .ForMember(d => d.EntityName, opt => opt.MapFrom(s => s.Name))
                  .ForMember(d => d.EntityId, opt => opt.MapFrom(s => s.Id));
            Mapper.CreateMap<Injury, FavoriteDto>()
                  .ForMember(d => d.Entity, opt => opt.UseValue(FavoriteTypeDto.Injury))
                  .ForMember(d => d.EntityName, opt => opt.MapFrom(s => s.MedicalName))
                  .ForMember(d => d.EntityId, opt => opt.MapFrom(s => s.Id));
            Mapper.CreateMap<Exercise, FavoriteDto>()
                  .ForMember(d => d.Entity, opt => opt.UseValue(FavoriteTypeDto.Exercise))
                  .ForMember(d => d.EntityName, opt => opt.MapFrom(s => s.Name))
                  .ForMember(d => d.EntityId, opt => opt.MapFrom(s => s.Id));

            Mapper.CreateMap<UserDto, User>()
                  .Include<CreateUserRequest, User>()
                  .Include<UpdateUserRequest, User>();
            Mapper.CreateMap<CreateUserRequest, User>();
            Mapper.CreateMap<UpdateUserRequest, User>();

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
            Mapper.CreateMap<BodyPartDto, BodyPart>()
                  .Include<CreateBodyPartRequest, BodyPart>()
                  .Include<UpdateBodyPartRequest, BodyPart>()
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
            Mapper.CreateMap<CreateBodyPartRequest, BodyPart>();
            Mapper.CreateMap<UpdateBodyPartRequest, BodyPart>();


            Mapper.CreateMap<BodyPartMatrixItem, SkeletonAreaDto>()
                  .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SkeletonArea.Id))
                  .ForMember(d => d.Orientation, opt => opt.MapFrom(s => s.SkeletonArea.Orientation.Value))
                  .ForMember(d => d.Region, opt => opt.MapFrom(s => s.SkeletonArea.Region.Name))
                  .ForMember(d => d.DisplayName, opt => opt.MapFrom(s => s.SkeletonArea.DisplayName))
                  .ForMember(d => d.CssClassName, opt => opt.MapFrom(s => s.SkeletonArea.CssClassName))
                  .ForMember(d => d.Side, opt => opt.MapFrom(s => s.SkeletonArea.Side.Value));


            Mapper.CreateMap<SkeletonAreaDto, BodyPartMatrixItem>()
                  .ForMember(d => d.SkeletonAreaId, opt => opt.MapFrom(s => s.Id));
            Mapper.CreateMap<Symptom, SymptomDto>()
                  .ForMember(d => d.RenderType, opt => opt.MapFrom(s => s.RenderType.RenderType))
                  .ForMember(d => d.RenderOptions, opt => opt.MapFrom(s => s.RenderOptions))
                  .ForMember(d => d.RenderTemplate, opt => opt.MapFrom(s => s.RenderType.DefaultTemplate));
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
                    .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                   .ForMember(d => d.Description, opt => opt.MapFrom(s => s.Symptom.Description))
                   .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Symptom.Name))
                  .ForMember(d => d.RenderType, opt => opt.MapFrom(s => s.Symptom.RenderType.RenderType))
                  .ForMember(d => d.RenderOptions, opt => opt.MapFrom(s => s.Symptom.RenderOptions))
                  .ForMember(d => d.RenderTemplate, opt => opt.MapFrom(s => s.Symptom.RenderType.DefaultTemplate))
                   .ForMember(d => d.SymptomId, opt => opt.MapFrom(s => s.Symptom.Id));
            Mapper.CreateMap<DifferentialDiagnosisDto, DifferentialDiagnosis>()
                .ForMember(d => d.SumbittedOn, opt => opt.UseValue(DateTime.Now))
                .ForMember(d => d.SymptomDetails,
                    opt => opt.MapFrom(s => s.SymptomDetails.Where(p => !String.IsNullOrEmpty(p.GivenResponse))));
            Mapper.CreateMap<DifferentialDiagnosis, DifferentialDiagnosisDto>()
                   .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                   .ForMember(d => d.ReviewedOn, opt => opt.MapFrom(s => s.ReviewedOn))
                   .ForMember(d => d.SubmittedOn, opt => opt.MapFrom(s => s.SumbittedOn));
            Mapper.CreateMap<DifferentialDiagnosis, DiagnosisReportDto>()
                   .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                   .ForMember(d => d.ReviewedOn, opt => opt.MapFrom(s => s.ReviewedOn))
                   .ForMember(d => d.SubmittedOn, opt => opt.MapFrom(s => s.SumbittedOn));
            Mapper.CreateMap<SymptomDetail, PotentialSymptomDto>()
                  .ForMember(d => d.Name, opt => opt.MapFrom(s => s.SymptomMatrixItem.Symptom.Name))
                  .ForMember(d => d.BodyPart, opt => opt.MapFrom(s => s.SymptomMatrixItem.BodyPartMatrixItem.BodyPart.CommonName));
            Mapper.CreateMap<PotentialSymptomDto, SymptomDetail>()
                  .ForMember(d => d.SymptomMatrixItemId, opt => opt.MapFrom(s => s.Id));

            Mapper.CreateMap<Plan, BriefPlanDto>()
                  .ForMember(d => d.BodyRegions, opt =>
                  {
                      opt.Condition(s => s.PlanBodyRegionMatrixItems != null);
                      opt.MapFrom(s => s.PlanBodyRegionMatrixItems.Select(p => p.BodyRegion));
                  })
                  .ForMember(d => d.Categories, opt =>
                  {
                      opt.Condition(s => s.PlanCategoryMatrixItems != null);
                      opt.MapFrom(s => s.PlanCategoryMatrixItems.Select(p => p.Category));
                  });

            Mapper.CreateMap<Plan, PlanDto>()
                  .Include<Plan, CreatePlanRequest>()
                  .Include<Plan, UpdatePlanRequest>()
                  .ForMember(d => d.Exercises, opt =>
                      {
                          opt.Condition(s => s.PlanExerciseMatrixItems != null);
                          opt.MapFrom(s => s.PlanExerciseMatrixItems);
                      })
                  .ForMember(d => d.BodyRegions, opt =>
                      {
                          opt.Condition(s => s.PlanBodyRegionMatrixItems != null);
                          opt.MapFrom(s => s.PlanBodyRegionMatrixItems.Select(p => p.BodyRegion));
                      })
                  .ForMember(d => d.Categories, opt =>
                      {
                          opt.Condition(s => s.PlanCategoryMatrixItems != null);
                          opt.MapFrom(s => s.PlanCategoryMatrixItems.Select(p => p.Category));
                      });
            Mapper.CreateMap<Plan, CreatePlanRequest>();
            Mapper.CreateMap<Plan, UpdatePlanRequest>();
            
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
                      })
                  .ForMember(d => d.PlanCategoryMatrixItems, opt =>
                      {
                          opt.Condition(s => s.Categories != null);
                          opt.MapFrom(s => s.Categories);
                      });
            Mapper.CreateMap<String, PlanCategoryMatrixItem>()
                  .ForMember(d => d.Category, opt => opt.MapFrom(s => s));
            Mapper.CreateMap<BodyRegionDto, PlanBodyRegionMatrixItem>()
                  .ForMember(d => d.BodyRegionId, opt => opt.MapFrom(s => s.Id));
            Mapper.CreateMap<PlanExerciseDto, PlanExerciseMatrixItem>()
                  .ForMember(d => d.ExerciseId, opt => opt.MapFrom(s => s.ExerciseId))
                  .ForMember(d => d.Repititions, opt => opt.MapFrom(s => s.Repititions))
                  .ForMember(d => d.Sets, opt => opt.MapFrom(s => s.Sets))
                  .ForMember(d => d.PerWeek, opt => opt.MapFrom(s => s.PerWeek))
                  .ForMember(d => d.PerDay, opt => opt.MapFrom(s => s.PerDay))
                  .ForMember(d => d.HoldFor, opt => opt.MapFrom(s => s.HoldFor));

            Mapper.CreateMap<String, ExerciseCategoryMatrixItem>()
                  .ForMember(d => d.Category, opt => opt.MapFrom(s => s));
            Mapper.CreateMap<Exercise, ExerciseDto>()
                  .Include<Exercise, CreateExerciseRequest>()
                  .Include<Exercise, UpdateExerciseRequest>()
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
                      })
                  .ForMember(d => d.Categories, opt =>
                      {
                          opt.Condition(s => s.ExerciseCategoryMatrixItems != null);
                          opt.MapFrom(s => s.ExerciseCategoryMatrixItems.Select(p => p.Category));
                      });
            Mapper.CreateMap<Exercise, CreateExerciseRequest>();
            Mapper.CreateMap<Exercise, UpdateExerciseRequest>();

            Mapper.CreateMap<Exercise, BriefExerciseDto>()
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
                  .ForMember(d => d.Categories, opt =>
                  {
                      opt.Condition(s => s.ExerciseCategoryMatrixItems != null);
                      opt.MapFrom(s => s.ExerciseCategoryMatrixItems.Select(p => p.Category));
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
                  .ForMember(d => d.HoldType, opt => opt.MapFrom(s => s.HoldType))
                  .ForMember(d => d.ExerciseId, opt => opt.MapFrom(s => s.ExerciseId));
            Mapper.CreateMap<ExerciseDto, Exercise>()
                  .ForMember(d => d.Repititions, opt => opt.MapFrom(s => s.Repititions))
                  .ForMember(d => d.Sets, opt => opt.MapFrom(s => s.Sets))
                  .ForMember(d => d.PerDay, opt => opt.MapFrom(s => s.PerDay))
                  .ForMember(d => d.PerWeek, opt => opt.MapFrom(s => s.PerWeek))
                  .ForMember(d => d.HoldFor, opt => opt.MapFrom(s => s.HoldFor))
                  .ForMember(d => d.HoldType, opt => opt.MapFrom(s => s.HoldType))
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
                      })
                  .ForMember(d => d.ExerciseCategoryMatrixItems, opt =>
                      {
                          opt.Condition(s => s.Categories != null);
                          opt.MapFrom(s => s.Categories);
                      });

            
            
            Mapper.CreateMap<ExerciseVideoMatrixItem, VideoDto>()
                  .ForMember(d => d.Id, opt => opt.MapFrom(s => s.VideoId));
            Mapper.CreateMap<ExerciseEquipmentMatrixItem, VideoDto>()
                  .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EquipmentId));
            Mapper.CreateMap<String, VideoCategoryMatrixItem>()
                  .ForMember(d => d.Category, opt => opt.MapFrom(s => s));
            Mapper.CreateMap<Video, VideoDto>()
                  .ForMember(d => d.Categories, opt =>
                      {
                          opt.Condition(s => s.VideoCategoryMatrixItems != null);
                          opt.MapFrom(s => s.VideoCategoryMatrixItems.Select(p => p.Category));
                      });
            Mapper.CreateMap<VideoDto, ExerciseVideoMatrixItem>()
                  .ForMember(d => d.VideoId, opt => opt.MapFrom(s => s.Id));
            Mapper.CreateMap<EquipmentDto, ExerciseEquipmentMatrixItem>()
                  .ForMember(d => d.EquipmentId, opt => opt.MapFrom(s => s.Id));
            Mapper.CreateMap<BodyRegionDto, ExerciseBodyRegionMatrixItem>()
                  .ForMember(d => d.BodyRegionId, opt => opt.MapFrom(s => s.Id));
            Mapper.CreateMap<BodyPartDto, ExerciseBodyPartMatrixItem>()
                  .ForMember(d => d.BodyPartId, opt => opt.MapFrom(s => s.Id));
            Mapper.CreateMap<BodyRegionDto, InjuryBodyRegionMatrixItem>()
                  .ForMember(d => d.BodyRegionId, opt => opt.MapFrom(s => s.Id));
            
            Mapper.CreateMap<VideoDto, Video>()
                  .Include<CreateVideoRequest,Video>()
                  .Include<UpdateVideoRequest,Video>()
                  .ForMember(d => d.VideoCategoryMatrixItems, opt =>
                      {
                          opt.Condition(s => s.Categories != null);
                          opt.MapFrom(s => s.Categories);
                      });
            Mapper.CreateMap<CreateVideoRequest, Video>();
            Mapper.CreateMap<UpdateVideoRequest, Video>();

            Mapper.CreateMap<Equipment, EquipmentDto>();
            Mapper.CreateMap<Equipment, BriefEquipmentDto>();
            Mapper.CreateMap<EquipmentDto, Equipment>()
                  .Include<CreateEquipmentRequest, Equipment>()
                  .Include<UpdateEquipmentRequest, Equipment>();
            Mapper.CreateMap<CreateEquipmentRequest, Equipment>();
            Mapper.CreateMap<UpdateEquipmentRequest, Equipment>();




            Mapper.CreateMap<BodyRegion, BodyRegionDto>();
            Mapper.CreateMap<BodyRegionDto, BodyRegion>()
                  .Include<CreateBodyRegionRequest, BodyRegion>()
                  .Include<UpdateBodyRegionRequest, BodyRegion>();
            Mapper.CreateMap<CreateBodyRegionRequest, BodyRegion>();
            Mapper.CreateMap<UpdateBodyRegionRequest, BodyRegion>();
            
            
            
            Mapper.CreateMap<Cause, CauseDto>();
            Mapper.CreateMap<CauseDto, Cause>()
                  .Include<CreateCauseRequest, Cause>()
                  .Include<UpdateCauseRequest, Cause>();
            Mapper.CreateMap<CreateCauseRequest, Cause>()
                  .ForMember(d => d.FilterId, opt => opt.MapFrom(s => s.Filter.Id))
                  .ForMember(d => d.Filter, opt => opt.Ignore());

            Mapper.CreateMap<UpdateCauseRequest, Cause>()
                  .ForMember(d => d.FilterId, opt => opt.MapFrom(s => s.Filter.Id))
                  .ForMember(d => d.Filter, opt => opt.Ignore());
            Mapper.CreateMap<Injury, BriefInjuryDto>()
                .ForMember(d => d.Signs, opt => opt.MapFrom(s => s.InjurySignMatrixItems.Select(p => p.Sign)))
                .ForMember(d => d.BodyRegions,
                    opt => opt.MapFrom(s => s.InjuryBodyRegionMatrixItems.Select(p => p.BodyRegion)));

            Mapper.CreateMap<Injury, InjuryDto>()
                  .ForMember(d => d.Plans, opt => opt.MapFrom(s => s.InjuryPlanMatrixItems.Select(p => p.Plan)))
                  .ForMember(d => d.Causes, opt => opt.MapFrom(s => s.InjuryCauseMatrixItems.Select(p => p.Cause)))
                  .ForMember(d => d.Signs, opt => opt.MapFrom(s => s.InjurySignMatrixItems.Select(p => p.Sign)))
                  .ForMember(d => d.Treatments,
                             opt => opt.MapFrom(s => s.InjuryTreatmentMatrixItems.Select(p => p.Treatment)))
                  .ForMember(d => d.InjuryPrognoses,
                             opt => opt.MapFrom(s => s.InjuryPrognosisMatrixItems.Select(p => p.Prognosis)))
                  .ForMember(d => d.BodyRegions,
                             opt => opt.MapFrom(s => s.InjuryBodyRegionMatrixItems.Select(p => p.BodyRegion)))
                  .ForMember(d => d.InjurySymptoms, opt =>
                      {
                          opt.Condition(s => s.InjurySymptomMatrixItems != null);
                          opt.MapFrom(s => s.InjurySymptomMatrixItems);
                      })
                  .ForMember(d => d.InjuryPrognoses, opt =>
                      {
                          opt.Condition(s => s.InjuryPrognosisMatrixItems != null);
                          opt.MapFrom(s => s.InjuryPrognosisMatrixItems);
                      });
                  
            Mapper.CreateMap<InjurySymptomMatrixItem, InjurySymptomDto>()
                  .ForMember(d => d.SymptomId, opt => opt.MapFrom(s => s.SymptomMatrixItem.SymptomId))
                  .ForMember(d => d.Name, opt => opt.MapFrom(s => s.SymptomMatrixItem.Symptom.Name))
                  .ForMember(d => d.Description, opt => opt.MapFrom(s => s.SymptomMatrixItem.Symptom.Description))
                  .ForMember(d => d.RenderType, opt => opt.MapFrom(s => s.SymptomMatrixItem.Symptom.RenderType.RenderType))
                  .ForMember(d => d.RenderTemplate, opt => opt.MapFrom(s => s.SymptomMatrixItem.Symptom.RenderType.DefaultTemplate))
                  .ForMember(d => d.RenderOptions, opt => opt.MapFrom(s => s.SymptomMatrixItem.Symptom.RenderOptions))
                  .ForMember(d => d.BodyPartMatrixItemName, opt => opt.MapFrom(s => String.Format("{0} {1} {2}", 
                                                                    s.SymptomMatrixItem.BodyPartMatrixItem.SkeletonArea.Orientation.Value, 
                                                                    s.SymptomMatrixItem.BodyPartMatrixItem.SkeletonArea.Side.Value, 
                                                                    s.SymptomMatrixItem.BodyPartMatrixItem.BodyPart.CommonName)))
                  .ForMember(d => d.BodyPartMatrixItemId, opt => opt.MapFrom(s => s.SymptomMatrixItem.BodyPartMatrixItem.Id));
            Mapper.CreateMap<BodyPartMatrixItem, BodyPartMatrixItemDto>()
                  .ForMember(d => d.SkeletonArea, opt => opt.MapFrom(s => s.SkeletonArea))
                  .ForMember(d => d.BodyPart, opt => opt.MapFrom(s => s.BodyPart));
 
            Mapper.CreateMap<InjuryDto, Injury>()
                .Include<CreateInjuryRequest,Injury>()
                .Include<UpdateInjuryRequest,Injury>()
                  .ForMember(d => d.InjuryPlanMatrixItems, opt =>
                      {
                          opt.Condition(s => s.Plans != null);
                          opt.MapFrom(s => s.Plans);
                      })
                  .ForMember(d => d.InjurySignMatrixItems, opt =>
                      {
                          opt.Condition(s => s.Signs != null);
                          opt.MapFrom(s => s.Signs);
                      })
                  .ForMember(d => d.InjuryBodyRegionMatrixItems, opt =>
                      {
                          opt.Condition(s => s.BodyRegions != null);
                          opt.MapFrom(s => s.BodyRegions);
                      })
                  .ForMember(d => d.InjuryCauseMatrixItems, opt =>
                      {
                          opt.Condition(s => s.Causes != null);
                          opt.MapFrom(s => s.Causes);
                      })
                  .ForMember(d => d.InjurySymptomMatrixItems, opt =>
                      {
                          opt.Condition(s => s.InjurySymptoms != null);
                          opt.MapFrom(s => s.InjurySymptoms);
                      })
                  .ForMember(d => d.InjuryPrognosisMatrixItems, opt =>
                  {
                      opt.Condition(s => s.InjuryPrognoses != null);
                      opt.MapFrom(s => s.InjuryPrognoses);
                  })
                  .ForMember(d => d.InjuryTreatmentMatrixItems, opt =>
                  {
                      opt.Condition(s => s.Treatments != null);
                      opt.MapFrom(s => s.Treatments);
                  });

            Mapper.CreateMap<CreateInjuryRequest, Injury>();
            Mapper.CreateMap<UpdateInjuryRequest, Injury>();

            Mapper.CreateMap<InjurySymptomDto, InjurySymptomMatrixItem>()
                  .ForMember(d => d.SymptomMatrixItem, opt => opt.MapFrom(s => s));
            Mapper.CreateMap<InjurySymptomDto, SymptomMatrixItem>()
                  .ForMember(d => d.BodyPartMatrixItemId, opt => opt.MapFrom(s => s.BodyPartMatrixItemId))
                  .ForMember(d => d.SymptomId, opt => opt.MapFrom(s => s.SymptomId));
            Mapper.CreateMap<InjuryPrognosisDto, InjuryPrognosisMatrixItem>()
                  .ForMember(d => d.Id, opt => opt.Ignore());
            Mapper.CreateMap<InjuryPrognosisMatrixItem, InjuryPrognosisDto>()
                  .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Prognosis.Name))
                  .ForMember(d => d.Category, opt => opt.MapFrom(s => s.Prognosis.Category))
                  .ForMember(d => d.Description, opt => opt.MapFrom(s => s.Prognosis.Description))
                  .ForMember(d => d.PrognosisId, opt => opt.MapFrom(s => s.PrognosisId));
            Mapper.CreateMap<PlanDto, InjuryPlanMatrixItem>()
                  .ForMember(d => d.PlanId, opt => opt.MapFrom(s => s.Id));
            Mapper.CreateMap<CauseDto, InjuryCauseMatrixItem>()
                  .ForMember(d => d.CauseId, opt => opt.MapFrom(s => s.Id));
            Mapper.CreateMap<TreatmentDto, InjuryTreatmentMatrixItem>()
                  .ForMember(d => d.TreatmentId, opt => opt.MapFrom(s => s.Id));
            Mapper.CreateMap<SignDto, InjurySignMatrixItem>()
                  .ForMember(d => d.SignId, opt => opt.MapFrom(s => s.Id));
            Mapper.CreateMap<BodyRegionDto, InjuryBodyRegionMatrixItem>()
                  .ForMember(d => d.BodyRegionId, opt => opt.MapFrom(s => s.Id));
            Mapper.CreateMap<Injury, PotentialInjuryDto>()
                  .ForMember(d => d.Plans, opt => opt.MapFrom(s => s.InjuryPlanMatrixItems.Select(p => p.Plan)))
                  .ForMember(d => d.Causes, opt => opt.MapFrom(s => s.InjuryCauseMatrixItems.Select(p => p.Cause)))
                  .ForMember(d => d.Signs, opt => opt.MapFrom(s => s.InjurySignMatrixItems.Select(p => p.Sign)));

            Mapper.CreateMap<Filter, FilterDto>();
            Mapper.CreateMap<FilterDto, Filter>();


            Mapper.CreateMap<Sign, SignDto>();
            Mapper.CreateMap<SignDto, Sign>()
                  .Include<CreateSignRequest, Sign>()
                  .Include<UpdateSignRequest, Sign>();

            Mapper.CreateMap<CreateSignRequest, Sign>()
                  .ForMember(d => d.FilterId, opt => opt.MapFrom(s => s.Filter.Id))
                  .ForMember(d => d.Filter, opt => opt.Ignore());
            Mapper.CreateMap<UpdateSignRequest, Sign>()
                  .ForMember(d => d.FilterId, opt => opt.MapFrom(s => s.Filter.Id))
                  .ForMember(d => d.Filter, opt => opt.Ignore());

            Mapper.CreateMap<Treatment, TreatmentDto>();
            Mapper.CreateMap<TreatmentDto, Treatment>();

            Mapper.CreateMap<Prognosis, PrognosisDto>();
            Mapper.CreateMap<PrognosisDto, Prognosis>();


        }
    }
}
