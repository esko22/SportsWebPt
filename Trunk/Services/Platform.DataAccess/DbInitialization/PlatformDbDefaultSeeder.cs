﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;

using SportsWebPt.Common.DataAccess.Ef;
using SportsWebPt.Common.Utilities;
using SportsWebPt.Common.Utilities.Security;
using SportsWebPt.Platform.Core.Models;

namespace SportsWebPt.Platform.DataAccess
{
    public class PlatformDbDefaultSeeder : ISeed<DbContext>
    {
        #region Fields

        [ThreadStatic] 
        private static PlatformDbContext _dbContext;

        #endregion

        public void Seed(DbContext dbContext)
        {
            Check.Argument.IsNotNull(dbContext, "DbConext");

            if(!(dbContext is PlatformDbContext))
                throw new ArgumentException("Invalid DbConextType");

            _dbContext = dbContext as PlatformDbContext;

            var components = AddComponents();
            var regions = AddRegions();
            var orientations = AddOrientations();
            var sides = AddSides();
            var skeletonareas = AddSkeletonAreas(regions, sides, orientations);
            var renderTypes = AddRenderTypes();
            var symptoms = AddSymptoms(renderTypes);
            var treatments = AddTreatments();
            var bodyPartMartix = AddBodyPartsToAreas(skeletonareas,components);
            var symptomMatrixItems = BuildSymptomMatrix(symptoms, bodyPartMartix);
            var injuries = AddInjuries();
            var equipment = AddEquipment();
            var videos = AddVideos();
            var exercises = AddExercises();
            var filters = AddFilterCategories();
            var causes = AddCauses(filters);
            var signs = AddSigns(filters);
            var plans = AddPlans();
            var vendors = AddVendors();
            var prognoses = AddPrognosis();

            BuildInjurySymptomMatrix(symptomMatrixItems,injuries);
            AssociateInjuryCause(causes, injuries);
            AssociateInjurySigns(signs, injuries);
            AssociateInjuryPlans(injuries, plans);
            AssociateExerciseCategories(exercises);
            AssociatePlanExercise(plans, exercises);
            AssociateExerciseVideos(exercises, videos);
            AssociateExerciseEquipment(exercises, equipment);
            AssociateExerciseBodyRegion(exercises, regions);
            AssociatePlanBodyRegion(plans, regions);
            AssociateInjuryBodyRegion(injuries, regions);
            AssociateBodyPartsToExercise(components,exercises);
            AssociateEquipmentToVendor(equipment,vendors);
            AssociatePlanCategories(plans);
            AssociateVideoCategory(videos);
            AssociateInjuryTreatment(treatments,injuries);
            AssociateInjuryPrognosis(prognoses, injuries);
        }

        private void AssociateInjuryTreatment(IList<Treatment> treatments, IList<Injury> injuries)
        {
            var injuryTreatments = new List<InjuryTreatmentMatrixItem>()
                {
                    new InjuryTreatmentMatrixItem() {Treatment = treatments[0], Injury = injuries[0]},
                    new InjuryTreatmentMatrixItem() {Treatment = treatments[0], Injury = injuries[1]},
                    new InjuryTreatmentMatrixItem() {Treatment = treatments[0], Injury = injuries[2]},
                    new InjuryTreatmentMatrixItem() {Treatment = treatments[0], Injury = injuries[3]},
                    new InjuryTreatmentMatrixItem() {Treatment = treatments[1], Injury = injuries[3]},
                    new InjuryTreatmentMatrixItem() {Treatment = treatments[2], Injury = injuries[1]},
                    new InjuryTreatmentMatrixItem() {Treatment = treatments[3], Injury = injuries[0]},
                    new InjuryTreatmentMatrixItem() {Treatment = treatments[4], Injury = injuries[2]},
                    new InjuryTreatmentMatrixItem() {Treatment = treatments[5], Injury = injuries[2]}
                };

            injuryTreatments.ForEach(p => _dbContext.InjuryTreatmentMatrixItems.Add(p));
            _dbContext.SaveChanges();
            
        }

        private void AssociateInjuryCause(IList<Cause> causes, IList<Injury> injuries)
        {
            var injuryCauses = new List<InjuryCauseMatrixItem>()
                {
                    new InjuryCauseMatrixItem() {Cause = causes[0], Injury = injuries[0]},
                    new InjuryCauseMatrixItem() {Cause = causes[0], Injury = injuries[1]},
                    new InjuryCauseMatrixItem() {Cause = causes[0], Injury = injuries[2]},
                    new InjuryCauseMatrixItem() {Cause = causes[0], Injury = injuries[3]},
                    new InjuryCauseMatrixItem() {Cause = causes[1], Injury = injuries[3]},
                    new InjuryCauseMatrixItem() {Cause = causes[2], Injury = injuries[1]},
                    new InjuryCauseMatrixItem() {Cause = causes[3], Injury = injuries[0]},
                    new InjuryCauseMatrixItem() {Cause = causes[4], Injury = injuries[2]},
                    new InjuryCauseMatrixItem() {Cause = causes[5], Injury = injuries[2]}
                };

            injuryCauses.ForEach(p => _dbContext.InjuryCauseMatrixItems.Add(p));
            _dbContext.SaveChanges();
        }

        private List<Cause> AddCauses(IList<Filter> filterCategories)
        {
            var causes = new List<Cause>()
                {
                    new Cause() {Description = "Repetitive stress", Category = CauseCategory.Lifestyle, Filter = filterCategories[10]},
                    new Cause() {Description = "Accelerating", Category = CauseCategory.Lifestyle, Filter = filterCategories[11]},
                    new Cause() {Description = "Sudden increase in training volume", Category = CauseCategory.Physiological, Filter = filterCategories[12]},
                    new Cause() {Description = "Speed Workouts", Category = CauseCategory.Lifestyle, Filter = filterCategories[11]},
                    new Cause() {Description = "Running", Category = CauseCategory.Physiological, Filter = filterCategories[13]},
                    new Cause() {Description = "Overweight", Category = CauseCategory.Physiological, Filter = filterCategories[14]}
                };

            causes.ForEach(p => _dbContext.Causes.Add(p));
            _dbContext.SaveChanges();

            return causes;
        }

        private void AssociateInjurySigns(IList<Sign> signs, IList<Injury> injuries)
        {
            var injurySigns = new List<InjurySignMatrixItem>()
                {
                    new InjurySignMatrixItem() {Injury = injuries[3], Sign = signs[0]},
                    new InjurySignMatrixItem() {Injury = injuries[2], Sign = signs[1]},
                    new InjurySignMatrixItem() {Injury = injuries[2], Sign = signs[0]},
                    new InjurySignMatrixItem() {Injury = injuries[0], Sign = signs[3]},
                    new InjurySignMatrixItem() {Injury = injuries[0], Sign = signs[4]},
                    new InjurySignMatrixItem() {Injury = injuries[1], Sign = signs[3]},
                    new InjurySignMatrixItem() {Injury = injuries[2], Sign = signs[2]}
                };

            injurySigns.ForEach(p => _dbContext.InjurySignMatrixItems.Add(p));
            _dbContext.SaveChanges();
        }

        private void AssociateInjuryPrognosis(IList<Prognosis> prognoses, IList<Injury> injuries)
        {
            var injuryPrognosis = new List<InjuryPrognosisMatrixItem>()
                {
                    new InjuryPrognosisMatrixItem() {Injury = injuries[3], Prognosis = prognoses[0], Duration = "2 - 3 Weeks"},
                    new InjuryPrognosisMatrixItem() {Injury = injuries[2], Prognosis = prognoses[1], Duration = " Never"},
                    new InjuryPrognosisMatrixItem() {Injury = injuries[2], Prognosis = prognoses[0], Duration = "2 - 3 Months"},
                    new InjuryPrognosisMatrixItem() {Injury = injuries[0], Prognosis = prognoses[3], Duration = "4 - 6 Weeks" },
                    new InjuryPrognosisMatrixItem() {Injury = injuries[0], Prognosis = prognoses[4], Duration = "8 - 10 Weeks"},
                    new InjuryPrognosisMatrixItem() {Injury = injuries[1], Prognosis = prognoses[3], Duration = "2 - 3 Weeks"},
                    new InjuryPrognosisMatrixItem() {Injury = injuries[2], Prognosis = prognoses[2], Duration = "7 - 10 Months"}
                };

            injuryPrognosis.ForEach(p => _dbContext.InjuryPrognosisMatrixItems.Add(p));
            _dbContext.SaveChanges();
        }

