using System.Collections.Generic;
using System.Linq;
using System;

using AutoMapper;

using SportsWebPt.Common.ServiceStack;
using SportsWebPt.Common.Utilities;
using SportsWebPt.Platform.Core;
using SportsWebPt.Platform.Core.Models;
using SportsWebPt.Platform.DataAccess;
using SportsWebPt.Platform.ServiceModels;

namespace SportsWebPt.Platform.ServiceImpl
{
    public class DiagnosisReportService : RestService
    {
        #region Properties

        public IDiffDiagUnitOfWork DiffDiagUnitOfWork { get; set; }

        #endregion

        #region Methods

        public object Get(DiagnosisReportRequest request)
        {
            var differentialDiagEntity = DiffDiagUnitOfWork.GetDiffDiagWithDetails(request.IdAsInt);
            var diagnosisReportDto = Mapper.Map<DiagnosisReportDto>(differentialDiagEntity);

            var potentialInjuryDtos = new List<PotentialInjuryDto>();
            var potentialInjuries =
                DiffDiagUnitOfWork.GetPotentialInjuries(
                    differentialDiagEntity.SymptomDetails.Select(s => s.SymptomMatrixItemId), PlatformServiceConfiguration.Instance.ClinicId);

            //TODO: MONEY MAKER, can use some love but it works for now...

            foreach (var potentialInjury in potentialInjuries)
            {
                var symptomCount = potentialInjury.InjurySymptomMatrixItems.Count;
                var matchedSymptoms = new List<SymptomDetail>();
                var hasRedFlag = false;

                foreach (var symptomMatrixItem in potentialInjury.InjurySymptomMatrixItems)
                {
                    var symptom = symptomMatrixItem.SymptomMatrixItem.Symptom;

                    var givenSymptom =
                        differentialDiagEntity.SymptomDetails.SingleOrDefault(
                            p => p.SymptomMatrixItemId == symptomMatrixItem.SymptomMatrixItemId);

                    if(givenSymptom == null)
                        continue;

                    switch (symptom.ResponseType)
                    {
                        case SymptomResponseType.Exact:
                            if (Convert.ToInt32(givenSymptom.GivenResponse) == Convert.ToInt32(symptomMatrixItem.ComparisonValue))
                            {
                                matchedSymptoms.Add(givenSymptom);
                                if (symptomMatrixItem.IsRedFlag)
                                    hasRedFlag = true;
                            }
                            break;
                        case SymptomResponseType.EqualAndBelowThreshold:
                            if (Convert.ToInt32(givenSymptom.GivenResponse) <= Convert.ToInt32(symptomMatrixItem.ComparisonValue))
                            {
                                matchedSymptoms.Add(givenSymptom);
                                if (symptomMatrixItem.IsRedFlag)
                                    hasRedFlag = true;
                            }
                            break;
                        case SymptomResponseType.EqualAndAboveThreshold:
                            if (Convert.ToInt32(givenSymptom.GivenResponse) >= Convert.ToInt32(symptomMatrixItem.ComparisonValue))
                            {
                                matchedSymptoms.Add(givenSymptom);
                                if (symptomMatrixItem.IsRedFlag)
                                    hasRedFlag = true;
                            }
                            break;
                        case SymptomResponseType.BelowThreshold:
                            if (Convert.ToInt32(givenSymptom.GivenResponse) < Convert.ToInt32(symptomMatrixItem.ComparisonValue))
                            {
                                matchedSymptoms.Add(givenSymptom);
                                if (symptomMatrixItem.IsRedFlag)
                                    hasRedFlag = true;
                            }
                            break;
                        case SymptomResponseType.AboveThreshold:
                            if (Convert.ToInt32(givenSymptom.GivenResponse) > Convert.ToInt32(symptomMatrixItem.ComparisonValue))
                            {
                                matchedSymptoms.Add(givenSymptom);
                                if (symptomMatrixItem.IsRedFlag)
                                    hasRedFlag = true;
                            }
                            break;
                        case SymptomResponseType.Any:
                            var anyValues = symptomMatrixItem.ComparisonValue.Split(',');
                            if (givenSymptom.GivenResponse.Split(',').Any(anyValues.Contains))
                            {
                                matchedSymptoms.Add(givenSymptom);
                                if (symptomMatrixItem.IsRedFlag)
                                    hasRedFlag = true;
                            }
                            break;
                        case SymptomResponseType.All:
                            var allValues = symptomMatrixItem.ComparisonValue.Split(',');
                            var hasAllValues = givenSymptom.GivenResponse.Split(',').All(allValues.Contains);

                            if (hasAllValues){
                                matchedSymptoms.Add(givenSymptom);
                                if (symptomMatrixItem.IsRedFlag)
                                    hasRedFlag = true;
                            }

                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }

                var potentialInjuryDto = Mapper.Map<PotentialInjuryDto>(potentialInjury);
                potentialInjuryDto.Likelyhood = hasRedFlag ? 1.0 : matchedSymptoms.Count / (double)symptomCount;
                var givenSymptoms = new List<PotentialSymptomDto>();
                Mapper.Map(matchedSymptoms, givenSymptoms);
                potentialInjuryDto.GivenSymptoms = givenSymptoms.ToArray();
                potentialInjuryDtos.Add(potentialInjuryDto);
            }

            if (potentialInjuryDtos.Count > 0)
                diagnosisReportDto.PotentialInjuries = potentialInjuryDtos.ToArray();

            return Ok(new ApiResponse<DiagnosisReportDto>()
            {
                Response = diagnosisReportDto
            });
        }

        public object Post(CreateDiagnosisReportRequest request)
        {
            var differentialDiagEntity = Mapper.Map<DifferentialDiagnosis>(request);
            DiffDiagUnitOfWork.AddDifferentialDiagnosis(differentialDiagEntity, request.SessionId);
            DiffDiagUnitOfWork.Commit();

            return Ok(new ApiResponse<DifferentialDiagnosisDto>()
            {
                Response = Mapper.Map<DifferentialDiagnosisDto>(differentialDiagEntity)
            });
        }


        #endregion

    }
}
