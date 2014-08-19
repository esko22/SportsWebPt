using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsWebPt.Platform.ServiceModels
{
    public class BriefEpisodeDto
    {
        #region Properties

        public int Id { get; set; }

        public EpisodeStateDto State { get; set; }

        public DateTime CreatedOn { get; set; }

        public int PrognosisId { get; set; }

        public int ClinicPatientId { get; set; }

        public int ClinicTherapistId { get; set; }

        #endregion

    }


    public class EpisodeDto : BriefEpisodeDto
    {
        #region Properties

        public PrognosisDto Prognosis { get; set; }

        public UserDto Patient { get; set; }

        public TherapistDto Therapist { get; set; }

        #endregion
    }

    public enum EpisodeStateDto
    {
        Pending = 1,
        Active = 2,
        InActive = 3,
        ReferredOn = 4,
        Recovered = 5
    }
}