        private List<Prognosis> AddPrognosis()
        {
            var prognoses = new List<Prognosis>()
                {
                    new Prognosis() {Description = "If an appropriate rehabilitation plan is initiated within the first week after the onset of symptoms, a full recovery is expected within ", Name = "All ginney", Category = PrognosisCategory.BestCase},
                    new Prognosis() {Description = "Some gargin....", Name = "Maybe Ok", Category = PrognosisCategory.DelayedRecovery},
                    new Prognosis() {Description = "Your just screwed", Name = "F'd", Category = PrognosisCategory.WorstCase},
                    new Prognosis() {Description = "Blahhhh blahhh blahh", Name = "blahhh", Category = PrognosisCategory.BestCase},
                    new Prognosis() {Description = "If symptoms are ignored and the individual adheres to the 'no pain, no gain' theory, the recovery process becomes compromised.", Name = "Something bad", Category = PrognosisCategory.DelayedRecovery},
                    new Prognosis()
                        {
                            Description = "If an appropriate rehabilitation plan is initiated within the first week after the onset of symptoms, a full recovery is expected within ",
                            Name = "Rehab Best",
                            Category = PrognosisCategory.BestCase
                        }
                };

            prognoses.ForEach(t => _dbContext.Prognoses.Add(t));
            _dbContext.SaveChanges();

            return prognoses;
        } 

        private List<Filter> AddFilterCategories()
        {
            var signFilters = new List<Filter>()
                {
                    new Filter() {FilterCategory = "No Visual Abnormalities", FilterType = FilterType.Sign},
                    new Filter() {FilterCategory = "Swelling, Redness or Bruising", FilterType = FilterType.Sign},
                    new Filter() {FilterCategory = "Muscle Knot or Bump", FilterType = FilterType.Sign},
                    new Filter() {FilterCategory = "Abnormal Body / Joint Alignment", FilterType = FilterType.Sign},
                    new Filter() {FilterCategory = "Clicking, Snapping or Grinding", FilterType = FilterType.Sign},
                    new Filter() {FilterCategory = "Tightness or Stiffness", FilterType = FilterType.Sign},
                    new Filter() {FilterCategory = "Pain", FilterType = FilterType.Sign},
                    new Filter() {FilterCategory = "Tenderness to Touch", FilterType = FilterType.Sign},
                    new Filter() {FilterCategory = "Weakness or Joint Instability", FilterType = FilterType.Sign},
                    new Filter() {FilterCategory = "Variable Symptoms / Other", FilterType = FilterType.Sign},
                    new Filter() {FilterCategory = "Prolonged Sit/Stand/Sleeping", FilterType = FilterType.Sign},
                    new Filter() {FilterCategory = "Daily Activities", FilterType = FilterType.Sign},
                    new Filter() {FilterCategory = "Sport or Exercise", FilterType = FilterType.Sign},


                    new Filter() {FilterCategory = "Repetitive Movements", FilterType = FilterType.Cause},
                    new Filter() {FilterCategory = "Changes in Activity Level", FilterType = FilterType.Cause},
                    new Filter() {FilterCategory = "Trauma or Sport Related", FilterType = FilterType.Cause},
                    new Filter() {FilterCategory = "Poor Body Mechanics or Posture", FilterType = FilterType.Cause},
                    new Filter() {FilterCategory = "Improper Equipment or Shoes", FilterType = FilterType.Cause},
                    new Filter() {FilterCategory = "Structural/Skeletal Abnormality", FilterType = FilterType.Cause},
                    new Filter() {FilterCategory = "Muscle Tightness or Weakness", FilterType = FilterType.Cause},
                    new Filter() {FilterCategory = "Previous Injury or Surgery", FilterType = FilterType.Cause},
                    new Filter() {FilterCategory = "Unknown", FilterType = FilterType.Cause}
                };

            signFilters.ForEach(p => _dbContext.Filters.Add(p));
            _dbContext.SaveChanges();

            return signFilters;
        } 


        private List<Sign> AddSigns(IList<Filter> filterCategories)
        {
            var signs = new List<Sign>()
                {
                    new Sign() { Category = SignCategory.Functional, Description = "Painful to jump", Filter = filterCategories[0]},
                    new Sign() { Category = SignCategory.Visual, Description = "Love Handles", Filter = filterCategories[1]},
                    new Sign() { Category = SignCategory.Functional, Description = "Limited push-off", Filter = filterCategories[0]},
                    new Sign() { Category = SignCategory.Subjective, Description = "Throbing", Filter = filterCategories[2]},
                    new Sign() { Category = SignCategory.Subjective, Description = "Stiffness", Filter = filterCategories[3]},
                };

            signs.ForEach(p => _dbContext.Signs.Add(p));
            _dbContext.SaveChanges();

            return signs;
        }

        private void AssociateInjuryPlans(IList<Injury> injuries, IList<Plan> plans)
        {
            var injuryPlans = new List<InjuryPlanMatrixItem>()
                {
                    new InjuryPlanMatrixItem() {Injury = injuries[2], Plan = plans[0]},
                    new InjuryPlanMatrixItem() {Injury = injuries[2], Plan = plans[3]},
                    new InjuryPlanMatrixItem() {Injury = injuries[0], Plan = plans[1]},
                    new InjuryPlanMatrixItem() {Injury = injuries[1], Plan = plans[2]},
                    new InjuryPlanMatrixItem() {Injury = injuries[3], Plan = plans[3]},
                    new InjuryPlanMatrixItem() {Injury = injuries[3], Plan = plans[4]}
                };
            injuryPlans.ForEach(p => _dbContext.InjuryPlanMatrixItems.Add(p));
            _dbContext.SaveChanges();
        }

        private List<Plan> AddPlans()
        {
            var plans = new List<Plan>()
                {
                    new Plan()
                        {
                            RoutineName = "Plantar Fascia Rehab 1",
                            Description = "Plantar facciitis is an extremely common injury.   When this thick fascial structure becomes tight and inflamed, it can be very painful and limit function substantially. ",
                            Duration = 5,
                            AnimationTag = "xyz12345dssf",
                            StructuresInvolved = "Plantar Fascia, Gastroc/Soleus",
                            Instructions = "Do some shit"
                        },
                     new Plan()
                        {
                            RoutineName = "Sprained Ankle Stretching",
                            Description = "This program is designed to stretch and help mobilize the ankle after an ankle sprain",
                            Duration = 5,
                            AnimationTag = "xyz12345dssf",
                            StructuresInvolved = "Ankle Joint/Ligaments, Soleus",
                            Instructions = "Do some shit"
                        },
                     new Plan()
                        {
                            RoutineName = "General Shin Splint Stretching",
                            Description = "This program is designed for stretching the muscles that lead to shin splints if they become overly tight. ",
                            Duration = 5,
                            StructuresInvolved = "Ankle/Toe Extensors, Deep Toe Flexors, gastrocnemius, soleus",
                            Instructions = "Do some shit"
                        },
                     new Plan()
                        {
                            RoutineName = "Soleus Stretching",
                            Description = "This program is designed for athletes to stretch their soleus muscle.",
                            Duration = 5,
                            StructuresInvolved = "Soleus",
                            Instructions = "Do some shit"
                        },
                     new Plan()
                        {
                            RoutineName = "Calf Strain Rehab",
                            Description = "This program is designed to be used for athletes with an acute calf muscle.",
                            Duration = 5,
                            StructuresInvolved = "Gastrocnemius, Soleus, Hamstrings",
                            Instructions = "Do some shit"
                        }
                };

            plans.ForEach(p => _dbContext.Plans.Add(p));
            _dbContext.SaveChanges();

            return plans;

        }

