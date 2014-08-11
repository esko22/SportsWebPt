using System;
using System.Data.Entity;
using System.Linq;

using SportsWebPt.Common.Utilities;
using SportsWebPt.Common.DataAccess;
using SportsWebPt.Common.DataAccess.Ef;
using SportsWebPt.Platform.Core.Models;

namespace SportsWebPt.Platform.DataAccess
{
    public class ResearchUnitOfWork : BaseUnitOfWork, IResearchUnitOfWork
    {
        #region Properties

        public IRepository<Equipment> EquipmentRepo { get { return GetStandardRepo<Equipment>(); } }
        public IVideoRepo VideoRepo { get { return GetRepo<IVideoRepo>(); } }
        public IRepository<Sign> SignRepo { get { return GetStandardRepo<Sign>(); } }
        public IRepository<Cause> CauseRepo { get { return GetStandardRepo<Cause>(); } }
        public IInjuryRepo InjuryRepo { get { return GetRepo<IInjuryRepo>(); } }
        public IRepository<ClinicPlanMatrixItem> ClinicPlanRepo { get { return GetStandardRepo<ClinicPlanMatrixItem>(); } }
        public IRepository<ClinicExerciseMatrixItem> ClinicExerciseRepo { get { return GetStandardRepo<ClinicExerciseMatrixItem>(); } }
        public IRepository<ClinicInjuryMatrixItem> ClinicInjuryRepo { get { return GetStandardRepo<ClinicInjuryMatrixItem>(); } }
        public IPlanRepo PlanRepo { get { return GetRepo<IPlanRepo>(); } }
        public IRepository<InjuryBodyRegionMatrixItem> InjuryBodyRegionMatrixRepo { get { return GetStandardRepo<InjuryBodyRegionMatrixItem>(); } }
        public IRepository<InjuryCauseMatrixItem> InjuryCauseMatrixRepo { get { return GetStandardRepo<InjuryCauseMatrixItem>(); } }
        public IRepository<InjurySignMatrixItem> InjurySignMatrixRepo { get { return GetStandardRepo<InjurySignMatrixItem>(); } }
        public IRepository<InjuryPlanMatrixItem> InjuryPlanMatrixRepo { get { return GetStandardRepo<InjuryPlanMatrixItem>(); } }
        public IRepository<InjurySymptomMatrixItem> InjurySymptomMatrixRepo { get { return GetStandardRepo<InjurySymptomMatrixItem>(); } }
        public IRepository<SymptomMatrixItem> SymptomMatrixRepo { get { return GetStandardRepo<SymptomMatrixItem>(); } }
        public IRepository<VideoCategoryMatrixItem> VideoCategoryRepo { get { return GetStandardRepo<VideoCategoryMatrixItem>(); } }
        public IRepository<Treatment> TreatmentRepo { get { return GetStandardRepo<Treatment>(); } }
        public IRepository<Prognosis> PrognosisRepo { get { return GetStandardRepo<Prognosis>(); } }
        public IRepository<InjuryPrognosisMatrixItem> InjuryPrognosisMatrixRepo { get { return GetStandardRepo<InjuryPrognosisMatrixItem>(); } }
        public IRepository<InjuryTreatmentMatrixItem> InjuryTreatmentMatrixRepo { get { return GetStandardRepo<InjuryTreatmentMatrixItem>(); } }
        public IRepository<InjuryPublishDetail> InjuryPublishDetailRepo { get { return GetStandardRepo<InjuryPublishDetail>(); } }

        #endregion

        #region Construction

        public ResearchUnitOfWork(IRepositoryProvider repositoryProvider)
            :base(repositoryProvider, new PlatformDbContext())
        {}

        #endregion

        #region Methods

        public IQueryable<Sign> GetSigns()
        {
            return SignRepo.GetAll()
                .Include(i => i.Filter);
        }

        public IQueryable<Cause> GetCauses()
        {
            return CauseRepo.GetAll()
                .Include(i => i.Filter);
        }

        public void MapSymptomMatrixItems(Injury injury)
        {
            foreach (var injurySymptom in injury.InjurySymptomMatrixItems)
            {
                var symptomMatrixItem =
                    SymptomMatrixRepo.GetAll()
                                     .FirstOrDefault(
                                         s =>
                                         s.SymptomId == injurySymptom.SymptomMatrixItem.SymptomId &&
                                         s.BodyPartMatrixItemId == injurySymptom.SymptomMatrixItem.BodyPartMatrixItemId);

                if (symptomMatrixItem != null)
                    injurySymptom.SymptomMatrixItem = symptomMatrixItem;
            }

            Commit();
        }

