using System;
using System.Collections.Generic;

using AutoMapper;

using SportsWebPt.Common.ServiceStackClient;
using SportsWebPt.Platform.ServiceModels;
using SportsWebPt.Platform.Web.Core;

namespace SportsWebPt.Platform.Web.Services
{
    public class ExamineService : BaseServiceStackClient, IExamineService
    {

        #region Fields

        private readonly SportsWebPtClientSettings _sportsWebPtClientSettings;

        #endregion
        

        #region Construction

        public ExamineService(SportsWebPtClientSettings clientSettings)
            : base(clientSettings)
        {
            _sportsWebPtClientSettings = clientSettings;
        }

        #endregion

        public IEnumerable<SkeletonArea> GetSkeletonAreas()
        {
            var response =
                GetSync<ListResponse<SkeletonAreaDto, SkeletonSortBy>>(_sportsWebPtClientSettings.SkeletonAreasUriPath);

            return response.Resource == null ? null : Mapper.Map<IEnumerable<SkeletonArea>>(response.Resource.Items);
        }

        public IEnumerable<SymptomaticRegion> GetSymptomaticRegions()
        {
            var response =
                GetSync<ListResponse<SymptomaticRegionDto, BasicSortBy>>(_sportsWebPtClientSettings.SymptomaticRegionUriPath);

            return response.Resource == null ? null : Mapper.Map<IEnumerable<SymptomaticRegion>>(response.Resource.Items);
        }

        public IEnumerable<PotentialSymptom> GetPotentialSymptoms(int bodyPartMatrixId)
        {
            var response =
                GetSync<ListResponse<PotentialSymptomDto, BasicSortBy>>(String.Format("{0}?bodyPartMatrixId={1}", _sportsWebPtClientSettings.PotentialSymptomUriPath, bodyPartMatrixId));

            return response.Resource == null ? null : Mapper.Map<IEnumerable<PotentialSymptom>>(response.Resource.Items);
            
        }

        public int SubmitDifferentialDiagnosis(DifferentialDiagnosis differentialDiagnosis)
        {
            var resuest = new ApiResourceRequest<DifferentialDiagnosisDto>
            {
                Resource = Mapper.Map<DifferentialDiagnosisDto>(differentialDiagnosis)
            };

            var response =
                PostSync<ApiResponse<DifferentialDiagnosisDto>>(_sportsWebPtClientSettings.DiffDiagUriPath, resuest);

            return response.Resource.id;
        }

        public DiagnosisReport GetDiagnosisReport(int differntialDiagnosisId)
        {
            var response =
                GetSync<ApiResourceRequest<DiagnosisReportDto>>(String.Format("{0}/{1}", _sportsWebPtClientSettings.DiagnosisReport,
                                                                                          differntialDiagnosisId));

            var diagReport = Mapper.Map<DiagnosisReport>(response.Resource);

            return diagReport;
        }
    }
}
