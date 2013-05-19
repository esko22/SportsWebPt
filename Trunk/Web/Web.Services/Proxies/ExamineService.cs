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

        private readonly String _skeletonAreasUriPath = String.Empty;
        private readonly String _bodyPartUriPath = String.Empty;
        private readonly String _symptomaticRegionUriPath = String.Empty;
        private readonly String _potentialSymptomUriPath = String.Empty;
        private readonly String _diffDiagUriPath = String.Empty;
        private readonly String _diagnosisReport = String.Empty;

        #endregion

        #region Construction

        public ExamineService(BaseServiceStackClientSettings clientSettings)
            : base(clientSettings)
        {
            _skeletonAreasUriPath = String.Format("/{0}/areas", _settings.Version);
            _bodyPartUriPath = String.Format("/{0}/bodyparts", _settings.Version);
            _symptomaticRegionUriPath = String.Format("/{0}/symptomaticregions", _settings.Version);
            _potentialSymptomUriPath = String.Format("/{0}/potentialsymptoms", _settings.Version);
            _diffDiagUriPath = String.Format("/{0}/differentialdiagnosis", _settings.Version);
            _diagnosisReport = String.Format("/{0}/diagnosisreports", _settings.Version);
        }

        #endregion

        public IEnumerable<SkeletonArea> GetSkeletonAreas()
        {
            var response =
                GetSync<ListResponse<SkeletonAreaDto, SkeletonSortBy>>(_skeletonAreasUriPath);

            return response.Resource == null ? null : Mapper.Map<IEnumerable<SkeletonArea>>(response.Resource.Items);
        }


        public IEnumerable<BodyPart> GetBodyParts(int skeletionAreaId)
        {
            var response =
                GetSync<ListResponse<BodyPartDto,BodyPartSortBy>>(String.Format("{0}?areaId={1}",_bodyPartUriPath,skeletionAreaId));

            return response.Resource == null ? null : Mapper.Map<IEnumerable<BodyPart>>(response.Resource.Items);
        }


        public IEnumerable<SymptomaticRegion> GetSymptomaticRegions()
        {
            var response =
                GetSync<ListResponse<SymptomaticRegionDto, BasicSortBy>>(_symptomaticRegionUriPath);

            return response.Resource == null ? null : Mapper.Map<IEnumerable<SymptomaticRegion>>(response.Resource.Items);
        }

        public IEnumerable<PotentialSymptom> GetPotentialSymptoms(int bodyPartMatrixId)
        {
            var response =
                GetSync<ListResponse<PotentialSymptomDto, BasicSortBy>>(String.Format("{0}?bodyPartMatrixId={1}", _potentialSymptomUriPath, bodyPartMatrixId));

            return response.Resource == null ? null : Mapper.Map<IEnumerable<PotentialSymptom>>(response.Resource.Items);
            
        }

        public int SubmitDifferentialDiagnosis(DifferentialDiagnosis differentialDiagnosis)
        {
            var resuest = new ApiResourceRequest<DifferentialDiagnosisDto>
            {
                Resource = Mapper.Map<DifferentialDiagnosisDto>(differentialDiagnosis)
            };

            var response =
                PostSync<ApiResponse<DifferentialDiagnosisDto>>(_diffDiagUriPath, resuest);

            return response.Resource.id;
        }

        public DiagnosisReport GetDiagnosisReport(int differntialDiagnosisId)
        {
            var response =
                GetSync<ApiResourceRequest<DiagnosisReportDto>>(String.Format("{0}/{1}", _diagnosisReport,
                                                                                          differntialDiagnosisId));

            var diagReport = Mapper.Map<DiagnosisReport>(response.Resource);

            return diagReport;
        }
    }
}