        public void UpdateVideo(Video video)
        {
            var videoInDb =
                VideoRepo.GetAll()
                            .SingleOrDefault(p => p.Id == video.Id);

            if (videoInDb == null)
                throw new ArgumentNullException("video id", "video id does not exist");

            var deletedCategories = videoInDb.VideoCategoryMatrixItems.Except(
                video.VideoCategoryMatrixItems, (item, matrixItem) => item.Category == matrixItem.Category).ToList();

            var addedCategories = video.VideoCategoryMatrixItems.Except(
                videoInDb.VideoCategoryMatrixItems, (item, matrixItem) => item.Category == matrixItem.Category).ToList();

            deletedCategories.ForEach(e => VideoCategoryRepo.Delete(e));
            addedCategories.ForEach(e =>
            {
                e.VideoId = video.Id;
                VideoCategoryRepo.Add(e);
            });

            var videoEntry = _context.Entry(videoInDb);
            videoEntry.CurrentValues.SetValues(video);

            Commit();
        }
        
        public void UpdateInjury(Injury injury)
        {
            var injuryInDb =
                        InjuryRepo.GetInjuryDetails()
                .SingleOrDefault(p => p.Id == injury.Id);

            if (injuryInDb == null)
                throw new ArgumentNullException("injury id", "injury does not exist");

            var deletedPlans = injuryInDb.InjuryPlanMatrixItems.Except(
                injury.InjuryPlanMatrixItems, (item, matrixItem) => item.PlanId == matrixItem.PlanId).ToList();

            var addedPlans = injury.InjuryPlanMatrixItems.Except(
                injuryInDb.InjuryPlanMatrixItems, (item, matrixItem) => item.PlanId == matrixItem.PlanId).ToList();

            deletedPlans.ForEach(e => InjuryPlanMatrixRepo.Delete(e));
            addedPlans.ForEach(e =>
            {
                e.InjuryId = injury.Id;
                InjuryPlanMatrixRepo.Add(e);
            });

            var deletedSigns = injuryInDb.InjurySignMatrixItems.Except(
                injury.InjurySignMatrixItems, (item, matrixItem) => item.SignId == matrixItem.SignId).ToList();

            var addedSigns = injury.InjurySignMatrixItems.Except(
                injuryInDb.InjurySignMatrixItems, (item, matrixItem) => item.SignId == matrixItem.SignId).ToList();

            deletedSigns.ForEach(e => InjurySignMatrixRepo.Delete(e));
            addedSigns.ForEach(e =>
            {
                e.InjuryId = injury.Id;
                InjurySignMatrixRepo.Add(e);
            });

            var deletedBodyRegions = injuryInDb.InjuryBodyRegionMatrixItems.Except(
                injury.InjuryBodyRegionMatrixItems, (item, matrixItem) => item.BodyRegionId == matrixItem.BodyRegionId).ToList();

            var addedBodyRegions = injury.InjuryBodyRegionMatrixItems.Except(
                injuryInDb.InjuryBodyRegionMatrixItems, (item, matrixItem) => item.BodyRegionId == matrixItem.BodyRegionId).ToList();

            deletedBodyRegions.ForEach(e => InjuryBodyRegionMatrixRepo.Delete(e));
            addedBodyRegions.ForEach(e =>
            {
                e.InjuryId = injury.Id;
                InjuryBodyRegionMatrixRepo.Add(e);
            });

            var deletedCauses = injuryInDb.InjuryCauseMatrixItems.Except(
                injury.InjuryCauseMatrixItems, (item, matrixItem) => item.CauseId == matrixItem.CauseId).ToList();

            var addedCauses = injury.InjuryCauseMatrixItems.Except(
                injuryInDb.InjuryCauseMatrixItems, (item, matrixItem) => item.CauseId == matrixItem.CauseId).ToList();

            deletedCauses.ForEach(e => InjuryCauseMatrixRepo.Delete(e));
            addedCauses.ForEach(e =>
            {
                e.InjuryId = injury.Id;
                InjuryCauseMatrixRepo.Add(e);
            });


            var deletedTreatments = injuryInDb.InjuryTreatmentMatrixItems.Except(
                injury.InjuryTreatmentMatrixItems, (item, matrixItem) => item.TreatmentId == matrixItem.TreatmentId).ToList();

            var addedTreatments = injury.InjuryTreatmentMatrixItems.Except(
                injuryInDb.InjuryTreatmentMatrixItems, (item, matrixItem) => item.TreatmentId == matrixItem.TreatmentId).ToList();

            deletedTreatments.ForEach(e => InjuryTreatmentMatrixRepo.Delete(e));
            addedTreatments.ForEach(e =>
            {
                e.InjuryId = injury.Id;
                InjuryTreatmentMatrixRepo.Add(e);
            });



            var deletedPrognoses = injuryInDb.InjuryPrognosisMatrixItems.Except(
                injury.InjuryPrognosisMatrixItems, (item, matrixItem) => item.PrognosisId == matrixItem.PrognosisId).ToList();

            var addedPrognoses = injury.InjuryPrognosisMatrixItems.Except(
                injuryInDb.InjuryPrognosisMatrixItems, (item, matrixItem) => item.PrognosisId == matrixItem.PrognosisId).ToList();

            deletedPrognoses.ForEach(e => InjuryPrognosisMatrixRepo.Delete(e));
            addedPrognoses.ForEach(e =>
            {
                e.InjuryId = injury.Id;
                InjuryPrognosisMatrixRepo.Add(e);
            });


            var deletedSymptoms = injuryInDb.InjurySymptomMatrixItems.Except(
                injury.InjurySymptomMatrixItems, (item, matrixItem) => item.Id == matrixItem.Id).ToList();

            var addedSymptoms = injury.InjurySymptomMatrixItems.Except(
                injuryInDb.InjurySymptomMatrixItems, (item, matrixItem) => item.Id == matrixItem.Id).ToList();

            deletedSymptoms.ForEach(e => InjurySymptomMatrixRepo.Delete(e));
            deletedSymptoms.ForEach(s => SymptomMatrixRepo.GetById(s.SymptomMatrixItemId).Decom = true);

            addedSymptoms.ForEach(e =>
                {
                    var previousSymptom =
                        SymptomMatrixRepo.GetAll()
                                         .FirstOrDefault(
                                             p =>
                                             p.BodyPartMatrixItemId == e.SymptomMatrixItem.BodyPartMatrixItemId &&
                                             p.SymptomId == e.SymptomMatrixItem.SymptomId);

                    if (previousSymptom != null)
                    {
                        previousSymptom.Decom = false;
                        e.SymptomMatrixItem = null;
                        e.SymptomMatrixItemId = previousSymptom.Id;
                    }

                    e.InjuryId = injury.Id;
                    InjurySymptomMatrixRepo.Add(e);

                });

            var injuryEntry = _context.Entry(injuryInDb);
            injuryEntry.CurrentValues.SetValues(injury);

            Commit();
        }

