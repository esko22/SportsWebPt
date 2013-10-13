using System;
using System.Linq;

using AutoMapper;

using SportsWebPt.Platform.ServiceModels;
using SportsWebPt.Platform.Web.Core;

namespace SportsWebPt.Platform.Web.Services
{
    public static class ServicesContentMaps
    {
        public static void CreateContentMaps()
        {
            Mapper.CreateMap<User, UserDto>()
                  .Include<User, CreateUserRequest>()
                  .Include<User, UpdateUserRequest>();
            Mapper.CreateMap<User, CreateUserRequest>();
            Mapper.CreateMap<User, UpdateUserRequest>();
            Mapper.CreateMap<UserDto, User>();
            Mapper.CreateMap<SkeletonAreaDto, SkeletonArea>();
            Mapper.CreateMap<SkeletonArea, SkeletonAreaDto>();
            Mapper.CreateMap<BodyPartDto, BodyPart>();
            Mapper.CreateMap<BodyPart, BodyPartDto>()
                  .Include<BodyPart, CreateBodyPartRequest>()
                  .Include<BodyPart, UpdateBodyPartRequest>();
            Mapper.CreateMap<BodyPart, CreateBodyPartRequest>();
            Mapper.CreateMap<BodyPart, UpdateBodyPartRequest>();
            Mapper.CreateMap<BodyPartMatrixItemDto, BodyPartMatrixItem>()
                  .ForMember(d => d.displayName,
                             opt =>
                             opt.MapFrom(
                                 s =>
                                 String.Format("{0} >> {1}", s.SkeletonArea.DisplayName, s.BodyPart.CommonName)));
            Mapper.CreateMap<BodyPartMatrixItem,BodyPartMatrixItemDto>();
            Mapper.CreateMap<SymptomDto, Symptom>()
                                  .ForMember(d => d.displayName,
                             opt =>
                             opt.MapFrom(s => String.Format("{0} -- {1}", s.Name, s.RenderType)));
            Mapper.CreateMap<SymptomaticRegionDto, SymptomaticRegion>()
                  .ForMember(d => d.orientation, opt => opt.MapFrom(s => s.Orientation))
                  .ForMember(d => d.region, opt => opt.MapFrom(s => s.Region))
                  .ForMember(d => d.side, opt => opt.MapFrom(s => s.Side))
                  .ForMember(d => d.bodyParts, opt => opt.MapFrom(s => s.BodyParts));
            Mapper.CreateMap<SymptomaticBodyPartDto, SymptomaticBodyPart>()
                  .ForMember(d => d.potentialSymptoms, opt => opt.MapFrom(s => s.PotentialSymptoms));
            Mapper.CreateMap<PotentialSymptomDto, PotentialSymptom>();
            Mapper.CreateMap<PotentialSymptom, PotentialSymptomDto>();
            Mapper.CreateMap<DifferentialDiagnosis, CreateDiagnosisReportRequest>()
                  .ForMember(d => d.SubmittedFor, opt => opt.MapFrom(s => s.submittedFor));
            Mapper.CreateMap<DiagnosisReportDto, DiagnosisReport>();
            Mapper.CreateMap<InjurySymptomDto, InjurySymptom>()
                  .ForMember(d => d.bodyPartMatrixItemName, opt => opt.MapFrom(s => s.BodyPartMatrixItemName))
                  .ForMember(d => d.bodyPartMatrixItemId, opt => opt.MapFrom(s => s.BodyPartMatrixItemId))
                  .ForMember(d => d.symptomId, opt => opt.MapFrom(s => s.SymptomId));
            Mapper.CreateMap<InjurySymptom, InjurySymptomDto>()
                  .ForMember(d => d.BodyPartMatrixItemId, opt => opt.MapFrom(s => s.bodyPartMatrixItemId))
                  .ForMember(d => d.SymptomId, opt => opt.MapFrom(s => s.symptomId));
            
            Mapper.CreateMap<Injury, InjuryDto>()
                .Include<Injury,CreateInjuryRequest>()
                .Include<Injury, UpdateInjuryRequest>()
                  .ForMember(d => d.Plans, opt => opt.MapFrom(s => s.plans))
                  .ForMember(d => d.BodyRegions, opt => opt.MapFrom(s => s.bodyRegions))
                  .ForMember(d => d.Causes, opt => opt.MapFrom(s => s.causes))
                  .ForMember(d => d.Signs, opt => opt.MapFrom(s => s.signs))
                  .ForMember(d => d.InjurySymptoms, opt => opt.MapFrom(s => s.injurySymptoms));
            Mapper.CreateMap<Injury, CreateInjuryRequest>();
            Mapper.CreateMap<Injury, UpdateInjuryRequest>();

            Mapper.CreateMap<InjuryDto, Injury>()
                  .ForMember(d => d.plans, opt => opt.MapFrom(s => s.Plans))
                  .ForMember(d => d.causes, opt => opt.MapFrom(s => s.Causes))
                  .ForMember(d => d.signs, opt => opt.MapFrom(s => s.Signs))
                  .ForMember(d => d.injurySymptoms, opt => opt.MapFrom(s => s.InjurySymptoms));
            Mapper.CreateMap<PotentialInjuryDto, PotentialInjury>()
                  .ForMember(d => d.plans, opt => opt.MapFrom(s => s.Plans))
                  .ForMember(d => d.causes, opt => opt.MapFrom(s => s.Causes))
                  .ForMember(d => d.signs, opt => opt.MapFrom(s => s.Signs))
                  .ForMember(d => d.givenSymptoms, opt => opt.MapFrom(s => s.GivenSymptoms));

            Mapper.CreateMap<PlanDto, Plan>()
                  .ForMember(d => d.categories, opt => opt.MapFrom(s => s.Categories.Select(p => p.Replace("_", " "))));
            Mapper.CreateMap<Plan, PlanDto>()
                  .Include<Plan, CreatePlanRequest>()
                  .Include<Plan, UpdatePlanRequest>()
                  .ForMember(d => d.Categories, opt => opt.MapFrom(s => s.categories.Select(p => p.Replace(" ", "_"))));
            Mapper.CreateMap<Plan, CreatePlanRequest>();
            Mapper.CreateMap<Plan, UpdatePlanRequest>();

            
            
            Mapper.CreateMap<ExerciseDto, Exercise>()
                  .ForMember(d => d.categories, opt => opt.MapFrom(s => s.Categories.Select(p => p.Replace("_", " "))));
            Mapper.CreateMap<Exercise, ExerciseDto>()
                  .Include<Exercise, CreateExerciseRequest>()
                  .Include<Exercise, UpdateExerciseRequest>()
                  .ForMember(d => d.Categories, opt => opt.MapFrom(s => s.categories.Select(p => p.Replace(" ", "_"))));
            Mapper.CreateMap<Exercise, CreateExerciseRequest>();
            Mapper.CreateMap<Exercise, UpdateExerciseRequest>();

            Mapper.CreateMap<PlanExerciseDto, PlanExercise>();
            Mapper.CreateMap<PlanExercise, PlanExerciseDto>()
                  .ForMember(d => d.ExerciseId, opt => opt.MapFrom(s => s.exerciseId))
                  .ForMember(d => d.ExerciseId, opt =>
                      {
                          //This is in place to use planexercise id as id in the admin section when adding exercises to plans 
                          opt.Condition(s => s.exerciseId > 0 );
                          opt.MapFrom(s => s.exerciseId);
                      }); 
            Mapper.CreateMap<EquipmentDto, Equipment>()
                  .ForMember(d => d.category, opt => opt.MapFrom(s => s.Category.Replace("_", " ")));
            Mapper.CreateMap<Equipment, EquipmentDto>()
                  .Include<Equipment, CreateEquipmentRequest>()
                  .Include<Equipment, UpdateEquipmentRequest>()
                  .ForMember(d => d.Category, opt => opt.MapFrom(s => s.category.Replace(" ", "_")));
            Mapper.CreateMap<Equipment, CreateEquipmentRequest>();
            Mapper.CreateMap<Equipment, UpdateEquipmentRequest>();

            Mapper.CreateMap<VideoDto, Video>()
                  .ForMember(d => d.categories, opt => opt.MapFrom(s => s.Categories.Select(p => p.Replace("_", " "))));
            Mapper.CreateMap<Video, VideoDto>()
                  .Include<Video, UpdateVideoRequest>()
                  .Include<Video, CreateVideoRequest>()
                  .ForMember(d => d.Categories, opt => opt.MapFrom(s => s.categories.Select(p => p.Replace(" ", "_"))));
            Mapper.CreateMap<Video, UpdateVideoRequest>();
            Mapper.CreateMap<Video, CreateVideoRequest>();


            Mapper.CreateMap<CauseDto, Cause>();
            Mapper.CreateMap<Cause, CauseDto>()
                  .Include<Cause, CreateCauseRequest>()
                  .Include<Cause, UpdateCauseRequest>();
            Mapper.CreateMap<Cause, CreateCauseRequest>();
            Mapper.CreateMap<Cause, UpdateCauseRequest>();


            Mapper.CreateMap<SignDto, Sign>();
            Mapper.CreateMap<Sign, SignDto>()
                  .Include<Sign, CreateSignRequest>()
                  .Include<Sign, UpdateSignRequest>();
            Mapper.CreateMap<Sign, CreateSignRequest>();
            Mapper.CreateMap<Sign, UpdateSignRequest>();


            Mapper.CreateMap<BodyRegionDto, BodyRegion>();
            Mapper.CreateMap<BodyRegion, BodyRegionDto>()
                  .Include<BodyRegion, CreateBodyRegionRequest>()
                  .Include<BodyRegion, UpdateBodyRegionRequest>();
            Mapper.CreateMap<BodyRegion, CreateBodyRegionRequest>();
            Mapper.CreateMap<BodyRegion, UpdateBodyRegionRequest>();


            Mapper.CreateMap<UserFavoriteDto, UserFavorite>();
            Mapper.CreateMap<UserFavorite, CreateUserFavoriteRequest>();

        }
    }
}
