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
    }
}