        private void AssociatePlanExercise(IList<Plan> plans, IList<Exercise> exercises)
        {
            var planExercises = new List<PlanExerciseMatrixItem>()
                {
                    new PlanExerciseMatrixItem() {Plan = plans[0], Exercise = exercises[0], Repititions = 3, Sets = 2, PerDay = 1, PerWeek = 3, Order = 1},
                    new PlanExerciseMatrixItem() {Plan = plans[0], Exercise = exercises[1], Repititions = 2, Sets = 7, PerDay = 2, PerWeek = 1, Order = 2},
                    new PlanExerciseMatrixItem() {Plan = plans[1], Exercise = exercises[0], Repititions = 4, Sets = 9, PerDay = 1, PerWeek = 3, Order = 1},
                    new PlanExerciseMatrixItem() {Plan = plans[2], Exercise = exercises[3], Repititions = 7, Sets = 2, PerDay = 5, PerWeek = 3, Order = 1},
                    new PlanExerciseMatrixItem() {Plan = plans[3], Exercise = exercises[2], Repititions = 2, Sets = 1, PerDay = 6, PerWeek = 2, Order = 1},
                    new PlanExerciseMatrixItem() { Plan = plans[3], Exercise = exercises[3], Repititions = 3, Sets = 1, PerDay = 1, PerWeek = 7, Order = 2},
                    new PlanExerciseMatrixItem() { Plan = plans[3], Exercise = exercises[4], Repititions = 5, Sets = 2, PerDay = 8, PerWeek = 6, Order = 3},
                    new PlanExerciseMatrixItem() { Plan = plans[4], Exercise = exercises[4], Repititions = 3, Sets = 2, PerDay = 4, PerWeek = 3, Order = 1},
                    new PlanExerciseMatrixItem() { Plan = plans[4], Exercise = exercises[5], Repititions = 2, Sets = 8, PerDay = 6, PerWeek = 2, Order = 2},
                    new PlanExerciseMatrixItem() { Plan = plans[4], Exercise = exercises[6], Repititions = 30, Sets = 2, PerDay = 4, PerWeek = 5, Order = 3}
                };

            planExercises.ForEach(p => _dbContext.PlansExceriseMatrixItems.Add(p));
            _dbContext.SaveChanges();

        }

        private List<Exercise> AddExercises()
        {

            var exercises = new List<Exercise>()
                {
                    new Exercise() {Name = "Heel Self Massage", Description = "Touch yo self", Difficulty = ExerciseDifficulty.Beginner, Duration = 5},
                    new Exercise() {Name = "Seated Plantar Fascis Stretch", Description = "Strech yo self", Difficulty = ExerciseDifficulty.Advanced, Duration = 10},
                    new Exercise() {Name = "Standing calf Stretch", Difficulty = ExerciseDifficulty.Beginner, Duration = 5},
                    new Exercise() {Name = "Standing Wall Calf Stretch", Difficulty = ExerciseDifficulty.Intermediate, Duration = 3},
                    new Exercise() {Name = "Standing Soleus Stretch 1", Difficulty = ExerciseDifficulty.Beginner, Duration = 5},
                    new Exercise() {Name = "Standing Soleus Stretch 2", Difficulty = ExerciseDifficulty.Advanced, Duration = 7},
                    new Exercise() {Name = "Calf Knee Massage", Description = "Touch yo self", Difficulty = ExerciseDifficulty.Beginner, Duration = 5}
                };

            exercises.ForEach(p => _dbContext.Exercises.Add(p));
            _dbContext.SaveChanges();

            return exercises;
        }

        private void AssociateExerciseCategories(IEnumerable<Exercise> exercises)
        {
            var exerciseCategories = exercises.Select(exercise => new ExerciseCategoryMatrixItem() {Category = FunctionCategory.Balance, Exercise = exercise}).ToList();

            exerciseCategories.ForEach(e => _dbContext.ExerciseCategoryMatrixItems.Add(e));
            _dbContext.SaveChanges();
        }

        private void AssociatePlanCategories(IEnumerable<Plan> plans)
        {
            var planCategories = plans.Select(plan => new PlanCategoryMatrixItem() { Category = FunctionCategory.Balance, Plan = plan }).ToList();

            planCategories.ForEach(e => _dbContext.PlanCategoryMatrixItems.Add(e));
            _dbContext.SaveChanges();
        }

        private List<Video> AddVideos()
        {
            var videos = new List<Video>()
                {
                    new Video() {CreationDate = DateTime.Now, Name = "Calf Raises", MedicalName = "Calf Raises", Filename = "SWPT_Template.m4v", Description = "How to raise your calfs"},
                    new Video() {CreationDate = DateTime.Now, Name = "Shin Stretch", MedicalName = "Shin Stretch", Filename = "SWPT_Template.m4v", Description = "How to strech your shins"},
                    new Video() {CreationDate = DateTime.Now, Name = "Ankle Strength", MedicalName = "Ankle Strength", Filename = "SWPT_Template.m4v", Description = "How to strengthen your ankles"},
                    new Video() {CreationDate = DateTime.Now, Name = "Toe Stretch", MedicalName = "Toe Stretch", Filename = "SWPT_Template.m4v", Description = "How to strech your toes"},
                    new Video() {CreationDate = DateTime.Now, Name = "Heel self message", MedicalName = "Heel self message", Filename = "SWPT_Template.m4v", Description = "How to touch yourself"}
                };

            videos.ForEach(p => _dbContext.Videos.Add(p));
            _dbContext.SaveChanges();

            return videos;
        }

        private void AssociateVideoCategory(IEnumerable<Video> videos)
        {
            var videoCategories = videos.Select(video => new VideoCategoryMatrixItem() { Category = FunctionCategory.Balance, Video = video }).ToList();

            videoCategories.ForEach(e => _dbContext.VideoCategoryMatrixItems.Add(e));
            _dbContext.SaveChanges();
        }

        private void AssociateExerciseVideos(IList<Exercise> exercises, IList<Video> videos)
        {
            var exerciseVideos = new List<ExerciseVideoMatrixItem>()
                {
                    new ExerciseVideoMatrixItem() {Exercise = exercises[0], Video = videos[4]},
                    new ExerciseVideoMatrixItem() {Exercise = exercises[1], Video = videos[3]},
                    new ExerciseVideoMatrixItem() {Exercise = exercises[2], Video = videos[0]},
                    new ExerciseVideoMatrixItem() {Exercise = exercises[3], Video = videos[1]},
                    new ExerciseVideoMatrixItem() {Exercise = exercises[4], Video = videos[2]},
                    new ExerciseVideoMatrixItem() {Exercise = exercises[5], Video = videos[0]},
                    new ExerciseVideoMatrixItem() {Exercise = exercises[6], Video = videos[4]}
                };

            exerciseVideos.ForEach(p => _dbContext.ExerciseVideoMatrixItems.Add(p));
            _dbContext.SaveChanges();

        }

        private void AssociateExerciseEquipment(IList<Exercise> exercises, IList<Equipment> equipment)
        {
            var exerciseEquipment = new List<ExerciseEquipmentMatrixItem>()
                {
                    new ExerciseEquipmentMatrixItem() {Exercise = exercises[0], Equipment = equipment[2]},
                    new ExerciseEquipmentMatrixItem() {Exercise = exercises[2], Equipment = equipment[0]},
                    new ExerciseEquipmentMatrixItem() {Exercise = exercises[2], Equipment = equipment[3]},
                    new ExerciseEquipmentMatrixItem() {Exercise = exercises[3], Equipment = equipment[0]},
                    new ExerciseEquipmentMatrixItem() {Exercise = exercises[3], Equipment = equipment[3]}
                };

            exerciseEquipment.ForEach(p => _dbContext.ExerciseEquipmentMatrixItems.Add(p));
            _dbContext.SaveChanges();

        }

