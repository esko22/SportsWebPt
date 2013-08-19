using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

using SportsWebPt.Common.DataAccess.Ef;
using SportsWebPt.Common.Utilities;
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

            var users = AddUsers();
            var components = AddComponents();
            var regions = AddRegions();
            var orientations = AddOrientations();
            var sides = AddSides();
            var skeletonareas = AddSkeletonAreas(regions, sides, orientations);
            var renderTypes = AddRenderTypes();
            var symptoms = AddSymptoms(renderTypes);
            var bodyPartMartix = AddBodyPartsToAreas(skeletonareas,components);
            var symptomMatrixItems = BuildSymptomMatrix(symptoms, bodyPartMartix);
            var injuries = AddInjuries();
            var equipment = AddEquipment();
            var videos = AddVideos();
            var exercises = AddExercises();
            var causes = AddCauses();
            var signs = AddSigns();
            var plans = AddPlans();

            BuildInjurySymptomMatrix(symptomMatrixItems,injuries);
            AssociateInjuryCause(causes, injuries);
            AssociateInjurySigns(signs, injuries);
            AssociateInjuryPlans(injuries, plans);
            AssociatePlanExercise(plans, exercises);
            AssociateExerciseVideos(exercises, videos);
            AssociateExerciseEquipment(exercises, equipment);
            AssociateExerciseBodyRegion(exercises, regions);
            AssociatePlanBodyRegion(plans, regions);
            AssociateInjuryBodyRegion(injuries, regions);

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

        private List<Cause> AddCauses()
        {
            var causes = new List<Cause>()
                {
                    new Cause() {Description = "Repetitive stress"},
                    new Cause() {Description = "Accelerating"},
                    new Cause() {Description = "Sudden increase in training volume"},
                    new Cause() {Description = "Speed Workouts"},
                    new Cause() {Description = "Running"},
                    new Cause() {Description = "Overweight"}
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

        private List<Sign> AddSigns()
        {
            var signs = new List<Sign>()
                {
                    new Sign() { Category = SignCategory.Functional, Description = "Painful to jump"},
                    new Sign() { Category = SignCategory.Visual, Description = "Love Handles"},
                    new Sign() { Category = SignCategory.Functional, Description = "Limited push-off"},
                    new Sign() { Category = SignCategory.Subjective, Description = "Throbing"},
                    new Sign() { Category = SignCategory.Subjective, Description = "Stiffness"},
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
                            MusclesInvolved = "Plantar Fascia, Gastroc/Soleus",
                            Category = PlanCategory.Rehabilitation,
                            Tags = "Pain, Swelling",
                            PageName = "Plantar-Fascia-Rehab-1"
                        },
                     new Plan()
                        {
                            RoutineName = "Sprained Ankle Stretching",
                            Description = "This program is designed to stretch and help mobilize the ankle after an ankle sprain",
                            Duration = 5,
                            MusclesInvolved = "Ankle Joint/Ligaments, Soleus",
                            Category = PlanCategory.Stretch,
                            Tags = "Pain, Weakness",
                            PageName = "Sprained-Ankle-Stretching"
                        },
                     new Plan()
                        {
                            RoutineName = "General Shin Splint Stretching",
                            Description = "This program is designed for stretching the muscles that lead to shin splints if they become overly tight. ",
                            Duration = 5,
                            MusclesInvolved = "Ankle/Toe Extensors, Deep Toe Flexors, gastrocnemius, soleus",
                            Category = PlanCategory.Stretch,
                            Tags = "Pain, Weakness",
                            PageName = "General-Shin-Splint-Stretching"
                        },
                     new Plan()
                        {
                            RoutineName = "Soleus Stretching",
                            Description = "This program is designed for athletes to stretch their soleus muscle.",
                            Duration = 5,
                            MusclesInvolved = "Soleus",
                            Category = PlanCategory.Stretch,
                            Tags = "Pain, Weakness",
                            PageName = "Soleus-Stretching"
                        },
                     new Plan()
                        {
                            RoutineName = "Calf Strain Rehab",
                            Description = "This program is designed to be used for athletes with an acute calf muscle.",
                            Duration = 5,
                            MusclesInvolved = "Gastrocnemius, Soleus, Hamstrings",
                            Category = PlanCategory.Rehabilitation,
                            Tags = "Pain, Tingle",
                            PageName = "Calf-Strain-Rehab"
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
                    new PlanExerciseMatrixItem() {Plan = plans[0], Exercise = exercises[0], Repititions = 3, Sets = 2, PerDay = 1, PerWeek = 3},
                    new PlanExerciseMatrixItem() {Plan = plans[0], Exercise = exercises[1], Repititions = 2, Sets = 7, PerDay = 2, PerWeek = 1},
                    new PlanExerciseMatrixItem() {Plan = plans[1], Exercise = exercises[0], Repititions = 4, Sets = 9, PerDay = 1, PerWeek = 3},
                    new PlanExerciseMatrixItem() {Plan = plans[2], Exercise = exercises[3], Repititions = 7, Sets = 2, PerDay = 5, PerWeek = 3},
                    new PlanExerciseMatrixItem() {Plan = plans[3], Exercise = exercises[2], Repititions = 2, Sets = 1, PerDay = 6, PerWeek = 2},
                    new PlanExerciseMatrixItem() { Plan = plans[3], Exercise = exercises[3], Repititions = 3, Sets = 1, PerDay = 1, PerWeek = 7},
                    new PlanExerciseMatrixItem() { Plan = plans[3], Exercise = exercises[4], Repititions = 5, Sets = 2, PerDay = 8, PerWeek = 6},
                    new PlanExerciseMatrixItem() { Plan = plans[4], Exercise = exercises[4], Repititions = 3, Sets = 2, PerDay = 4, PerWeek = 3},
                    new PlanExerciseMatrixItem() { Plan = plans[4], Exercise = exercises[5], Repititions = 2, Sets = 8, PerDay = 6, PerWeek = 2},
                    new PlanExerciseMatrixItem() { Plan = plans[4], Exercise = exercises[6], Repititions = 30, Sets = 2, PerDay = 4, PerWeek = 5}
                };

            planExercises.ForEach(p => _dbContext.PlansExceriseMatrixItems.Add(p));
            _dbContext.SaveChanges();

        }

        private List<Exercise> AddExercises()
        {
            var exercises = new List<Exercise>()
                {
                    new Exercise() {Name = "Heel Self Massage", Description = "Touch yo self", Difficulty = ExerciseDifficulty.Beginner, Duration = 5,Tags = "pain, tightness", PageName = "heel-self-massage"},
                    new Exercise() {Name = "Seated Plantar Fascis Stretch", Description = "Strech yo self", Difficulty = ExerciseDifficulty.Advanced, Duration = 10,Tags = "pain, tightness", PageName = "seated-plantar-fascis-strech"},
                    new Exercise() {Name = "Standing calf Stretch", Difficulty = ExerciseDifficulty.Beginner, Duration = 5,Tags = "pain, tightness", PageName = "standing-calf-strech"},
                    new Exercise() {Name = "Standing Wall Calf Stretch", Difficulty = ExerciseDifficulty.Intermediate, Duration = 3,Tags = "pain, tightness", PageName = "standing-wall-calf-strech"},
                    new Exercise() {Name = "Standing Soleus Stretch 1", Difficulty = ExerciseDifficulty.Beginner, Duration = 5, Tags = "pain, tightness", PageName = "standing-soleus-strech-1" },
                    new Exercise() {Name = "Standing Soleus Stretch 2", Difficulty = ExerciseDifficulty.Advanced, Duration = 7, Tags = "pain, tightness", PageName = "standing-soleus-strech-2"},
                    new Exercise() {Name = "Calf Knee Massage", Description = "Touch yo self", Difficulty = ExerciseDifficulty.Beginner, Duration = 5, Tags = "pain, tightness", PageName = "calf-knee-massage" }
                };

            exercises.ForEach(p => _dbContext.Exercises.Add(p));
            _dbContext.SaveChanges();

            return exercises;
        }

        private List<Video> AddVideos()
        {
            var videos = new List<Video>()
                {
                    new Video() {CreationDate = DateTime.Now, Name = "Calf Raises", Filename = "SWPT_Template.m4v", Description = "How to raise your calfs"},
                    new Video() {CreationDate = DateTime.Now, Name = "Shin Stretch", Filename = "SWPT_Template.m4v", Description = "How to strech your shins"},
                    new Video() {CreationDate = DateTime.Now, Name = "Ankle Strength", Filename = "SWPT_Template.m4v", Description = "How to strengthen your ankles"},
                    new Video() {CreationDate = DateTime.Now, Name = "Toe Stretch", Filename = "SWPT_Template.m4v", Description = "How to strech your toes"},
                    new Video() {CreationDate = DateTime.Now, Name = "Heel self message", Filename = "SWPT_Template.m4v", Description = "How to touch yourself"}
                };

            videos.ForEach(p => _dbContext.Videos.Add(p));
            _dbContext.SaveChanges();

            return videos;
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



        private List<Equipment> AddEquipment()
        {
            var equipmentList = new List<Equipment>()
                {
                    new Equipment()
                        {
                            CommonName = "Foam Roller",
                            TechnicalName = "Roller de Foame",
                            PriceRange = "10 - 50",
                            RecommendedVendor = "Foam House"
                        },
                    new Equipment()
                        {
                            CommonName = "Theracane",
                            TechnicalName = "Theracane",
                            PriceRange = "20 - 60",
                            RecommendedVendor = "ActiveLife"
                        },
                    new Equipment() {CommonName = "Baseball", TechnicalName = "Baseball", PriceRange = "2 - 7"},
                    new Equipment() {CommonName = "Wall", TechnicalName = "Wall", PriceRange = "0"}
                };

            equipmentList.ForEach(p => _dbContext.Equipment.Add(p));
            _dbContext.SaveChanges();

            return equipmentList;
        }

        private List<SymptomMatrixItem> BuildSymptomMatrix(IEnumerable<Symptom> symptoms, IEnumerable<BodyPartMatrixItem> bodyPartMartix)
        {
            var symptomMatrixItems = new List<SymptomMatrixItem>();
            bodyPartMartix.ForEach(c => symptoms.ForEach(s => symptomMatrixItems.Add(new SymptomMatrixItem {BodyPartMatrixItemId = c.Id, SymptomId = s.Id})));
            symptomMatrixItems.ForEach(s => _dbContext.SymptomMatrix.Add(s));
            _dbContext.SaveChanges();

            return symptomMatrixItems;
        }

        private void BuildInjurySymptomMatrix(IList<SymptomMatrixItem> symptomMatrixItems, IList<Injury> injuries)
        {
            var injurySymptomMatrixItems = new List<InjurySymptomMatrixItem>();

            symptomMatrixItems.Where(s => s.BodyPartMatrixItem.BodyPart.CommonName.Equals("Ankle")).ForEach(p => injurySymptomMatrixItems.Add(new InjurySymptomMatrixItem()
                {
                    Injury = injuries[0],
                    SymptomMatrixItem = p,
                    ThresholdValue = 1
                }));

            symptomMatrixItems.Where(s => s.BodyPartMatrixItem.BodyPart.CommonName.Equals("Shin")).ForEach(p => injurySymptomMatrixItems.Add(new InjurySymptomMatrixItem()
            {
                Injury = injuries[1],
                SymptomMatrixItem = p,
                ThresholdValue = 1
            }));

            symptomMatrixItems.Where(s => s.BodyPartMatrixItem.BodyPart.CommonName.Equals("Foot")).ForEach(p => injurySymptomMatrixItems.Add(new InjurySymptomMatrixItem()
            {
                Injury = injuries[2],
                SymptomMatrixItem = p,
                ThresholdValue = 5
            }));

            symptomMatrixItems.Where(s => s.BodyPartMatrixItem.BodyPart.CommonName.Equals("Foot")).ForEach(p => injurySymptomMatrixItems.Add(new InjurySymptomMatrixItem()
            {
                Injury = injuries[0],
                SymptomMatrixItem = p,
                ThresholdValue = 1
            }));

            symptomMatrixItems.Where(s => s.BodyPartMatrixItem.BodyPart.CommonName.Equals("Calf")).ForEach(p => injurySymptomMatrixItems.Add(new InjurySymptomMatrixItem()
            {
                Injury = injuries[3],
                SymptomMatrixItem = p,
                ThresholdValue = 1
            }));


            injurySymptomMatrixItems.ForEach(p => _dbContext.InjurySymptomMatrix.Add(p));
            _dbContext.SaveChanges();
        }

        private List<Injury> AddInjuries()
        {
            var injuries = new List<Injury>()
                {
                    new Injury()
                        {
                            Tags = "Pain, Tingle",
                            PageName = "Sprained-Ankle",
                            CommonName = "Sprained Ankle",
                            Description =
                                "An Ankle strain/sprain is easily one of the most common and easily identified injuries in any sport.  An athlete knows they sprained their ankle the instant they do it and the next couple days are predictable with rehabilitation. This is a bad sentence.  I understand what you’re trying to say, but can you rephrase?  The timetable for return to activity is heavily dependent on severity of the strain or sprain.  It is very important to ICE the ankle within the first 48-72 hours of the sprain.  Consistent icing can have a significant impact in expediting the rehabilitation time. There has to be more information for you to add!!!  What about adding a recommendation to have an X-ray for severe injuries to asure there is not a break????  Also, is there any way to strengthen the tendons and muscles to HELP prevent the injury???  ",
                            MedicalName = "Ankle Sprain",
                            OpeningStatement =
                                "Almost every athlete that plays some type of lateral sport (basketball, soccer, football, etc.) and even runners have experienced some type of ankle strain/sprain. Ankle strain/sprain may be minor to severe."
                        },
                    new Injury()
                        {                            
                            Tags = "Pain, Bruise",
                            PageName = "Shin-Splints",
                            CommonName = "Shin Splints",
                            Description =
                                "Shin splints can manifest in two different forms, one coming from the muscles in the front of the shin (ankle/toe extensors) and the other coming from the back of the shin (deep toe flexors).  Here we will focus on the anterior shin splints.  The muscles on the front of your shin (anterior tibialis, extensor hallucis and digitorum longus) are responsible for controlling the foot and ankle during the landing phase of running or walking.  With continued use, the muscles continue to experience stress and begin to shorten and form pockets of scar tissue.  If not take care of properly, this process will continue and eventually the athlete will begin to experience pain and tightness in the front of the shin.  As symptoms progress, they can eventually lead to difficulty with walking and running activities.  Anterior shin splints can be prevented with regular self -massage, stretching, and minor activity modification.",
                            MedicalName = "Anterior Tbialis",
                            OpeningStatement =
                                "Suffering from shin splints?  Many athletes have suffered from some form of shin splints at some point in there training.  Shin splints can be very painful and can sideline the most veteran and toughest athletes."
                        },
                    new Injury()
                        {
                            Tags = "Pain, Pressure",
                            PageName = "Plantar-Faciitis",
                            CommonName = "Plantar Faciitis",
                            Description =
                                "Plantar Fasciitis is the name given to describe the inflammatory condition of the Plantar Fascia, the thick band of ligament that runs along the bottom of the foot and connects the heel bone to the toes. This tissue provides support to the arch of the foot.  With each step, the plantar fascia undergoes a moderate stretching tension as the arch compresses. Plantar Fasciitis occurs when a repeatedly excessive stretch causes small tears in the plantar fascia.  There are a variety of causes for plantar fasciitis.  As we age, our bodies adapt to our lifestyles, and asymmetries arise between the left and right sides of our body.  Many people notice they are stronger on one side or more flexible on the other side.  This natural adaptation to your lifestyle may contribute to this injury affecting only one of your feet.  Commonly experienced by men and women in their middle ages, the condition may also affect young people, especially recreational athletes, such as runners. ",
                            MedicalName = "Plantar Faciitis",
                            OpeningStatement =
                                "Plantar fasciitis is the most common cause of heel pain.  Those affected by this condition most often describe a stiffness or sharp pain in their heel or along the bottom of their foot."
                        },
                    new Injury()
                        {
                            Tags = "Pain, Pinch",
                            PageName = "Calf-Strain",
                            CommonName = "Calf Strain",
                            Description =
                                "The calf is constructed of three main structures: Gastrocnemius, Soleus, and Achilles tendon.  This unit is vital in pushing off to running, jumping, pedaling, and any exercise that involves pushing off with the legs.  A calf strain occurs when a portion of the muscle is contracted to forcefully during activity and goes into spasm or 'freaks out'.  This is known as the 'oh crap' moment.  When this event occurs, the athlete will experience sharp pain each time the muscle is contracted, usually with pushing off during running and walking.  The injured area needs to go through the healing process, which is usually between 2-4 weeks (although can be much longer with severe strains).  Light stretching and light soft tissue work can dramatically increase the healing process and get the athlete back to training sooner.",
                            MedicalName = "Gastrocnemius/Soleus Strain",
                            OpeningStatement =
                                "Suffering from calf strain?  A calf strain will make it hard for any person to walk, run, or complete any exercise that involves pushing off with the legs.  Some athletes may be able to push thorough the pain and continue exercise.  Others may not be able to fight through the pain.  Calf strains typically happen to athletes during high intensity workouts or races. "
                        },
                };

            injuries.ForEach(p => _dbContext.Injuries.Add(p));
            _dbContext.SaveChanges();

            return injuries;
        }

        private List<User> AddUsers()
        {
            var users = new List<User>()
                {
                    new User() {EmailAddress = "kdot@swpt.com", FirstName = "Kendrick", LastName = "Lamar", Hash = "123456", UserName = "k dot"},
                    new User() {EmailAddress = "anut@swpt.com", FirstName = "Alexander", LastName = "Nut", Hash = "123456", UserName = "anut"},
                    new User() {EmailAddress = "jdee@swpt.com", FirstName = "Jay", LastName = "Dee", Hash = "123456", UserName = "jdilla"}
                };

            users.ForEach(u => _dbContext.Users.Add(u));
            _dbContext.SaveChanges();

            return users;
        }

        private List<BodyPart> AddComponents()
        {
            var parts = new List<BodyPart>()
                {
                    new BodyPart() {CommonName = "Foot", ScientificName = "FancyFoot"},
                    new BodyPart() {CommonName = "Ankle"},
                    new BodyPart() {CommonName = "Wrist"},
                    new BodyPart() {CommonName = "Hamstring"},
                    new BodyPart() {CommonName = "Quad"},
                    new BodyPart() {CommonName = "Neck"},
                    new BodyPart() {CommonName = "Throat"},
                    new BodyPart() {CommonName = "Shoulder"},
                    new BodyPart() {CommonName = "Elbow"},
                    new BodyPart() {CommonName = "Knee"},
                    new BodyPart() {CommonName = "Shin"},
                    new BodyPart() {CommonName = "Calf"},
                    new BodyPart() {CommonName = "Hip"},
                    new BodyPart() {CommonName = "Bicep"},
                    new BodyPart() {CommonName = "Tricep"}
                };

            parts.ForEach(u => _dbContext.BodyParts.Add(u));
            _dbContext.SaveChanges();

            return parts;
        }

        private List<BodyRegion> AddRegions()
        {
            var regions = new List<BodyRegion>()
                {
                    new BodyRegion() { Name = "AboveShoulder"},
                    new BodyRegion() { Name = "Chest"},
                    new BodyRegion() { Name = "Shoulders"},
                    new BodyRegion() { Name = "Core"},
                    new BodyRegion() { Name = "LowerBack"},
                    new BodyRegion() { Name = "UpperArm"},
                    new BodyRegion() { Name = "LowerArm"},
                    new BodyRegion() { Name = "UpperLeg"},
                    new BodyRegion() { Name = "LowerLeg"},
                    new BodyRegion() { Name = "Groin"},
                    new BodyRegion() { Name = "Butt"}
                };

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
                    new SkeletonArea() { Orientation = orientations[0], Region = regionTypes[0], Side = sides[2]},
                    new SkeletonArea() { Orientation = orientations[2], Region = regionTypes[0], Side = sides[2]},
                    new SkeletonArea() { Orientation = orientations[0], Region = regionTypes[1], Side = sides[2]},
                    new SkeletonArea() { Orientation = orientations[2], Region = regionTypes[2], Side = sides[2]},
                    new SkeletonArea() { Orientation = orientations[0], Region = regionTypes[3], Side = sides[2]},
                    new SkeletonArea() { Orientation = orientations[2], Region = regionTypes[4], Side = sides[2]},
                    new SkeletonArea() { Orientation = orientations[0], Region = regionTypes[5], Side = sides[0]},
                    new SkeletonArea() { Orientation = orientations[0], Region = regionTypes[5], Side = sides[1]},
                    new SkeletonArea() { Orientation = orientations[2], Region = regionTypes[5], Side = sides[0]},
                    new SkeletonArea() { Orientation = orientations[2], Region = regionTypes[5], Side = sides[1]},
                    new SkeletonArea() { Orientation = orientations[0], Region = regionTypes[6], Side = sides[0]},
                    new SkeletonArea() { Orientation = orientations[0], Region = regionTypes[6], Side = sides[1]},
                    new SkeletonArea() { Orientation = orientations[2], Region = regionTypes[6], Side = sides[0]},
                    new SkeletonArea() { Orientation = orientations[2], Region = regionTypes[6], Side = sides[1]},
                    new SkeletonArea() { Orientation = orientations[0], Region = regionTypes[7], Side = sides[0]},
                    new SkeletonArea() { Orientation = orientations[0], Region = regionTypes[7], Side = sides[1]},
                    new SkeletonArea() { Orientation = orientations[2], Region = regionTypes[7], Side = sides[0]},
                    new SkeletonArea() { Orientation = orientations[2], Region = regionTypes[7], Side = sides[1]},
                    new SkeletonArea() { Orientation = orientations[0], Region = regionTypes[8], Side = sides[0]},
                    new SkeletonArea() { Orientation = orientations[0], Region = regionTypes[8], Side = sides[1]},
                    new SkeletonArea() { Orientation = orientations[2], Region = regionTypes[8], Side = sides[0]},
                    new SkeletonArea() { Orientation = orientations[2], Region = regionTypes[8], Side = sides[1]},
                    new SkeletonArea() { Orientation = orientations[0], Region = regionTypes[9], Side = sides[2]},
                    new SkeletonArea() { Orientation = orientations[2], Region = regionTypes[10], Side = sides[2]}
                };

            areas.ForEach(u => _dbContext.SkeletonAreas.Add(u));
            _dbContext.SaveChanges();

            return areas;
        }

        private IEnumerable<BodyPartMatrixItem> AddBodyPartsToAreas(IList<SkeletonArea> areas, IList<BodyPart> bodyParts)
        {
            var bodyPartMatrixItems = new List<BodyPartMatrixItem>();

            foreach (var bodyPart in new [] { bodyParts[0], bodyParts[1],bodyParts[9],bodyParts[10]})
            {
                var matrixItem = new BodyPartMatrixItem
                    {
                        SkeletonArea = areas[18],
                        BodyPart = bodyPart
                    };
                bodyPartMatrixItems.Add(matrixItem);
            }

            bodyPartMatrixItems.Last().IsSecondary = true;
            

            foreach (var bodyPart in new[] { bodyParts[0], bodyParts[1], bodyParts[9], bodyParts[10] })
            {
                var matrixItem = new BodyPartMatrixItem
                {
                    SkeletonArea = areas[19],
                    BodyPart = bodyPart
                };
                bodyPartMatrixItems.Add(matrixItem);
            }

            bodyPartMatrixItems.Last().IsSecondary = true;


            foreach (var bodyPart in new[] { bodyParts[0], bodyParts[1], bodyParts[11] })
            {
                var matrixItem = new BodyPartMatrixItem
                {
                    SkeletonArea = areas[20],
                    BodyPart = bodyPart
                };
                bodyPartMatrixItems.Add(matrixItem);
            }
            bodyPartMatrixItems.Last().IsSecondary = true;


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
                    new SymptomRenderType() {DefaultTemplate = "examine.dropdown", RenderType = "Dropdown"}
                };

            renderTypes.ForEach(p => _dbContext.SymptomRenderTypes.Add(p));
            _dbContext.SaveChanges();

            return renderTypes;
        } 

        private IEnumerable<Symptom> AddSymptoms(IList<SymptomRenderType> renderTypes)
        {
            var symptoms = new List<Symptom>()
                {
                    new Symptom() { Name = "Swelling", RenderType = renderTypes[1], Description = "Rate level of swelling"},
                    new Symptom() { Name = "Pain", RenderType = renderTypes[2], Description = "Rate level of pain"},
                    new Symptom() { Name = "Bruising", RenderType = renderTypes[0], Description = "Visible Brusing"},
                    new Symptom() { Name = "Duration", RenderType = renderTypes[3], Description = "Started"}
                };

            symptoms.ForEach(u => _dbContext.Symptoms.Add(u));
            _dbContext.SaveChanges();

            return symptoms;
        }

    }
}
