using System;
using System.Collections.Generic;

using AutoMapper;
using SportsWebPt.Common.ServiceStack;
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
            var request = GetSync(new SkeletonAreaListRequest());

            return request.Response == null ? null : Mapper.Map<IEnumerable<SkeletonArea>>(request.Response.Items);
        }

        public IEnumerable<SymptomaticRegion> GetSymptomaticRegions()
        {
            var request = GetSync (new SymptomaticRegionListRequest());

            return request.Response == null ? null : Mapper.Map<IEnumerable<SymptomaticRegion>>(request.Response.Items);
        }

        public IEnumerable<PotentialSymptom> GetPotentialSymptoms(int bodyPartMatrixId)
        {
            var request = GetSync(new PotentialSymptomListRequest() {BodyPartMatrixId = bodyPartMatrixId});

            return request.Response == null ? null : Mapper.Map<IEnumerable<PotentialSymptom>>(request.Response.Items);
        }

        public int SubmitDifferentialDiagnosis(DifferentialDiagnosis differentialDiagnosis)
        {
            var response = PostSync(Mapper.Map<CreateDiagnosisReportRequest>(differentialDiagnosis));

            return response.Response.Id;
        }

        public DiagnosisReport GetDiagnosisReport(int differntialDiagnosisId)
        {
            var request = GetSync(new DiagnosisReportRequest() { Id = differntialDiagnosisId.ToString()});

            return Mapper.Map<DiagnosisReport>(request.Response);
        }
    }
}
