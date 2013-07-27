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
                  .ForMember(d => d.plans, opt => opt.MapFrom(s =>s.plans))
                  .ForMember(d => d.causes, opt => opt.MapFrom(s =>s.causes))                  
                  .ForMember(d => d.signs, opt => opt.MapFrom(s =>s.signs));
            Mapper.CreateMap<Injury, InjuryDto>()
                  .ForMember(d => d.plans, opt => opt.MapFrom(s => s.plans))
                  .ForMember(d => d.bodyRegions, opt => opt.MapFrom(s => s.bodyRegions))
                  .ForMember(d => d.causes, opt => opt.MapFrom(s => s.causes))
                  .ForMember(d => d.signs, opt => opt.MapFrom(s => s.signs));
            Mapper.CreateMap<PotentialInjuryDto, PotentialInjury>()
                  .ForMember(d => d.plans, opt => opt.MapFrom(s => s.plans))
                  .ForMember(d => d.causes, opt => opt.MapFrom(s => s.causes))
                  .ForMember(d => d.signs, opt => opt.MapFrom(s => s.signs))
                  .ForMember(d => d.givenSymptoms, opt => opt.MapFrom(s => s.givenSymptoms));
            Mapper.CreateMap<PlanDto, Plan>();
            Mapper.CreateMap<Plan, PlanDto>();
            Mapper.CreateMap<ExerciseDto, Exercise>();
            Mapper.CreateMap<PlanExerciseDto, PlanExercise>();
            Mapper.CreateMap<Exercise, ExerciseDto>();
            Mapper.CreateMap<PlanExercise, PlanExerciseDto>()
                  .ForMember(d => d.ExerciseId, opt => opt.MapFrom(s => s.exerciseId))
                  .ForMember(d => d.ExerciseId, opt =>
                      {
                          //This is in place to get around weirdness in the admin section when adding exercises to plans 
                          opt.Condition(s => s.refExercise > 0 );
                          opt.MapFrom(s => s.refExercise);
                      }); 
            Mapper.CreateMap<EquipmentDto, Equipment>();
            Mapper.CreateMap<Equipment, EquipmentDto>();
            Mapper.CreateMap<VideoDto, Video>();
            Mapper.CreateMap<Video, VideoDto>();
            Mapper.CreateMap<CauseDto, Cause>();
            Mapper.CreateMap<SignDto, Sign>();
            Mapper.CreateMap<Cause, CauseDto>();
            Mapper.CreateMap<Sign, SignDto>();
            Mapper.CreateMap<BodyRegion, BodyRegionDto>();
            Mapper.CreateMap<BodyRegionDto, BodyRegion>();

        }
    }
}
