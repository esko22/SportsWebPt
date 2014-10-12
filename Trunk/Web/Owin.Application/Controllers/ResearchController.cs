using System;
using System.Collections.Generic;
using System.Web.Http;

using SportsWebPt.Common.Utilities;
using SportsWebPt.Platform.Web.Core;
using SportsWebPt.Platform.Web.Services;
using YelpSharp;
using YelpSharp.Data;

namespace SportsWebPt.Platform.Web.Application
{
    public class ResearchController : ApiController
    {

        #region Fields

        private readonly IResearchService _researchService; 

        #endregion

        #region Construction

        public ResearchController(IResearchService researchService)
        {
            Check.Argument.IsNotNull(researchService, "Examine Service");
            _researchService = researchService;
        }

        #endregion

        #region Methods

        [HttpGet]
        [Route("data/research/injury/detail/{pageName}")]
        public Injury InjuryDetail(String pageName)
        {
            Check.Argument.IsNotNullOrEmpty(pageName, "pageName");

            return _researchService.GetInjuryByPageName(pageName);
        }

        [HttpGet]
        [Route("data/research/plan/detail/{pageName}")]
        public Plan PlanDetail(String pageName)
        {
            Check.Argument.IsNotNullOrEmpty(pageName, "pageName");

            return _researchService.GetPlanByPageName(pageName);
        }

        [HttpGet]
        [Route("data/research/exercise/detail/{pageName}")]
        public Exercise ExerciseDetail(String pageName)
        {
            Check.Argument.IsNotNullOrEmpty(pageName, "pageName");

            return _researchService.GetExerciseByPageName(pageName);
        }

        [HttpGet]
        [Route("data/research/bodyparts")]
        public IEnumerable<BodyPart> GetBodyParts()
        {
            return _researchService.GetBodyParts();
        }

        [HttpGet]
        [Route("data/research/bodyregions")]
        public IEnumerable<BodyRegion> GetBodyRegions()
        {
            return _researchService.GetBodyRegions();
        }

        [HttpGet]
        [Route("data/research/signs")]
        public IEnumerable<Sign> GetSigns()
        {
            return _researchService.GetSigns();
        }

        [HttpGet]
        [Route("data/exercises/brief")]
        public IEnumerable<BriefExercise> GetBriefExercises()
        {
            return _researchService.GetResearchExercises();
        }

        [HttpGet]
        [Route("data/plans/brief")]
        public IEnumerable<BriefPlan> GetBriefPlans()
        {
            return _researchService.GetResearchPlans();
        }


        [HttpGet]
        [Route("data/injuries/brief")]
        public IEnumerable<BriefInjury> GetBriefInjuries()
        {
            return _researchService.GetResearchInjuries();
        }

        [HttpGet]
        [Route("data/research/locate/{zipcode}")]
        public IEnumerable<Business> GetTherapyByLocation(String zipcode)
        {
            var yelpProxy = new Yelp(WebPlatformConfigSettings.Instance.YelpOptions);
            var results = yelpProxy.Search(WebPlatformConfigSettings.Instance.YelpSearchTerm, zipcode).Result;

            return results.businesses;
        }

        [HttpGet]
        [Route("data/research/equipment")]
        public IEnumerable<Equipment> GetEquipment()
        {
            return _researchService.GetEquipment();
        }

        [HttpGet]
        [Route("data/research/equipment/brief")]
        public IEnumerable<BriefEquipment> GetBriefEquipment()
        {
            return _researchService.GetBriefEquipment();
        }

        #endregion

    }
}
