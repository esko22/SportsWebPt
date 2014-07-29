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
        IEnumerable<GridExercise> GetExercises(String therapistId);
        IEnumerable<GridPlan> GetPlans(String therapistId);
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
        
        public IEnumerable<GridExercise> GetExercises(String therapistId)
        {
            var request = GetSync(new TherapistExerciseListRequest() { Id = therapistId, IsOwner = true});

            return request.Response == null ? null : Mapper.Map<IEnumerable<GridExercise>>(request.Response.Items.OrderBy(p => p.Name));

        }

        public IEnumerable<GridPlan> GetPlans(String therapistId)
        {
            var request = GetSync(new TherapistPlanListRequest() { Id = therapistId, IsOwner = true });

            return request.Response == null ? null : Mapper.Map<IEnumerable<GridPlan>>(request.Response.Items.OrderBy(p => p.RoutineName));
        } 

        #endregion

    }
}
