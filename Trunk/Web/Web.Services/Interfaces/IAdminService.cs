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

        int AddExercise(Exercise exercise);

        void UpdateExercise(Exercise exercise);

        IEnumerable<GridPlan> GetClinicPlans();
        IEnumerable<GridInjury> GetClinicInjuries();
        IEnumerable<GridExercise> GetClinicExercises();

        int AddPlan(Plan plan);

        void PublishPlan(Plan plan);
        void PublishInjury(Injury injury);
        void PublishExercise(Exercise exercise);

        void UpdatePlan(Plan plan);

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

        IEnumerable<BodyPart> GetBodyParts();

        IEnumerable<SkeletonArea> GetSkeletonAreas();

        int AddBodyPart(BodyPart bodyPart);

        void UpdateBodyPart(BodyPart bodyPart);

        IEnumerable<BodyRegion> GetBodyRegions();

        int AddBodyRegion(BodyRegion bodyRegion);

        void UpdateBodyRegion(BodyRegion bodyRegion);

        bool ValidatePageName(string pageName);

        IEnumerable<Treatment> GetTreatments();

        int AddTreatment(Treatment treatment);

        void UpdateTreatment(Treatment treatment);

        IEnumerable<Prognosis> GetPrognoses();

        int AddPrognosis(Prognosis prognosis);

        void UpdatePrognosis(Prognosis prognosis);

    }
}
