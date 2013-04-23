using System.Collections.Generic;
using System.Linq;
using AutoMapper;

using SportsWebPt.Common.ServiceStack.Infrastructure;
using SportsWebPt.Platform.Core.Models;
using SportsWebPt.Platform.DataAccess;
using SportsWebPt.Platform.ServiceModels;

namespace SportsWebPt.Platform.ServiceImpl
{
    public class SymptomaticRegionService : LoggingRestServiceBase<SymptomaticRegionListRequest, ListResponse<SymptomaticRegionDto, BasicSortBy>>
    {
        #region Properties

        public ISkeletonUnitOfWork SkeletonUnitOfWork { get; set; }

        #endregion

        #region Methods

        public override object OnGet(SymptomaticRegionListRequest request)
        {
            var areas = SkeletonUnitOfWork.SkeletonAreaRepo.GetSymptonmaticRegions();
            var responseList = new List<SymptomaticRegionDto>();

            //TODO: because I am being lazy and don't want to figure this out in AutoMapper
            MapResponse(areas, responseList);

            return
                Ok(new ListResponse<SymptomaticRegionDto, BasicSortBy>(responseList.ToArray(), responseList.Count, 0, 0,
                                                                        null, null));

        }

        private void MapResponse(IEnumerable<SkeletonArea> areas, ICollection<SymptomaticRegionDto> responseList)
        {
            foreach (var skeletonArea in areas)
            {
                var regionDto = new SymptomaticRegionDto();

                var skeletonAreaDto = new SkeletonAreaDto
                    {
                        id = skeletonArea.Id,
                        orientation = skeletonArea.Orientation.Value,
                        region = skeletonArea.Region.Name,
                        side = skeletonArea.Side.Value
                    };
                regionDto.SkeletonArea = skeletonAreaDto;


                var componentSymptoms = new List<ComponentSymptomDto>();

                foreach (var areaComponent in skeletonArea.Components)
                {
                    var componentSymptomDto = new ComponentSymptomDto();
                    var componentDto = new AreaComponentDto
                        {
                            commonName = areaComponent.CommonName,
                            scientificName = areaComponent.ScientificName,
                            id = areaComponent.Id
                        };

                    componentSymptomDto.Component = componentDto;

                    componentSymptomDto.Symptoms = areaComponent.Symptoms.Select(symptom => new SymptomDto
                        {
                            renderType = symptom.RenderType.ToString(), name = symptom.Name, description = symptom.Description, id = symptom.Id
                        }).ToArray();
                    componentSymptoms.Add(componentSymptomDto);
                }

                regionDto.ComponentSymptoms = componentSymptoms.ToArray();
                responseList.Add(regionDto);
            }
        }

        #endregion
    }
}
