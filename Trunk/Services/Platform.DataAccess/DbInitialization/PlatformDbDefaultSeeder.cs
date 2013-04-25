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

            AddComponentsToAreas(skeletonareas,components);
            AddSymptomsToComponents(symptoms, components);
        }

        private void AddSymptomsToComponents(IEnumerable<Symptom> symptoms, List<AreaComponent> components)
        {
            components.ForEach(c => c.Symptoms = new List<Symptom>(symptoms));
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

        private List<AreaComponent> AddComponents()
        {
            var parts = new List<AreaComponent>()
                {
                    new AreaComponent() {CommonName = "Foot", ScientificName = "FancyFoot"},
                    new AreaComponent() {CommonName = "Ankle"},
                    new AreaComponent() {CommonName = "Wrist"},
                    new AreaComponent() {CommonName = "Hamstring"},
                    new AreaComponent() {CommonName = "Quad"},
                    new AreaComponent() {CommonName = "Neck"},
                    new AreaComponent() {CommonName = "Throat"},
                    new AreaComponent() {CommonName = "Shoulder"},
                    new AreaComponent() {CommonName = "Elbow"},
                    new AreaComponent() {CommonName = "Knee"},
                    new AreaComponent() {CommonName = "Shin"},
                    new AreaComponent() {CommonName = "Calf"},
                    new AreaComponent() {CommonName = "Hip"},
                    new AreaComponent() {CommonName = "Bicep"},
                    new AreaComponent() {CommonName = "Tricep"}
                };

            parts.ForEach(u => _dbContext.AreaComponents.Add(u));
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

            regions.ForEach(u => _dbContext.RegionTypes.Add(u));
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

            orientations.ForEach(u => _dbContext.OrientationTypes.Add(u));
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

            sides.ForEach(u => _dbContext.SideTypes.Add(u));
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

        private void AddComponentsToAreas(IList<SkeletonArea> areas, IList<AreaComponent> components)
        {
            var area = areas[18];
            area.Components =  new [] { components[0], components[1],components[9],components[10]};

            area = areas[19];
            area.Components = new[] { components[0], components[1], components[9], components[10]};

            area = areas[20];
            area.Components = new[] { components[0], components[1], components[9], components[10] };


            _dbContext.SaveChanges();

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
