using System;
using System.Linq;
using SportsWebPt.Common.ServiceStack;
using SportsWebPt.Platform.DataAccess;
using SportsWebPt.Platform.ServiceModels;

namespace SportsWebPt.Platform.ServiceImpl
{
    public class PageNameValidationService : RestService
    {
        #region Properties

        public IResearchUnitOfWork ResearchUnitOfWork { get; set; }

        #endregion

        #region Methods

        public object Get(PageNameValidationRequest request)
        {
            if (String.IsNullOrEmpty(request.PageName))
                return true;

            if (ResearchUnitOfWork.ExerciseRepo.GetAll().Any(p => p.PageName.Equals(request.PageName, StringComparison.OrdinalIgnoreCase)))
                return false;

            if (ResearchUnitOfWork.InjuryRepo.GetAll().Any(p => p.PageName.Equals(request.PageName, StringComparison.OrdinalIgnoreCase)))
                return false;

            return !ResearchUnitOfWork.PlanRepo.GetAll().Any(p => p.PageName.Equals(request.PageName, StringComparison.OrdinalIgnoreCase));
        }

        #endregion
    }
}
