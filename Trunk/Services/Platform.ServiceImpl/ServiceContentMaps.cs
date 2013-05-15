using System;
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
            Mapper.CreateMap<SymptomDetail, PotentialSymptomDto>();
            Mapper.CreateMap<PotentialSymptomDto, SymptomDetail>();
            Mapper.CreateMap<Injury, InjuryDto>();

        }
    }
}
