using System;
using System.Collections.Generic;

namespace SportsWebPt.Platform.Core.Models
{
    public class Injury
    {
        #region Properties

        public int Id { get; set; }

        public String CommonName { get; set; }

        public String MedicalName { get; set; }

        public String Description { get; set; }

        public String Prognosis { get; set; }

        public String Recovery { get; set; }

        public String AnimationTag { get; set; }

        public InjurySeverity Severity { get; set; }

        #endregion

        #region Navigation Properties

        public ICollection<InjurySymptomMatrixItem> InjurySymptomMatrixItems { get; set; }

        public ICollection<InjuryPlanMatrixItem> InjuryPlanMatrixItems { get; set; }

        public ICollection<InjuryCauseMatrixItem> InjuryCauseMatrixItems { get; set; }

        public ICollection<InjurySignMatrixItem> InjurySignMatrixItems { get; set; }

        public ICollection<InjuryBodyRegionMatrixItem> InjuryBodyRegionMatrixItems { get; set; }

        public ICollection<InjuryTreatmentMatrixItem> InjuryTreatmentMatrixItems { get; set; }

        public ICollection<InjuryPrognosisMatrixItem> InjuryPrognosisMatrixItems { get; set; }

        public ICollection<User> Users { get; set; }

        public InjuryPublishDetail PublishDetail { get; set; }

        #endregion

    }

    public class InjuryPublishDetail
    {
        #region Properties

        public int Id { get; set; }

        public String PageName { get; set; }

        public String Tags { get; set; }

        public String OpeningStatement { get; set; }

        #endregion   

        #region Navigation Properties

        public virtual Injury Injury { get; set; }

        #endregion

    }
}
