﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using BrockAllen.MembershipReboot;
using BrockAllen.MembershipReboot.Relational;
using SportsWebPt.Identity.Core;
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
            Mapper.CreateMap<UserDto, User>()
                  .ForMember(d => d.videos, opt =>
                      {
                          opt.Condition(s => s.VideoFavorites != null);
                          opt.MapFrom(s => s.VideoFavorites);
                      })
                  .ForMember(d => d.plans, opt =>
                      {
                          opt.Condition(s => s.PlanFavorites != null);
                          opt.MapFrom(s => s.PlanFavorites);
                      })
                  .ForMember(d => d.injuries, opt =>
                      {
                          opt.Condition(s => s.InjuryFavorites != null);
                          opt.MapFrom(s => s.InjuryFavorites);
                      })
                  .ForMember(d => d.exercises, opt =>
                      {
                          opt.Condition(s => s.ExerciseFavorites != null);
                          opt.MapFrom(s => s.ExerciseFavorites);
                      });
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
            Mapper.CreateMap<PotentialSymptomDto, PotentialSymptom>()
                  .ForMember(d => d.givenResponse, opt => opt.MapFrom(s => s.GivenResponse.Split(',')));
            Mapper.CreateMap<PotentialSymptom, PotentialSymptomDto>()
                  .ForMember(d => d.GivenResponse, opt => opt.MapFrom(s => String.Join(",", s.givenResponse ?? new[] { "0" })));
            Mapper.CreateMap<DifferentialDiagnosis, CreateDiagnosisReportRequest>()
                  .ForMember(d => d.SubmittedFor, opt => opt.MapFrom(s => s.submittedFor));
            Mapper.CreateMap<DifferentialDiagnosisDto, DifferentialDiagnosis>();
            Mapper.CreateMap<DiagnosisReportDto, DiagnosisReport>();
            Mapper.CreateMap<InjurySymptomDto, InjurySymptom>()
                  .ForMember(d => d.bodyPartMatrixItemName, opt => opt.MapFrom(s => s.BodyPartMatrixItemName))
                  .ForMember(d => d.bodyPartMatrixItemId, opt => opt.MapFrom(s => s.BodyPartMatrixItemId))
                  .ForMember(d => d.symptomId, opt => opt.MapFrom(s => s.SymptomId))
                  .ForMember(d => d.givenResponse, opt => opt.MapFrom(s => s.ComparisonValue.Split(',')));
            Mapper.CreateMap<InjurySymptom, InjurySymptomDto>()
                  .ForMember(d => d.BodyPartMatrixItemId, opt => opt.MapFrom(s => s.bodyPartMatrixItemId))
                  .ForMember(d => d.SymptomId, opt => opt.MapFrom(s => s.symptomId))
                  .ForMember(d => d.ComparisonValue,opt => opt.MapFrom(s => String.Join(",", s.givenResponse)));



            Mapper.CreateMap<InjuryPrognosisDto, Prognosis>();
            Mapper.CreateMap<InjuryPrognosisDto, InjuryPrognosis>()
                  .ForMember(d => d.prognosisId, opt => opt.MapFrom(s => s.PrognosisId));

            Mapper.CreateMap<InjuryPrognosis, InjuryPrognosisDto>()
                  .ForMember(d => d.PrognosisId, opt => opt.MapFrom(s => s.prognosisId));
            
            
            Mapper.CreateMap<Injury, InjuryDto>()
                .Include<Injury,CreateInjuryRequest>()
                .Include<Injury, UpdateInjuryRequest>()
                  .ForMember(d => d.Plans, opt => opt.MapFrom(s => s.plans))
                  .ForMember(d => d.BodyRegions, opt => opt.MapFrom(s => s.bodyRegions))
                  .ForMember(d => d.Causes, opt => opt.MapFrom(s => s.causes))
                  .ForMember(d => d.Signs, opt => opt.MapFrom(s => s.signs))
                  .ForMember(d => d.Treatments, opt => opt.MapFrom(s => s.treatments))
                  .ForMember(d => d.InjuryPrognoses, opt => opt.MapFrom(s => s.injuryPrognoses))
                  .ForMember(d => d.InjurySymptoms, opt => opt.MapFrom(s => s.injurySymptoms));
            Mapper.CreateMap<Injury, CreateInjuryRequest>();
            Mapper.CreateMap<Injury, UpdateInjuryRequest>();

            Mapper.CreateMap<BriefInjuryDto, BriefInjury>()
                .ForMember(d => d.signs, opt => opt.MapFrom(s => s.Signs));


            Mapper.CreateMap<InjuryDto, Injury>()
                  .ForMember(d => d.plans, opt => opt.MapFrom(s => s.Plans))
                  .ForMember(d => d.causes, opt => opt.MapFrom(s => s.Causes))
                  .ForMember(d => d.signs, opt => opt.MapFrom(s => s.Signs))
                  .ForMember(d => d.treatments, opt => opt.MapFrom(s => s.Treatments))
                  .ForMember(d => d.injuryPrognoses, opt => opt.MapFrom(s => s.InjuryPrognoses))
                  .ForMember(d => d.injurySymptoms, opt => opt.MapFrom(s => s.InjurySymptoms))
                  .ForMember(d => d.bestCase, opt => opt.MapFrom(s => s.InjuryPrognoses.FirstOrDefault(p => p.Category.Equals("bestcase",StringComparison.OrdinalIgnoreCase))))
                  .ForMember(d => d.worstCase, opt => opt.MapFrom(s => s.InjuryPrognoses.FirstOrDefault(p => p.Category.Equals("worstcase",StringComparison.OrdinalIgnoreCase))))
                  .ForMember(d => d.delayedRecovery, opt => opt.MapFrom(s => s.InjuryPrognoses.FirstOrDefault(p => p.Category.Equals("delayedrecovery",StringComparison.OrdinalIgnoreCase))));
            Mapper.CreateMap<PotentialInjuryDto, PotentialInjury>()
                  .ForMember(d => d.plans, opt => opt.MapFrom(s => s.Plans))
                  .ForMember(d => d.causes, opt => opt.MapFrom(s => s.Causes))
                  .ForMember(d => d.signs, opt => opt.MapFrom(s => s.Signs))
                  .ForMember(d => d.givenSymptoms, opt => opt.MapFrom(s => s.GivenSymptoms))
                  .ForMember(d => d.treatments, opt => opt.MapFrom(s => s.Treatments))
                  .ForMember(d => d.injuryPrognoses, opt => opt.MapFrom(s => s.InjuryPrognoses))
                  .ForMember(d => d.bestCase, opt => opt.MapFrom(s => s.InjuryPrognoses.FirstOrDefault(p => p.Category.Equals("bestcase", StringComparison.OrdinalIgnoreCase))))
                  .ForMember(d => d.worstCase, opt => opt.MapFrom(s => s.InjuryPrognoses.FirstOrDefault(p => p.Category.Equals("worstcase", StringComparison.OrdinalIgnoreCase))))
                  .ForMember(d => d.delayedRecovery, opt => opt.MapFrom(s => s.InjuryPrognoses.FirstOrDefault(p => p.Category.Equals("delayedrecovery", StringComparison.OrdinalIgnoreCase))));


            Mapper.CreateMap<BriefPlanDto, BriefPlan>()
                .ForMember(d => d.sharedClinics, opt => opt.MapFrom(s => s.SharedClinics))
                .ForMember(d => d.formattedCategories,
                    opt => opt.MapFrom(s => String.Join(",", s.Categories.OrderBy(o => o))))
                .ForMember(d => d.categories, opt =>
                {
                    opt.Condition(s => s.Categories != null && s.Categories.Any());
                    opt.MapFrom(s => s.Categories.Select(p => p.Replace("_", " ")));
                })
                .ForMember(d => d.formattedBodyRegions,
                    opt => opt.MapFrom(s => String.Join(",", s.BodyRegions.OrderBy(o => o.Name).Select(b => b.Name))));


            Mapper.CreateMap<BriefInjuryDto, GridInjury>()
                .ForMember(d => d.bodyRegions, opt => opt.MapFrom(s => String.Join(",", s.BodyRegions.OrderBy(o => o.Name).Select(b => b.Name))));

            Mapper.CreateMap<BriefExerciseDto, BriefExercise>()
                .ForMember(d => d.sharedClinics, opt => opt.MapFrom(s => s.SharedClinics))
                .ForMember(d => d.formattedCategories,
                    opt => opt.MapFrom(s => String.Join(",", s.Categories.OrderBy(o => o))))
                .ForMember(d => d.formattedBodyRegions,
                    opt => opt.MapFrom(s => String.Join(",", s.BodyRegions.OrderBy(o => o.Name).Select(b => b.Name))));

            Mapper.CreateMap<BriefPlanDto, BriefPlan>()
                  .ForMember(d => d.categories, opt => opt.MapFrom(s => s.Categories.Select(p => p.Replace("_", " "))));

            Mapper.CreateMap<PlanDto, Plan>()
                  .ForMember(d => d.categories, opt =>
                  {
                      opt.Condition(s => s.Categories != null && s.Categories.Any());
                      opt.MapFrom(s => s.Categories.Select(p => p.Replace("_", " ")));
                  });
            Mapper.CreateMap<Plan, PlanDto>()
                  .Include<Plan, CreatePlanRequest>()
                  .Include<Plan, UpdatePlanRequest>()
                  .ForMember(d => d.Categories,
                      opt =>
                      {
                          opt.Condition(s => s.categories != null && s.categories.Any());
                          opt.MapFrom(s => s.categories.Select(p => p.Replace(" ", "_")));
                      });
            Mapper.CreateMap<Plan, CreatePlanRequest>()
                  .ForMember(d => d.Categories, opt => opt.MapFrom(s => s.categories.Select(p => p.Replace(" ", "_"))));

            Mapper.CreateMap<Plan, UpdatePlanRequest>()
                  .ForMember(d => d.Categories, opt => opt.MapFrom(s => s.categories.Select(p => p.Replace(" ", "_"))));

            Mapper.CreateMap<Plan, PublishPlanRequest>();
            Mapper.CreateMap<Injury, PublishInjuryRequest>();
            Mapper.CreateMap<Exercise, PublishExerciseRequest>();

            //TODO: this is still ghetto hack... for some reason this inheritance map is not working
            Mapper.CreateMap<BriefExerciseDto, BriefExercise>()
                .ForMember(d => d.categories, opt => opt.MapFrom(s => s.Categories.Select(p => p.Replace("_", " "))));

            Mapper.CreateMap<ExerciseDto, Exercise>()
                  .ForMember(d => d.categories, opt =>
                  {
                      opt.Condition(s => s.Categories != null &&  s.Categories.Any());
                      opt.MapFrom(s => s.Categories.Select(p => p.Replace("_", " ")));
                  });
            Mapper.CreateMap<Exercise, ExerciseDto>()
                  .ForMember(d => d.Categories, opt => opt.MapFrom(s => s.categories.Select(p => p.Replace(" ", "_"))));
            Mapper.CreateMap<Exercise, CreateExerciseRequest>()
                  .ForMember(d => d.Categories, opt => opt.MapFrom(s => s.categories.Select(p => p.Replace(" ", "_"))));
            Mapper.CreateMap<Exercise, UpdateExerciseRequest>()
                  .ForMember(d => d.Categories, opt => opt.MapFrom(s => s.categories.Select(p => p.Replace(" ", "_"))));

            Mapper.CreateMap<PlanExerciseDto, PlanExercise>();
            Mapper.CreateMap<PlanExercise, PlanExerciseDto>()
                  .ForMember(d => d.ExerciseId, opt => opt.MapFrom(s => s.exerciseId))
                  .ForMember(d => d.ExerciseId, opt =>
                      {
                          //This is in place to use planexercise id as id in the admin section when adding exercises to plans 
                          opt.Condition(s => s.exerciseId > 0 );
                          opt.MapFrom(s => s.exerciseId);
                      });
            Mapper.CreateMap<BriefEquipmentDto, BriefEquipment>();
            Mapper.CreateMap<EquipmentDto, Equipment>()
                  .ForMember(d => d.category, opt => opt.MapFrom(s => s.Category.Replace("_", " ")));
            Mapper.CreateMap<Equipment, EquipmentDto>()
                  .Include<Equipment, CreateEquipmentRequest>()
                  .Include<Equipment, UpdateEquipmentRequest>()
                  .ForMember(d => d.Category, opt => opt.MapFrom(s => s.category.Replace(" ", "_")));
            Mapper.CreateMap<Equipment, CreateEquipmentRequest>()
                  .ForMember(d => d.Category, opt => opt.MapFrom(s => s.category.Replace(" ", "_")));
            Mapper.CreateMap<Equipment, UpdateEquipmentRequest>()
                  .ForMember(d => d.Category, opt => opt.MapFrom(s => s.category.Replace(" ", "_")));

            Mapper.CreateMap<VideoDto, Video>()
                  .ForMember(d => d.categories, opt => opt.MapFrom(s => s.Categories.Select(p => p.Replace("_", " "))));
            Mapper.CreateMap<Video, VideoDto>()
                  .Include<Video, UpdateVideoRequest>()
                  .Include<Video, CreateVideoRequest>()
                  .ForMember(d => d.Categories, opt => opt.MapFrom(s => s.categories.Select(p => p.Replace(" ", "_"))));
            Mapper.CreateMap<Video, UpdateVideoRequest>()
                  .ForMember(d => d.Categories, opt => opt.MapFrom(s => s.categories.Select(p => p.Replace(" ", "_"))));
            Mapper.CreateMap<Video, CreateVideoRequest>()
                  .ForMember(d => d.Categories, opt => opt.MapFrom(s => s.categories.Select(p => p.Replace(" ", "_"))));

            Mapper.CreateMap<CauseDto, Cause>();
            Mapper.CreateMap<Cause, CauseDto>()
                  .Include<Cause, CreateCauseRequest>()
                  .Include<Cause, UpdateCauseRequest>();
            Mapper.CreateMap<Cause, CreateCauseRequest>()
                  .ForMember(d => d.Filter, opt => opt.MapFrom(s => new Filter() { id = s.filterId }));
            Mapper.CreateMap<Cause, UpdateCauseRequest>()
                  .ForMember(d => d.Filter, opt => opt.MapFrom(s => new Filter() { id = s.filterId }));

            Mapper.CreateMap<SignDto, Sign>();
            //seem to be having issues with this type of inheritance mapping
            //http://stackoverflow.com/questions/14705064/mapping-one-source-class-to-multiple-derived-classes-with-automapper
            Mapper.CreateMap<Sign, SignDto>()
                  .Include<Sign, CreateSignRequest>()
                  .Include<Sign, UpdateSignRequest>();
            Mapper.CreateMap<Sign, CreateSignRequest>()
                  .ForMember(d => d.Filter, opt => opt.MapFrom(s => new Filter() {id = s.filterId}));
            Mapper.CreateMap<Sign, UpdateSignRequest>()
                  .ForMember(d => d.Filter, opt => opt.MapFrom(s => new Filter() {id = s.filterId}));

            Mapper.CreateMap<BodyRegionDto, BodyRegion>();
            Mapper.CreateMap<BodyRegion, BodyRegionDto>()
                  .Include<BodyRegion, CreateBodyRegionRequest>()
                  .Include<BodyRegion, UpdateBodyRegionRequest>();
            Mapper.CreateMap<BodyRegion, CreateBodyRegionRequest>();
            Mapper.CreateMap<BodyRegion, UpdateBodyRegionRequest>();

            Mapper.CreateMap<FavoriteDto, Favorite>();
            Mapper.CreateMap<Favorite, CreateUserFavoriteRequest>();

            Mapper.CreateMap<FilterDto, Filter>();
            Mapper.CreateMap<Filter, FilterDto>();

            Mapper.CreateMap<TreatmentDto, Treatment>();
            Mapper.CreateMap<Treatment, TreatmentDto>();
            Mapper.CreateMap<Treatment, CreateTreatmentRequest>();
            Mapper.CreateMap<Treatment, UpdateTreatmentRequest>();

            Mapper.CreateMap<PrognosisDto, Prognosis>();
            Mapper.CreateMap<Prognosis, PrognosisDto>();
            Mapper.CreateMap<Prognosis, CreatePrognosisRequest>();
            Mapper.CreateMap<Prognosis, UpdatePrognosisRequest>();

            Mapper.CreateMap<Therapist, TherapistDto>();
            Mapper.CreateMap<TherapistDto, Therapist>();
            Mapper.CreateMap<Clinic, ClinicDto>();
            Mapper.CreateMap<ClinicDto, Clinic>();
            Mapper.CreateMap<ClinicExercise, ClinicExerciseDto>();
            Mapper.CreateMap<ClinicExerciseDto, ClinicExercise>();
            Mapper.CreateMap<ClinicPlan, ClinicPlanDto>();
            Mapper.CreateMap<ClinicPlanDto, ClinicPlan>();
            Mapper.CreateMap<CaseDto, Case>();
            Mapper.CreateMap<CaseSnapshotDto, CaseSnapshot>();
            Mapper.CreateMap<PatientSnapshotDto, PatientSnapshot>();
            Mapper.CreateMap<SessionDto, Session>();

            Mapper.CreateMap<Session, CreateSessionRequest>();
            Mapper.CreateMap<Session, UpdateSessionRequest>()
                                  .ForMember(d => d.Diagnosis, opt => opt.Ignore());
            Mapper.CreateMap<Case, CreateCaseRequest>();

            Mapper.CreateMap<ClinicPatientDto, ClinicPatient>();

            Mapper.CreateMap<ClinicTherapistDto, ClinicTherapist>();


            Mapper.CreateMap<SessionPayDto, SessionPay>();

            Mapper.CreateMap<SportsWebUser, User>()
                .ForMember(d => d.fullName, opt => opt.MapFrom(s => s.GetClaimValue("name")))
                .ForMember(d => d.emailAddress, opt => opt.MapFrom(s => s.Email));

        }
    }
}
