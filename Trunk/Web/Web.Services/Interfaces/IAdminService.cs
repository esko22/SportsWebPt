using System.Collections.Generic;

using SportsWebPt.Platform.Web.Core;

namespace SportsWebPt.Platform.Web.Services
{
    public interface IAdminService
    {
        IEnumerable<Equipment> GetEquipment();

        int AddEquipment(Equipment equipment);

        void UpdateEquipment(Equipment equipment);

        IEnumerable<Video> GetVideos();

        int AddVideo(Video video);

        void UpdateVideo(Video video);

        IEnumerable<Exercise> GetExercises();

        int AddExercise(Exercise exercise);

        void UpdateExercise(Exercise exercise);

        IEnumerable<Plan> GetPlans();

        int AddPlan(Plan plan);

        void UpdatePlan(Plan plan);

        IEnumerable<Injury> GetInjuries();

        int AddInjury(Injury injury);

        void UpdateInjury(Injury injury);

        IEnumerable<Sign> GetSigns();

        int AddSign(Sign sign);

        void UpdateSign(Sign sign);

        IEnumerable<Cause> GetCauses();

        int AddCause(Cause cause);

        void UpdateCause(Cause cause);

        IEnumerable<BodyPartMatrixItem> GetBodyPartMatrix();

        IEnumerable<Symptom> GetSymtpoms();


    }
}
