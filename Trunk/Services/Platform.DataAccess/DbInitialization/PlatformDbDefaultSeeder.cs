using System;
using System.Collections.Generic;
using System.Data.Entity;

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

            BuildSymptomMatrix(symptoms, bodyPartMartix);
        }

        private void BuildSymptomMatrix(IEnumerable<Symptom> symptoms, IEnumerable<BodyPartMatrixItem> bodyPartMartix)
        {
            var symptomMatrixItems = new List<SymptomMatrixItem>();
            bodyPartMartix.ForEach(c => symptoms.ForEach(s => symptomMatrixItems.Add(new SymptomMatrixItem {BodyPartMatrixItemId = c.Id, SymptomId = s.Id})));
            symptomMatrixItems.ForEach(s => _dbContext.SymptomMatrix.Add(s));
            _dbContext.SaveChanges();
        }

        private List<User> AddUsers()
        {
            var users = new List<User>()
                {
                    new User() {EmailAddress = "nunya@swpt.com", FirstName = "Fat", LastName = "Jon", Hash = "123456", UserName = "fatty j"},
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


            foreach (var bodyPart in new[] { bodyParts[0], bodyParts[1], bodyParts[9], bodyParts[10] })
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