        private void AssociateExerciseBodyRegion(IList<Exercise> exercises, IList<BodyRegion> bodyRegions)
        {
            var exerciseBodyRegions = new List<ExerciseBodyRegionMatrixItem>()
                {
                    new ExerciseBodyRegionMatrixItem() {Exercise = exercises[0], BodyRegion = bodyRegions[0]},
                    new ExerciseBodyRegionMatrixItem() {Exercise = exercises[0], BodyRegion = bodyRegions[7]},
                    new ExerciseBodyRegionMatrixItem() {Exercise = exercises[1], BodyRegion = bodyRegions[1]},
                    new ExerciseBodyRegionMatrixItem() {Exercise = exercises[2], BodyRegion = bodyRegions[2]},
                    new ExerciseBodyRegionMatrixItem() {Exercise = exercises[3], BodyRegion = bodyRegions[3]},
                    new ExerciseBodyRegionMatrixItem() {Exercise = exercises[4], BodyRegion = bodyRegions[4]},
                    new ExerciseBodyRegionMatrixItem() {Exercise = exercises[4], BodyRegion = bodyRegions[8]},
                    new ExerciseBodyRegionMatrixItem() {Exercise = exercises[5], BodyRegion = bodyRegions[5]},
                    new ExerciseBodyRegionMatrixItem() {Exercise = exercises[6], BodyRegion = bodyRegions[6]}
                };

            exerciseBodyRegions.ForEach(p => _dbContext.ExerciseBodyRegionMatrixItems.Add(p));
            _dbContext.SaveChanges();

        }

        private void AssociatePlanBodyRegion(IList<Plan> plans, IList<BodyRegion> bodyRegions)
        {
            var planBodyRegions = new List<PlanBodyRegionMatrixItem>()
                {
                    new PlanBodyRegionMatrixItem() {Plan = plans[0], BodyRegion = bodyRegions[0]},
                    new PlanBodyRegionMatrixItem() {Plan = plans[0], BodyRegion = bodyRegions[7]},
                    new PlanBodyRegionMatrixItem() {Plan = plans[1], BodyRegion = bodyRegions[1]},
                    new PlanBodyRegionMatrixItem() {Plan = plans[2], BodyRegion = bodyRegions[2]},
                    new PlanBodyRegionMatrixItem() {Plan = plans[3], BodyRegion = bodyRegions[3]},
                    new PlanBodyRegionMatrixItem() {Plan = plans[4], BodyRegion = bodyRegions[4]},
                    new PlanBodyRegionMatrixItem() {Plan = plans[4], BodyRegion = bodyRegions[8]},
                    new PlanBodyRegionMatrixItem() {Plan = plans[2], BodyRegion = bodyRegions[5]},
                    new PlanBodyRegionMatrixItem() {Plan = plans[1], BodyRegion = bodyRegions[6]}
                };

            planBodyRegions.ForEach(p => _dbContext.PlanBodyRegionMatrixItems.Add(p));
            _dbContext.SaveChanges();

        }


        private void AssociateInjuryBodyRegion(IList<Injury> injuries, IList<BodyRegion> bodyRegions)
        {
            var injuryBodyRegions = new List<InjuryBodyRegionMatrixItem>()
                {
                    new InjuryBodyRegionMatrixItem() {Injury = injuries[0], BodyRegion = bodyRegions[0]},
                    new InjuryBodyRegionMatrixItem() {Injury = injuries[0], BodyRegion = bodyRegions[7]},
                    new InjuryBodyRegionMatrixItem() {Injury = injuries[1], BodyRegion = bodyRegions[1]},
                    new InjuryBodyRegionMatrixItem() {Injury = injuries[2], BodyRegion = bodyRegions[2]},
                    new InjuryBodyRegionMatrixItem() {Injury = injuries[3], BodyRegion = bodyRegions[3]},
                    new InjuryBodyRegionMatrixItem() {Injury = injuries[2], BodyRegion = bodyRegions[4]},
                    new InjuryBodyRegionMatrixItem() {Injury = injuries[1], BodyRegion = bodyRegions[8]},
                    new InjuryBodyRegionMatrixItem() {Injury = injuries[3], BodyRegion = bodyRegions[5]},
                    new InjuryBodyRegionMatrixItem() {Injury = injuries[3], BodyRegion = bodyRegions[6]}
                };

            injuryBodyRegions.ForEach(p => _dbContext.InjuryBodyRegionMatrixItems.Add(p));
            _dbContext.SaveChanges();

        }


        private void AssociateBodyPartsToExercise(IList<BodyPart> bodyParts, IList<Exercise> exercises)
        {
            var exerciseBodyPartItems = new List<ExerciseBodyPartMatrixItem>()
                {
                    new ExerciseBodyPartMatrixItem() {BodyPart = bodyParts[0], Exercise = exercises[0]},
                    new ExerciseBodyPartMatrixItem() {BodyPart = bodyParts[1], Exercise = exercises[0]},
                    new ExerciseBodyPartMatrixItem() {BodyPart = bodyParts[2], Exercise = exercises[1]},
                    new ExerciseBodyPartMatrixItem() {BodyPart = bodyParts[3], Exercise = exercises[2]},
                    new ExerciseBodyPartMatrixItem() {BodyPart = bodyParts[4], Exercise = exercises[3]},
                    new ExerciseBodyPartMatrixItem() {BodyPart = bodyParts[5], Exercise = exercises[4]},
                    new ExerciseBodyPartMatrixItem() {BodyPart = bodyParts[5], Exercise = exercises[5]},
                    new ExerciseBodyPartMatrixItem() {BodyPart = bodyParts[6], Exercise = exercises[5]},
                    new ExerciseBodyPartMatrixItem() {BodyPart = bodyParts[6], Exercise = exercises[3]}
                };

            exerciseBodyPartItems.ForEach(p => _dbContext.ExerciseBodyPartMatrixItems.Add(p));
            _dbContext.SaveChanges();
        }

        private List<Equipment> AddEquipment()
        {
            var equipmentList = new List<Equipment>()
                {
                    new Equipment()
                        {
                            CommonName = "Foam Roller",
                            PriceRange = "10 - 50",
                            ProductInformation = "Foam House", 
                            Category = FunctionCategory.Strengthening
                        },
                    new Equipment()
                        {
                            CommonName = "Theracane",
                            PriceRange = "20 - 60",
                            ProductInformation = "ActiveLife", 
                            Category = FunctionCategory.Strengthening
                        },
                    new Equipment() {CommonName = "Baseball", PriceRange = "2 - 7", Category = FunctionCategory.Strengthening},
                    new Equipment() {CommonName = "Wall", PriceRange = "0", Category = FunctionCategory.Strengthening}
                };

            equipmentList.ForEach(p => _dbContext.Equipment.Add(p));
            _dbContext.SaveChanges();

            return equipmentList;
        }

        private List<SymptomMatrixItem> BuildSymptomMatrix(IEnumerable<Symptom> symptoms, IEnumerable<BodyPartMatrixItem> bodyPartMartix)
        {
            var symptomMatrixItems = new List<SymptomMatrixItem>();
            //bodyPartMartix.ForEach(c => symptoms.ForEach(s => symptomMatrixItems.Add(new SymptomMatrixItem {BodyPartMatrixItemId = c.Id, SymptomId = s.Id})));
            //symptomMatrixItems.ForEach(s => _dbContext.SymptomMatrix.Add(s));
            //_dbContext.SaveChanges();

            return symptomMatrixItems;
        }

