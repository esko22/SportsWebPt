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
            Mapper.CreateMap<User, UserDto>();
            Mapper.CreateMap<UserDto, User>();
            Mapper.CreateMap<SkeletonAreaDto, SkeletonArea>();
            Mapper.CreateMap<SkeletonArea, SkeletonAreaDto>();
            Mapper.CreateMap<BodyPartDto, BodyPart>();
            Mapper.CreateMap<BodyPart, BodyPartDto>();
            Mapper.CreateMap<BodyPartMatrixItemDto, BodyPartMatrixItem>()
                  .ForMember(d => d.displayName,
                             opt =>
                             opt.MapFrom(
                                 s =>
                                 String.Format("{0} >> {1}", s.SkeletonArea.DisplayName, s.BodyPart.CommonName)));
            Mapper.CreateMap<BodyPartMatrixItem, BodyPartMatrixItemDto>();
            Mapper.CreateMap<SymptomDto, Symptom>()
                                  .ForMember(d => d.displayName,
                             opt =>
                             opt.MapFrom(s => String.Format("{0} -- {1}", s.name, s.renderType)));
            Mapper.CreateMap<SymptomaticRegionDto, SymptomaticRegion>()
                  .ForMember(d => d.orientation, opt => opt.MapFrom(s => s.Orientation))
                  .ForMember(d => d.region, opt => opt.MapFrom(s => s.Region))
                  .ForMember(d => d.side, opt => opt.MapFrom(s => s.Side))
                  .ForMember(d => d.bodyParts, opt => opt.MapFrom(s => s.BodyParts));
            Mapper.CreateMap<SymptomaticBodyPartDto, SymptomaticBodyPart>()
                  .ForMember(d => d.potentialSymptoms, opt => opt.MapFrom(s => s.PotentialSymptoms));
            Mapper.CreateMap<PotentialSymptomDto, PotentialSymptom>();
            Mapper.CreateMap<PotentialSymptom, PotentialSymptomDto>();
            Mapper.CreateMap<DifferentialDiagnosis, DifferentialDiagnosisDto>()
                  .ForMember(d => d.submittedFor, opt => opt.MapFrom(s => s.submittedFor));
            Mapper.CreateMap<DiagnosisReportDto, DiagnosisReport>();
            Mapper.CreateMap<InjuryDto, Injury>()
                  .ForMember(d => d.plans, opt => opt.MapFrom(s =>s.plans))
                  .ForMember(d => d.causes, opt => opt.MapFrom(s =>s.causes))                  
                  .ForMember(d => d.signs, opt => opt.MapFrom(s =>s.signs))
                  .ForMember(d => d.injurySymptoms, opt => opt.MapFrom(s => s.InjurySymptoms));
            Mapper.CreateMap<InjurySymptomDto, InjurySymptom>()
                  .ForMember(d => d.bodyPartMatrixItemName, opt => opt.MapFrom(s => s.BodyPartMatrixItemName))
                  .ForMember(d => d.bodyPartMatrixItemId, opt => opt.MapFrom(s => s.BodyPartMatrixItemId))
                  .ForMember(d => d.thresholdValue, opt => opt.MapFrom(s => s.ThresholdValue))
                  .ForMember(d => d.symptomId, opt => opt.MapFrom(s => s.SymptomId));
            Mapper.CreateMap<InjurySymptom, InjurySymptomDto>()
                  .ForMember(d => d.BodyPartMatrixItemId, opt => opt.MapFrom(s => s.bodyPartMatrixItemId))
                  .ForMember(d => d.ThresholdValue, opt => opt.MapFrom(s => s.thresholdValue))
                  .ForMember(d => d.SymptomId, opt => opt.MapFrom(s => s.symptomId));
            Mapper.CreateMap<Injury, InjuryDto>()
                  .ForMember(d => d.plans, opt => opt.MapFrom(s => s.plans))
                  .ForMember(d => d.bodyRegions, opt => opt.MapFrom(s => s.bodyRegions))
                  .ForMember(d => d.causes, opt => opt.MapFrom(s => s.causes))
                  .ForMember(d => d.signs, opt => opt.MapFrom(s => s.signs))
                  .ForMember(d => d.InjurySymptoms, opt => opt.MapFrom(s => s.injurySymptoms));
            Mapper.CreateMap<PotentialInjuryDto, PotentialInjury>()
                  .ForMember(d => d.plans, opt => opt.MapFrom(s => s.plans))
                  .ForMember(d => d.causes, opt => opt.MapFrom(s => s.causes))
                  .ForMember(d => d.signs, opt => opt.MapFrom(s => s.signs))
                  .ForMember(d => d.givenSymptoms, opt => opt.MapFrom(s => s.givenSymptoms));
            Mapper.CreateMap<PlanDto, Plan>()
                  .ForMember(d => d.categories, opt => opt.MapFrom(s => s.Categories.Select(p => p.Replace("_", " "))));
            Mapper.CreateMap<Plan, PlanDto>()
                  .ForMember(d => d.Categories, opt => opt.MapFrom(s => s.categories.Select(p => p.Replace(" ", "_"))));
            Mapper.CreateMap<ExerciseDto, Exercise>()
                  .ForMember(d => d.categories, opt => opt.MapFrom(s => s.Categories.Select(p => p.Replace("_", " "))));
            Mapper.CreateMap<PlanExerciseDto, PlanExercise>();
            Mapper.CreateMap<Exercise, ExerciseDto>()
                  .ForMember(d => d.Categories, opt => opt.MapFrom(s => s.categories.Select(p => p.Replace(" ", "_"))));
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
                  .ForMember(d => d.Category, opt => opt.MapFrom(s => s.category.Replace(" ", "_")));
            Mapper.CreateMap<VideoDto, Video>()
                  .ForMember(d => d.categories, opt => opt.MapFrom(s => s.Categories.Select(p => p.Replace("_", " "))));
            Mapper.CreateMap<Video, VideoDto>()
                  .ForMember(d => d.Categories, opt => opt.MapFrom(s => s.categories.Select(p => p.Replace(" ", "_"))));
            Mapper.CreateMap<CauseDto, Cause>();
            Mapper.CreateMap<SignDto, Sign>();
            Mapper.CreateMap<Cause, CauseDto>();
            Mapper.CreateMap<Sign, SignDto>();
            Mapper.CreateMap<BodyRegion, BodyRegionDto>();
            Mapper.CreateMap<BodyRegionDto, BodyRegion>();

        }
    }
}
