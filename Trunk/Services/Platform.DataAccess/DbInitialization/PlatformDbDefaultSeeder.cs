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
            var symptoms = AddSymptoms();
            var bodyPartMartix = AddBodyPartsToAreas(skeletonareas,components);
            var symptomMatrixItems = BuildSymptomMatrix(symptoms, bodyPartMartix);
            var injuries = AddInjuries();

            BuildInjurySymptomMatrix(symptomMatrixItems,injuries);
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
                    new Injury() { CommonName = "Sprained Ankle", Description = "An Ankle strain/sprain is easily one of the most common and easily identified injuries in any sport.  An athlete knows they sprained their ankle the instant they do it and the next couple days are predictable with rehabilitation. This is a bad sentence.  I understand what you’re trying to say, but can you rephrase?  The timetable for return to activity is heavily dependent on severity of the strain or sprain.  It is very important to ICE the ankle within the first 48-72 hours of the sprain.  Consistent icing can have a significant impact in expediting the rehabilitation time. There has to be more information for you to add!!!  What about adding a recommendation to have an X-ray for severe injuries to asure there is not a break????  Also, is there any way to strengthen the tendons and muscles to HELP prevent the injury???  ", MedicalName = "Ankle Sprain", OpeningStatement = "Almost every athlete that plays some type of lateral sport (basketball, soccer, football, etc.) and even runners have experienced some type of ankle strain/sprain. Ankle strain/sprain may be minor to severe."},
                    new Injury() { CommonName = "Shin Splints", Description = "Shin splints can manifest in two different forms, one coming from the muscles in the front of the shin (ankle/toe extensors) and the other coming from the back of the shin (deep toe flexors).  Here we will focus on the anterior shin splints.  The muscles on the front of your shin (anterior tibialis, extensor hallucis and digitorum longus) are responsible for controlling the foot and ankle during the landing phase of running or walking.  With continued use, the muscles continue to experience stress and begin to shorten and form pockets of scar tissue.  If not take care of properly, this process will continue and eventually the athlete will begin to experience pain and tightness in the front of the shin.  As symptoms progress, they can eventually lead to difficulty with walking and running activities.  Anterior shin splints can be prevented with regular self -massage, stretching, and minor activity modification.", MedicalName = "Anterior Tbialis", OpeningStatement = "Suffering from shin splints?  Many athletes have suffered from some form of shin splints at some point in there training.  Shin splints can be very painful and can sideline the most veteran and toughest athletes."},
                    new Injury() { CommonName = "Plantar Faciitis", Description = "Plantar Fasciitis is the name given to describe the inflammatory condition of the Plantar Fascia, the thick band of ligament that runs along the bottom of the foot and connects the heel bone to the toes. This tissue provides support to the arch of the foot.  With each step, the plantar fascia undergoes a moderate stretching tension as the arch compresses. Plantar Fasciitis occurs when a repeatedly excessive stretch causes small tears in the plantar fascia.  There are a variety of causes for plantar fasciitis.  As we age, our bodies adapt to our lifestyles, and asymmetries arise between the left and right sides of our body.  Many people notice they are stronger on one side or more flexible on the other side.  This natural adaptation to your lifestyle may contribute to this injury affecting only one of your feet.  Commonly experienced by men and women in their middle ages, the condition may also affect young people, especially recreational athletes, such as runners. ", MedicalName = "Plantar Faciitis", OpeningStatement = "Plantar fasciitis is the most common cause of heel pain.  Those affected by this condition most often describe a stiffness or sharp pain in their heel or along the bottom of their foot."},
                    new Injury() { CommonName = "Calf Strain", Description = "The calf is constructed of three main structures: Gastrocnemius, Soleus, and Achilles tendon.  This unit is vital in pushing off to running, jumping, pedaling, and any exercise that involves pushing off with the legs.  A calf strain occurs when a portion of the muscle is contracted to forcefully during activity and goes into spasm or 'freaks out'.  This is known as the 'oh crap' moment.  When this event occurs, the athlete will experience sharp pain each time the muscle is contracted, usually with pushing off during running and walking.  The injured area needs to go through the healing process, which is usually between 2-4 weeks (although can be much longer with severe strains).  Light stretching and light soft tissue work can dramatically increase the healing process and get the athlete back to training sooner.", MedicalName = "Gastrocnemius/Soleus Strain", OpeningStatement = "Suffering from calf strain?  A calf strain will make it hard for any person to walk, run, or complete any exercise that involves pushing off with the legs.  Some athletes may be able to push thorough the pain and continue exercise.  Others may not be able to fight through the pain.  Calf strains typically happen to athletes during high intensity workouts or races. "},
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

            foreach (var bodyPart in new[] { bodyParts[0], bodyParts[1], bodyParts[9], bodyParts[10] })
            {
                var matrixItem = new BodyPartMatrixItem
                {
                    SkeletonArea = areas[19],
                    BodyPart = bodyPart
                };
                bodyPartMatrixItems.Add(matrixItem);
            }


            foreach (var bodyPart in new[] { bodyParts[0], bodyParts[1], bodyParts[11] })
            {
                var matrixItem = new BodyPartMatrixItem
                {
                    SkeletonArea = areas[20],
                    BodyPart = bodyPart
                };
                bodyPartMatrixItems.Add(matrixItem);
            }


            bodyPartMatrixItems.ForEach(p => _dbContext.BodyPartMatrix.Add(p));
            _dbContext.SaveChanges();

            return bodyPartMatrixItems;
        }

        private IEnumerable<Symptom> AddSymptoms()
        {
            var symptoms = new List<Symptom>()
                {
                    new Symptom() { Name = "Swelling", RenderType = SymptomRenderType.Slider, Description = "Puffy as shit"},
                    new Symptom() { Name = "Pain", RenderType = SymptomRenderType.Slider, Description = "Hurts like shit"},
                    new Symptom() { Name = "Bruising", RenderType = SymptomRenderType.Slider, Description = "Looks like shit"}
                };

            symptoms.ForEach(u => _dbContext.Symptoms.Add(u));
            _dbContext.SaveChanges();

            return symptoms;
        }

    }
}