        private void BuildInjurySymptomMatrix(IList<SymptomMatrixItem> symptomMatrixItems, IList<Injury> injuries)
        {
            var injurySymptomMatrixItems = new List<InjurySymptomMatrixItem>();

            //symptomMatrixItems.Where(s => s.BodyPartMatrixItem.BodyPart.CommonName.Equals("Ankle")).ForEach(p => injurySymptomMatrixItems.Add(new InjurySymptomMatrixItem()
            //    {
            //        Injury = injuries[0],
            //        SymptomMatrixItem = p,
            //        ThresholdValue = 1
            //    }));

            //symptomMatrixItems.Where(s => s.BodyPartMatrixItem.BodyPart.CommonName.Equals("Shin")).ForEach(p => injurySymptomMatrixItems.Add(new InjurySymptomMatrixItem()
            //{
            //    Injury = injuries[1],
            //    SymptomMatrixItem = p,
            //    ThresholdValue = 1
            //}));

            //symptomMatrixItems.Where(s => s.BodyPartMatrixItem.BodyPart.CommonName.Equals("Foot")).ForEach(p => injurySymptomMatrixItems.Add(new InjurySymptomMatrixItem()
            //{
            //    Injury = injuries[2],
            //    SymptomMatrixItem = p,
            //    ThresholdValue = 5
            //}));

            //symptomMatrixItems.Where(s => s.BodyPartMatrixItem.BodyPart.CommonName.Equals("Foot")).ForEach(p => injurySymptomMatrixItems.Add(new InjurySymptomMatrixItem()
            //{
            //    Injury = injuries[0],
            //    SymptomMatrixItem = p,
            //    ThresholdValue = 1
            //}));

            //symptomMatrixItems.Where(s => s.BodyPartMatrixItem.BodyPart.CommonName.Equals("Calf")).ForEach(p => injurySymptomMatrixItems.Add(new InjurySymptomMatrixItem()
            //{
            //    Injury = injuries[3],
            //    SymptomMatrixItem = p,
            //    ThresholdValue = 1
            //}));


            injurySymptomMatrixItems.ForEach(p => _dbContext.InjurySymptomMatrix.Add(p));
            _dbContext.SaveChanges();
        }

        private List<Vendor> AddVendors()
        {
            var vendors = new List<Vendor>()
                {
                    new Vendor() {Name = "Nike", Url = "www.nike.com"},
                    new Vendor() {Name = "Addidas", Url = "www.addidas.com"}
                };

            vendors.ForEach(p => _dbContext.Vendors.Add(p));
            _dbContext.SaveChanges();

            return vendors;
        } 

        private List<Treatment> AddTreatments()
        {
            var treatments = new List<Treatment>()
                {
                    new Treatment() {Description = "R.I.C.E", Name = "rice", Provider = ProviderType.Self, Category = TreatmentCategory.Modalities},
                    new Treatment() {Description = "B.L.O.W", Name = "blow", Provider = ProviderType.Self, Category = TreatmentCategory.ManualTherapy},
                    new Treatment() {Description = "Read", Name = "read", Provider = ProviderType.Self, Category = TreatmentCategory.Education},
                    new Treatment() {Description = "Blahhhh blahhh blahh", Name = "blahhh", Provider = ProviderType.Surgeon, Category = TreatmentCategory.Modalities},
                    new Treatment() {Description = "Go get Rx", Name = "drugs", Provider = ProviderType.Physican, Category = TreatmentCategory.TherEx},
                    new Treatment()
                        {
                            Description = "Get deep tissue stuff done",
                            Name = "deep tissue",
                            Provider = ProviderType.PhysicalTherapist,
                            Category = TreatmentCategory.ManualTherapy
                        }
                };

            treatments.ForEach(t => _dbContext.Treatments.Add(t));
            _dbContext.SaveChanges();

            return treatments;
        }

        private List<Injury> AddInjuries()
        {
            var injuries = new List<Injury>()
                {
                    new Injury()
                        {
                            Severity = InjurySeverity.Minor,
                            CommonName = "Sprained Ankle",
                            AnimationTag = "xyz12345dssf",
                            Description =
                                "An Ankle strain/sprain is easily one of the most common and easily identified injuries in any sport.  An athlete knows they sprained their ankle the instant they do it and the next couple days are predictable with rehabilitation. This is a bad sentence.  I understand what you’re trying to say, but can you rephrase?  The timetable for return to activity is heavily dependent on severity of the strain or sprain.  It is very important to ICE the ankle within the first 48-72 hours of the sprain.  Consistent icing can have a significant impact in expediting the rehabilitation time. There has to be more information for you to add!!!  What about adding a recommendation to have an X-ray for severe injuries to asure there is not a break????  Also, is there any way to strengthen the tendons and muscles to HELP prevent the injury???  ",
                            MedicalName = "Ankle Sprain",
                            Prognosis = "Prognosis Negative",
                            Recovery = "Do what we tell you to"
                        },
                    new Injury()
                        {                         
                            Severity = InjurySeverity.Minor,
                            CommonName = "Shin Splints",
                            AnimationTag = "xfg5345dssf",
                            Description =
                                "Shin splints can manifest in two different forms, one coming from the muscles in the front of the shin (ankle/toe extensors) and the other coming from the back of the shin (deep toe flexors).  Here we will focus on the anterior shin splints.  The muscles on the front of your shin (anterior tibialis, extensor hallucis and digitorum longus) are responsible for controlling the foot and ankle during the landing phase of running or walking.  With continued use, the muscles continue to experience stress and begin to shorten and form pockets of scar tissue.  If not take care of properly, this process will continue and eventually the athlete will begin to experience pain and tightness in the front of the shin.  As symptoms progress, they can eventually lead to difficulty with walking and running activities.  Anterior shin splints can be prevented with regular self -massage, stretching, and minor activity modification.",
                            MedicalName = "Anterior Tbialis",
                            Prognosis = "Prognosis Negative",
                            Recovery = "Do what we tell you to"
                        },
                    new Injury()
                        {
                            Severity = InjurySeverity.Major,
                            CommonName = "Plantar Faciitis",
                            Description =
                                "Plantar Fasciitis is the name given to describe the inflammatory condition of the Plantar Fascia, the thick band of ligament that runs along the bottom of the foot and connects the heel bone to the toes. This tissue provides support to the arch of the foot.  With each step, the plantar fascia undergoes a moderate stretching tension as the arch compresses. Plantar Fasciitis occurs when a repeatedly excessive stretch causes small tears in the plantar fascia.  There are a variety of causes for plantar fasciitis.  As we age, our bodies adapt to our lifestyles, and asymmetries arise between the left and right sides of our body.  Many people notice they are stronger on one side or more flexible on the other side.  This natural adaptation to your lifestyle may contribute to this injury affecting only one of your feet.  Commonly experienced by men and women in their middle ages, the condition may also affect young people, especially recreational athletes, such as runners. ",
                            MedicalName = "Plantar Faciitis",
                            Prognosis = "Prognosis Negative",
                            Recovery = "Do what we tell you to"
                        },
                    new Injury()
                        {
                            Severity = InjurySeverity.Moderate,
                            CommonName = "Calf Strain",
                            Description =
                                "The calf is constructed of three main structures: Gastrocnemius, Soleus, and Achilles tendon.  This unit is vital in pushing off to running, jumping, pedaling, and any exercise that involves pushing off with the legs.  A calf strain occurs when a portion of the muscle is contracted to forcefully during activity and goes into spasm or 'freaks out'.  This is known as the 'oh crap' moment.  When this event occurs, the athlete will experience sharp pain each time the muscle is contracted, usually with pushing off during running and walking.  The injured area needs to go through the healing process, which is usually between 2-4 weeks (although can be much longer with severe strains).  Light stretching and light soft tissue work can dramatically increase the healing process and get the athlete back to training sooner.",
                            MedicalName = "Gastrocnemius/Soleus Strain",
                            Prognosis = "Prognosis Negative",
                            Recovery = "Do what we tell you to"
                        },
                };

            injuries.ForEach(p => _dbContext.Injuries.Add(p));
            _dbContext.SaveChanges();

            return injuries;
        }

