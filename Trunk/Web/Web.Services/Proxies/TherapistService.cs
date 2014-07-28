using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using SportsWebPt.Common.ServiceStack;
using SportsWebPt.Platform.ServiceModels;
using SportsWebPt.Platform.Web.Core;

namespace SportsWebPt.Platform.Web.Services
{
    public interface ITherapistService
    {
        IEnumerable<BriefExercise> GetExercises(String therapistId);
        IEnumerable<BriefPlan> GetPlans(String therapistId);
    }


    public class TherapistService : BaseServiceStackClient, ITherapistService 
    {
        #region Fields

        private readonly SportsWebPtClientSettings _sportsWebPtClientSettings;

        #endregion

        #region Construction

        public TherapistService(SportsWebPtClientSettings clientSettings)
            : base(clientSettings)
        {
            _sportsWebPtClientSettings = clientSettings;
        }

        #endregion


        #region Methods
        
        public IEnumerable<BriefExercise> GetExercises(String therapistId)
        {
            var request = GetSync(new TherapistExerciseListRequest() { Id = therapistId });

            return request.Response == null ? null : Mapper.Map<IEnumerable<BriefExercise>>(request.Response.Items.OrderBy(p => p.Name));

        }

        public IEnumerable<BriefPlan> GetPlans(String therapistId)
        {
            var request = GetSync(new TherapistPlanListRequest() { Id = therapistId });

            return request.Response == null ? null : Mapper.Map<IEnumerable<BriefPlan>>(request.Response.Items.OrderBy(p => p.RoutineName));
        } 

        #endregion

    }
}