        #endregion
    }

    public interface IResearchUnitOfWork : IBaseUnitOfWork
    {
        //this needs to get broken down...
        IRepository<Equipment> EquipmentRepo { get; }
        IVideoRepo VideoRepo { get; }
        IRepository<InjuryBodyRegionMatrixItem> InjuryBodyRegionMatrixRepo { get; }
        IRepository<InjuryCauseMatrixItem> InjuryCauseMatrixRepo { get; }
        IRepository<InjurySignMatrixItem> InjurySignMatrixRepo { get; }
        IRepository<InjuryPlanMatrixItem> InjuryPlanMatrixRepo { get; }
        IRepository<Sign> SignRepo { get; }
        IRepository<Cause> CauseRepo { get; }
        IInjuryRepo InjuryRepo { get; }
        IRepository<ClinicPlanMatrixItem> ClinicPlanRepo { get; }
        IRepository<ClinicExerciseMatrixItem> ClinicExerciseRepo { get; }
        IRepository<ClinicInjuryMatrixItem> ClinicInjuryRepo { get; }
        IPlanRepo PlanRepo { get; }
        IRepository<VideoCategoryMatrixItem> VideoCategoryRepo { get; }
        IRepository<Treatment> TreatmentRepo { get; }
        IRepository<Prognosis> PrognosisRepo { get; }
        IRepository<InjuryPublishDetail> InjuryPublishDetailRepo { get; }

        void UpdateInjury(Injury injury);
        void UpdateVideo(Video video);
        void MapSymptomMatrixItems(Injury injury);

        //getting lazy and tired of creating custom repos..
        IQueryable<Sign> GetSigns();
        IQueryable<Cause> GetCauses();
    }
}