        private List<BodyPart> AddComponents()
        {
            var parts = new List<BodyPart>()
                {
                    new BodyPart() {CommonName = "Head"},
                    new BodyPart() {CommonName = "Neck"},
                    new BodyPart() {CommonName = "Back of Head"},
                    new BodyPart() {CommonName = "Back of Neck"},
                    new BodyPart() {CommonName = "Jaw"},
                    new BodyPart() {CommonName = "Collarbone"},
                    new BodyPart() {CommonName = "Perctoral Muscle"},
                    new BodyPart() {CommonName = "Ribs"},
                    new BodyPart() {CommonName = "Shoulder"},
                    new BodyPart() {CommonName = "AC Joint"},
                    new BodyPart() {CommonName = "Rotator Cuff"}, //10
                    new BodyPart() {CommonName = "Biceps"},
                    new BodyPart() {CommonName = "Elbow"},
                    new BodyPart() {CommonName = "Forearm"},
                    new BodyPart() {CommonName = "Hand"},
                    new BodyPart() {CommonName = "Wrist"},
                    new BodyPart() {CommonName = "Abdomen"}, //16
                    new BodyPart() {CommonName = "Hip"},
                    new BodyPart() {CommonName = "Pelvis"},
                    new BodyPart() {CommonName = "Groin"},
                    new BodyPart() {CommonName = "Quadriceps"}, //20
                    new BodyPart() {CommonName = "IT Band"}, 
                    new BodyPart() {CommonName = "Knee"},
                    new BodyPart() {CommonName = "Shin"},
                    new BodyPart() {CommonName = "Bottom of Foot"},
                    new BodyPart() {CommonName = "Ankle"},
                    new BodyPart() {CommonName = "Shoulder Blades"},
                    new BodyPart() {CommonName = "Middle of Back"},
                    new BodyPart() {CommonName = "Triceps"},
                    new BodyPart() {CommonName = "Lower Back"},
                    new BodyPart() {CommonName = "SI Joint"},
                    new BodyPart() {CommonName = "Piriformis"},
                    new BodyPart() {CommonName = "Upper Hamstring"},
                    new BodyPart() {CommonName = "Hamstring"},
                    new BodyPart() {CommonName = "Calf"},
                    new BodyPart() {CommonName = "Achilles"},
                    new BodyPart() {CommonName = "Heel"}
                };

            parts.ForEach(u => _dbContext.BodyParts.Add(u));
            _dbContext.SaveChanges();

            return parts;
        }

        private List<BodyRegion> AddRegions()
        {
            var regions = new List<BodyRegion>()
                {
                    new BodyRegion() { Name = "Head and Neck", RegionCategory = BodyRegionCategory.UpperExtremity},
                    new BodyRegion() { Name = "Chest", RegionCategory = BodyRegionCategory.UpperExtremity},
                    new BodyRegion() { Name = "Shoulder", RegionCategory = BodyRegionCategory.UpperExtremity},
                    new BodyRegion() { Name = "Arm", RegionCategory = BodyRegionCategory.UpperExtremity},
                    new BodyRegion() { Name = "Elbow", RegionCategory = BodyRegionCategory.UpperExtremity},
                    new BodyRegion() { Name = "Forearm", RegionCategory = BodyRegionCategory.UpperExtremity},
                    new BodyRegion() { Name = "Hand and Wrist", RegionCategory = BodyRegionCategory.UpperExtremity},
                    new BodyRegion() { Name = "Abdomen", RegionCategory = BodyRegionCategory.UpperExtremity},
                    new BodyRegion() { Name = "Upper Leg", RegionCategory = BodyRegionCategory.LowerExtremity},
                    new BodyRegion() { Name = "Knee", RegionCategory = BodyRegionCategory.LowerExtremity},
                    new BodyRegion() { Name = "Shin", RegionCategory = BodyRegionCategory.LowerExtremity},
                    new BodyRegion() { Name = "Foot and Ankle", RegionCategory = BodyRegionCategory.LowerExtremity},
                    new BodyRegion() { Name = "Middle Back", RegionCategory = BodyRegionCategory.UpperExtremity},
                    new BodyRegion() { Name = "Lower Back", RegionCategory = BodyRegionCategory.Spine},
                    new BodyRegion() { Name = "Hip and Buttock", RegionCategory = BodyRegionCategory.LowerExtremity},
                    new BodyRegion() { Name = "Calf", RegionCategory = BodyRegionCategory.LowerExtremity}
                };
            //8  to 10  - 17 to 13 
            regions.ForEach(u => _dbContext.BodyRegions.Add(u));
            _dbContext.SaveChanges();

            return regions;
        }

        private List<Orientation> AddOrientations()
        {
            var orientations = new List<Orientation>()
                {
                    new Orientation() { Value = "Front"},
                    new Orientation() { Value = "Side"},
                    new Orientation() { Value = "Back"}
                };

            orientations.ForEach(u => _dbContext.Orientations.Add(u));
            _dbContext.SaveChanges();

            return orientations;
        }

        private List<Side> AddSides()
        {
            var sides = new List<Side>()
                {
                    new Side() { Value = "Left"},
                    new Side() { Value = "Right"},
                    new Side() { Value = "Both"}
                };

            sides.ForEach(u => _dbContext.Sides.Add(u));
            _dbContext.SaveChanges();

            return sides;
        }

        private IList<SkeletonArea> AddSkeletonAreas(List<BodyRegion> regionTypes,List<Side> sides,List<Orientation> orientations)
        {
            var areas = new List<SkeletonArea>()
                {
                    new SkeletonArea() { Region = regionTypes[0], Orientation = orientations[0], Side = sides[2], CssClassName = "head-neck-front", DisplayName = "Head and Neck"},
                    new SkeletonArea() { Region = regionTypes[1], Orientation = orientations[0], Side = sides[2], CssClassName = "chest-front", DisplayName = "Chest"},
                    new SkeletonArea() { Region = regionTypes[2], Orientation = orientations[0], Side = sides[0], CssClassName = "left-shoulder-front", DisplayName = "Left Shoulder"},
                    new SkeletonArea() { Region = regionTypes[2], Orientation = orientations[0], Side = sides[1], CssClassName = "right-shoulder-front", DisplayName = "Right Shoulder"},
                    new SkeletonArea() { Region = regionTypes[3], Orientation = orientations[0], Side = sides[0], CssClassName = "left-arm-front", DisplayName = "Left Arm"},
                    new SkeletonArea() { Region = regionTypes[3], Orientation = orientations[0], Side = sides[1], CssClassName = "right-arm-front", DisplayName = "Right Arm"},
                    new SkeletonArea() { Region = regionTypes[4], Orientation = orientations[0], Side = sides[0], CssClassName = "left-elbow-front", DisplayName = "Left Elbow"},
                    new SkeletonArea() { Region = regionTypes[4], Orientation = orientations[0], Side = sides[1], CssClassName = "right-elbow-front", DisplayName = "Right Elbow"},
                    new SkeletonArea() { Region = regionTypes[5], Orientation = orientations[0], Side = sides[0], CssClassName = "left-formarm-front", DisplayName = "Left Forearm"},
                    new SkeletonArea() { Region = regionTypes[5], Orientation = orientations[0], Side = sides[1], CssClassName = "right-forarm-front", DisplayName = "Right Forearm"},
                    new SkeletonArea() { Region = regionTypes[6], Orientation = orientations[0], Side = sides[0], CssClassName = "left-hand-front", DisplayName = "Left Hand and Wrist"},
                    new SkeletonArea() { Region = regionTypes[6], Orientation = orientations[0], Side = sides[1], CssClassName = "right-hand-front", DisplayName = "Right Hand and Wrist"},
                    new SkeletonArea() { Region = regionTypes[7], Orientation = orientations[0], Side = sides[2], CssClassName = "abs-front", DisplayName = "Abdominal"},
                    new SkeletonArea() { Region = regionTypes[14], Orientation = orientations[0], Side = sides[0], CssClassName = "left-hip-pelvis-front", DisplayName = "Left Hip and Pelvis"},
                    new SkeletonArea() { Region = regionTypes[14], Orientation = orientations[0], Side = sides[1], CssClassName = "right-hip-pelvis-front", DisplayName = "Right Hip and Pelvis"},
                    new SkeletonArea() { Region = regionTypes[8], Orientation = orientations[0], Side = sides[0], CssClassName = "left-upperleg-front", DisplayName = "Left Upper Leg"},
                    new SkeletonArea() { Region = regionTypes[8], Orientation = orientations[0], Side = sides[1], CssClassName = "right-upperleg-front", DisplayName = "Right Upper Leg"},
                    new SkeletonArea() { Region = regionTypes[9], Orientation = orientations[0], Side = sides[0], CssClassName = "left-knee-front", DisplayName = "Left Knee"},
                    new SkeletonArea() { Region = regionTypes[9], Orientation = orientations[0], Side = sides[1], CssClassName = "right-knee-front", DisplayName = "Right Knee"},
                    new SkeletonArea() { Region = regionTypes[10], Orientation = orientations[0], Side = sides[0], CssClassName = "left-shin-front", DisplayName = "Left Shin"},
                    new SkeletonArea() { Region = regionTypes[10], Orientation = orientations[0], Side = sides[1], CssClassName = "right-shin-front", DisplayName = "Right Shin"},
                    new SkeletonArea() { Region = regionTypes[11], Orientation = orientations[0], Side = sides[0], CssClassName = "left-foot-front", DisplayName = "Left Foot and Ankle"},
                    new SkeletonArea() { Region = regionTypes[11], Orientation = orientations[0], Side = sides[1], CssClassName = "right-foot-front", DisplayName = "Right Foot and Ankle"},
                    new SkeletonArea() { Region = regionTypes[0], Orientation = orientations[2], Side = sides[2], CssClassName = "head-neck-back", DisplayName = "Back of Head and Neck"},
                    new SkeletonArea() { Region = regionTypes[12], Orientation = orientations[2], Side = sides[2], CssClassName = "mid-back-back", DisplayName = "Middle of Back"},
                    new SkeletonArea() { Region = regionTypes[3], Orientation = orientations[2], Side = sides[0], CssClassName = "left-arm-back", DisplayName = "Back Left Arm"},
                    new SkeletonArea() { Region = regionTypes[3], Orientation = orientations[2], Side = sides[1], CssClassName = "right-arm-back", DisplayName = "Back Right Arm"},
                    new SkeletonArea() { Region = regionTypes[13], Orientation = orientations[2], Side = sides[2], CssClassName = "lower-back-back", DisplayName = "Lower Back"},
                    new SkeletonArea() { Region = regionTypes[14], Orientation = orientations[2], Side = sides[0], CssClassName = "left-hip-back", DisplayName = "Left Hip and Buttock"},
                    new SkeletonArea() { Region = regionTypes[14], Orientation = orientations[2], Side = sides[1], CssClassName = "right-hip-back", DisplayName = "Right Hip and Buttock"},
                    new SkeletonArea() { Region = regionTypes[8], Orientation = orientations[2], Side = sides[0], CssClassName = "left-upperleg-back", DisplayName = "Back Left Leg"},
                    new SkeletonArea() { Region = regionTypes[8], Orientation = orientations[2], Side = sides[1], CssClassName = "right-upperleg-back", DisplayName = "Back Right Leg"},
                    new SkeletonArea() { Region = regionTypes[15], Orientation = orientations[2], Side = sides[0], CssClassName = "left-calf-back", DisplayName = "Left Calf"},
                    new SkeletonArea() { Region = regionTypes[15], Orientation = orientations[2], Side = sides[1], CssClassName = "right-calf-back", DisplayName = "Right Calf"},
                    new SkeletonArea() { Region = regionTypes[11], Orientation = orientations[2], Side = sides[0], CssClassName = "left-heel-back", DisplayName = "Left Heel and Ankle"},
                    new SkeletonArea() { Region = regionTypes[11], Orientation = orientations[2], Side = sides[1], CssClassName = "right-heel-back", DisplayName = "Right Heel and Ankle"},
                    new SkeletonArea() { Region = regionTypes[9], Orientation = orientations[2], Side = sides[0], CssClassName = "left-knee-back", DisplayName = "Back Left Knee"},
                    new SkeletonArea() { Region = regionTypes[9], Orientation = orientations[2], Side = sides[1], CssClassName = "right-knee-back", DisplayName = "Back Right Knee"},
                    new SkeletonArea() { Region = regionTypes[2], Orientation = orientations[2], Side = sides[0], CssClassName = "left-shoulder-back", DisplayName = "Back Left Shoulder"},
                    new SkeletonArea() { Region = regionTypes[2], Orientation = orientations[2], Side = sides[1], CssClassName = "right-shoulder-back", DisplayName = "Back Right Shoulder"}
                    };

            areas.ForEach(u => _dbContext.SkeletonAreas.Add(u));
            _dbContext.SaveChanges();

            return areas;
        }

        private IEnumerable<BodyPartMatrixItem> AddBodyPartsToAreas(IList<SkeletonArea> areas, IList<BodyPart> bodyParts)
        {
            var bodyPartMatrixItems = new List<BodyPartMatrixItem>();

            bodyPartMatrixItems.Add(new BodyPartMatrixItem() { SkeletonArea = areas[0], BodyPart = bodyParts[0]});
            bodyPartMatrixItems.Add(new BodyPartMatrixItem() { SkeletonArea = areas[0], BodyPart = bodyParts[1]});
            bodyPartMatrixItems.Add(new BodyPartMatrixItem() { SkeletonArea = areas[0], BodyPart = bodyParts[4] });
            bodyPartMatrixItems.Add(new BodyPartMatrixItem() { SkeletonArea = areas[1], BodyPart = bodyParts[5] });
            bodyPartMatrixItems.Add(new BodyPartMatrixItem() { SkeletonArea = areas[1], BodyPart = bodyParts[6] });
            bodyPartMatrixItems.Add(new BodyPartMatrixItem() { SkeletonArea = areas[1], BodyPart = bodyParts[7] });
            bodyPartMatrixItems.Add(new BodyPartMatrixItem() { SkeletonArea = areas[2], BodyPart = bodyParts[8] });
            bodyPartMatrixItems.Add(new BodyPartMatrixItem() { SkeletonArea = areas[3], BodyPart = bodyParts[8] });
            bodyPartMatrixItems.Add(new BodyPartMatrixItem() { SkeletonArea = areas[2], BodyPart = bodyParts[9] });
            bodyPartMatrixItems.Add(new BodyPartMatrixItem() { SkeletonArea = areas[3], BodyPart = bodyParts[9] });
            bodyPartMatrixItems.Add(new BodyPartMatrixItem() { SkeletonArea = areas[2], BodyPart = bodyParts[10] });
            bodyPartMatrixItems.Add(new BodyPartMatrixItem() { SkeletonArea = areas[3], BodyPart = bodyParts[10] });
            bodyPartMatrixItems.Add(new BodyPartMatrixItem() { SkeletonArea = areas[4], BodyPart = bodyParts[11] });
            bodyPartMatrixItems.Add(new BodyPartMatrixItem() { SkeletonArea = areas[5], BodyPart = bodyParts[11] });
            bodyPartMatrixItems.Add(new BodyPartMatrixItem() { SkeletonArea = areas[6], BodyPart = bodyParts[12] });
            bodyPartMatrixItems.Add(new BodyPartMatrixItem() { SkeletonArea = areas[7], BodyPart = bodyParts[12] });
            bodyPartMatrixItems.Add(new BodyPartMatrixItem() { SkeletonArea = areas[8], BodyPart = bodyParts[13] });
            bodyPartMatrixItems.Add(new BodyPartMatrixItem() { SkeletonArea = areas[9], BodyPart = bodyParts[13] });
            bodyPartMatrixItems.Add(new BodyPartMatrixItem() { SkeletonArea = areas[10], BodyPart = bodyParts[14] });
            bodyPartMatrixItems.Add(new BodyPartMatrixItem() { SkeletonArea = areas[10], BodyPart = bodyParts[15] });
            bodyPartMatrixItems.Add(new BodyPartMatrixItem() { SkeletonArea = areas[11], BodyPart = bodyParts[14] });
            bodyPartMatrixItems.Add(new BodyPartMatrixItem() { SkeletonArea = areas[11], BodyPart = bodyParts[15] });
            bodyPartMatrixItems.Add(new BodyPartMatrixItem() { SkeletonArea = areas[12], BodyPart = bodyParts[16] });
            bodyPartMatrixItems.Add(new BodyPartMatrixItem() { SkeletonArea = areas[13], BodyPart = bodyParts[17] });
            bodyPartMatrixItems.Add(new BodyPartMatrixItem() { SkeletonArea = areas[14], BodyPart = bodyParts[17] });
            bodyPartMatrixItems.Add(new BodyPartMatrixItem() { SkeletonArea = areas[13], BodyPart = bodyParts[18] });
            bodyPartMatrixItems.Add(new BodyPartMatrixItem() { SkeletonArea = areas[14], BodyPart = bodyParts[18] });
            bodyPartMatrixItems.Add(new BodyPartMatrixItem() { SkeletonArea = areas[13], BodyPart = bodyParts[19] });
            bodyPartMatrixItems.Add(new BodyPartMatrixItem() { SkeletonArea = areas[14], BodyPart = bodyParts[19] });
            bodyPartMatrixItems.Add(new BodyPartMatrixItem() { SkeletonArea = areas[15], BodyPart = bodyParts[20] });
            bodyPartMatrixItems.Add(new BodyPartMatrixItem() { SkeletonArea = areas[16], BodyPart = bodyParts[20] });
            bodyPartMatrixItems.Add(new BodyPartMatrixItem() { SkeletonArea = areas[15], BodyPart = bodyParts[19] });
            bodyPartMatrixItems.Add(new BodyPartMatrixItem() { SkeletonArea = areas[16], BodyPart = bodyParts[19] });
            bodyPartMatrixItems.Add(new BodyPartMatrixItem() { SkeletonArea = areas[15], BodyPart = bodyParts[21] });
            bodyPartMatrixItems.Add(new BodyPartMatrixItem() { SkeletonArea = areas[16], BodyPart = bodyParts[21] });
            bodyPartMatrixItems.Add(new BodyPartMatrixItem() { SkeletonArea = areas[17], BodyPart = bodyParts[22] });
            bodyPartMatrixItems.Add(new BodyPartMatrixItem() { SkeletonArea = areas[18], BodyPart = bodyParts[22] });
            bodyPartMatrixItems.Add(new BodyPartMatrixItem() { SkeletonArea = areas[19], BodyPart = bodyParts[23] });
            bodyPartMatrixItems.Add(new BodyPartMatrixItem() { SkeletonArea = areas[20], BodyPart = bodyParts[23] });
            bodyPartMatrixItems.Add(new BodyPartMatrixItem() { SkeletonArea = areas[21], BodyPart = bodyParts[24] });
            bodyPartMatrixItems.Add(new BodyPartMatrixItem() { SkeletonArea = areas[21], BodyPart = bodyParts[25] });
            bodyPartMatrixItems.Add(new BodyPartMatrixItem() { SkeletonArea = areas[22], BodyPart = bodyParts[25] });
            bodyPartMatrixItems.Add(new BodyPartMatrixItem() { SkeletonArea = areas[22], BodyPart = bodyParts[24] });
            bodyPartMatrixItems.Add(new BodyPartMatrixItem() { SkeletonArea = areas[23], BodyPart = bodyParts[2] });
            bodyPartMatrixItems.Add(new BodyPartMatrixItem() { SkeletonArea = areas[23], BodyPart = bodyParts[3] });
            bodyPartMatrixItems.Add(new BodyPartMatrixItem() { SkeletonArea = areas[24], BodyPart = bodyParts[26] });
            bodyPartMatrixItems.Add(new BodyPartMatrixItem() { SkeletonArea = areas[24], BodyPart = bodyParts[27] });
            bodyPartMatrixItems.Add(new BodyPartMatrixItem() { SkeletonArea = areas[25], BodyPart = bodyParts[28] });
            bodyPartMatrixItems.Add(new BodyPartMatrixItem() { SkeletonArea = areas[26], BodyPart = bodyParts[28] });
            bodyPartMatrixItems.Add(new BodyPartMatrixItem() { SkeletonArea = areas[27], BodyPart = bodyParts[29] });
            bodyPartMatrixItems.Add(new BodyPartMatrixItem() { SkeletonArea = areas[28], BodyPart = bodyParts[30] });
            bodyPartMatrixItems.Add(new BodyPartMatrixItem() { SkeletonArea = areas[28], BodyPart = bodyParts[31] });
            bodyPartMatrixItems.Add(new BodyPartMatrixItem() { SkeletonArea = areas[29], BodyPart = bodyParts[31] });
            bodyPartMatrixItems.Add(new BodyPartMatrixItem() { SkeletonArea = areas[29], BodyPart = bodyParts[30] });
            bodyPartMatrixItems.Add(new BodyPartMatrixItem() { SkeletonArea = areas[28], BodyPart = bodyParts[32] });
            bodyPartMatrixItems.Add(new BodyPartMatrixItem() { SkeletonArea = areas[29], BodyPart = bodyParts[32] });
            bodyPartMatrixItems.Add(new BodyPartMatrixItem() { SkeletonArea = areas[30], BodyPart = bodyParts[33] });
            bodyPartMatrixItems.Add(new BodyPartMatrixItem() { SkeletonArea = areas[31], BodyPart = bodyParts[33] });
            bodyPartMatrixItems.Add(new BodyPartMatrixItem() { SkeletonArea = areas[32], BodyPart = bodyParts[34] });
            bodyPartMatrixItems.Add(new BodyPartMatrixItem() { SkeletonArea = areas[33], BodyPart = bodyParts[34] });
            bodyPartMatrixItems.Add(new BodyPartMatrixItem() { SkeletonArea = areas[34], BodyPart = bodyParts[35] });
            bodyPartMatrixItems.Add(new BodyPartMatrixItem() { SkeletonArea = areas[35], BodyPart = bodyParts[35] });
            bodyPartMatrixItems.Add(new BodyPartMatrixItem() { SkeletonArea = areas[34], BodyPart = bodyParts[36] });
            bodyPartMatrixItems.Add(new BodyPartMatrixItem() { SkeletonArea = areas[35], BodyPart = bodyParts[36] });


            bodyPartMatrixItems.ForEach(p => _dbContext.BodyPartMatrix.Add(p));
            _dbContext.SaveChanges();

            return bodyPartMatrixItems;
        }

        private IList<SymptomRenderType> AddRenderTypes()
        {
            var renderTypes = new List<SymptomRenderType>
                {
                    new SymptomRenderType() {DefaultTemplate = "examine.radio.bool", RenderType = "RadioBoolean"},
                    new SymptomRenderType() {DefaultTemplate = "examine.slider.five", RenderType = "FiveScaleSlider"},
                    new SymptomRenderType() {DefaultTemplate = "examine.slider.ten", RenderType = "TenScaleSlider"},
                    new SymptomRenderType() {DefaultTemplate = "examine.duration.dropdown", RenderType = "Dropdown"},
                    new SymptomRenderType() {DefaultTemplate = "examine.feels.multi", RenderType = "MultiSelect"},
                    new SymptomRenderType() {DefaultTemplate = "examine.while.multi", RenderType = "MultiSelect"}
                };

            renderTypes.ForEach(p => _dbContext.SymptomRenderTypes.Add(p));
            _dbContext.SaveChanges();

            return renderTypes;
        } 

        private IEnumerable<Symptom> AddSymptoms(IList<SymptomRenderType> renderTypes)
        {
            var symptoms = new List<Symptom>()
                {
                    new Symptom() { Name = "Swelling", RenderType = renderTypes[0], Description = "Is it swollen? ", ResponseType = SymptomResponseType.Exact },
                    new Symptom() { Name = "Pain", RenderType = renderTypes[2], Description = "Rate level of pain:", ResponseType = SymptomResponseType.EqualAndAboveThreshold},
                    new Symptom() { Name = "Bruising", RenderType = renderTypes[0], Description = "Is it brused? ", ResponseType = SymptomResponseType.Exact},
                    new Symptom() { Name = "Duration", RenderType = renderTypes[3], Description = "Started...", ResponseType = SymptomResponseType.EqualAndAboveThreshold},
                    new Symptom() { Name = "Feels", RenderType = renderTypes[4], Description = "Feels like...", ResponseType = SymptomResponseType.Any},
                    new Symptom() { Name = "While", RenderType = renderTypes[5], Description = "Difficult or painful while...", ResponseType = SymptomResponseType.Any},
                    new Symptom() { Name = "Tender", RenderType = renderTypes[0], Description = "Is it tender to touch or rub? ", ResponseType = SymptomResponseType.Exact}
                };

            symptoms.ForEach(u => _dbContext.Symptoms.Add(u));
            _dbContext.SaveChanges();

            return symptoms;
        }

       
        private void AssociateEquipmentToVendor(IList<Equipment> equipment, IList<Vendor> vendors)
        {
            equipment.ForEach(e => e.Vendors = vendors);
            _dbContext.SaveChanges();
        }
    }
}
