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
            var parts = AddParts();
            var regions = AddRegions();
            var orientations = AddOrientations();
            var sides = AddSides();
            var skeletonHotspots = AddSkeletonHotspots(regions, sides, orientations);
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

        private List<BodyPart> AddParts()
        {
            var parts = new List<BodyPart>()
                {
                    new BodyPart() {CommonName = "Foot", ScientificName = "FancyFoot"},
                    new BodyPart() {CommonName = "Ankle"},
                    new BodyPart() {CommonName = "Wrist"},
                    new BodyPart() {CommonName = "Hamstring"},
                };

            parts.ForEach(u => _dbContext.PartTypes.Add(u));
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

        private List<SkeletonHotspot> AddSkeletonHotspots(List<BodyRegion> regionTypes,List<Side> sides,List<Orientation> orientations)
        {
            var hotspots = new List<SkeletonHotspot>()
                {
                    new SkeletonHotspot() { Orientation = orientations[0], Region = regionTypes[0], Side = sides[2]},
                    new SkeletonHotspot() { Orientation = orientations[2], Region = regionTypes[0], Side = sides[2]},
                    new SkeletonHotspot() { Orientation = orientations[0], Region = regionTypes[1], Side = sides[2]},
                    new SkeletonHotspot() { Orientation = orientations[2], Region = regionTypes[2], Side = sides[2]},
                    new SkeletonHotspot() { Orientation = orientations[0], Region = regionTypes[3], Side = sides[2]},
                    new SkeletonHotspot() { Orientation = orientations[2], Region = regionTypes[4], Side = sides[2]},
                    new SkeletonHotspot() { Orientation = orientations[0], Region = regionTypes[5], Side = sides[0]},
                    new SkeletonHotspot() { Orientation = orientations[0], Region = regionTypes[5], Side = sides[1]},
                    new SkeletonHotspot() { Orientation = orientations[2], Region = regionTypes[5], Side = sides[0]},
                    new SkeletonHotspot() { Orientation = orientations[2], Region = regionTypes[5], Side = sides[1]},
                    new SkeletonHotspot() { Orientation = orientations[0], Region = regionTypes[6], Side = sides[0]},
                    new SkeletonHotspot() { Orientation = orientations[0], Region = regionTypes[6], Side = sides[1]},
                    new SkeletonHotspot() { Orientation = orientations[2], Region = regionTypes[6], Side = sides[0]},
                    new SkeletonHotspot() { Orientation = orientations[2], Region = regionTypes[6], Side = sides[1]},
                    new SkeletonHotspot() { Orientation = orientations[0], Region = regionTypes[7], Side = sides[0]},
                    new SkeletonHotspot() { Orientation = orientations[0], Region = regionTypes[7], Side = sides[1]},
                    new SkeletonHotspot() { Orientation = orientations[2], Region = regionTypes[7], Side = sides[0]},
                    new SkeletonHotspot() { Orientation = orientations[2], Region = regionTypes[7], Side = sides[1]},
                    new SkeletonHotspot() { Orientation = orientations[0], Region = regionTypes[8], Side = sides[0]},
                    new SkeletonHotspot() { Orientation = orientations[0], Region = regionTypes[8], Side = sides[1]},
                    new SkeletonHotspot() { Orientation = orientations[2], Region = regionTypes[8], Side = sides[0]},
                    new SkeletonHotspot() { Orientation = orientations[2], Region = regionTypes[8], Side = sides[1]},
                    new SkeletonHotspot() { Orientation = orientations[0], Region = regionTypes[9], Side = sides[2]},
                    new SkeletonHotspot() { Orientation = orientations[2], Region = regionTypes[10], Side = sides[2]}
                };

            hotspots.ForEach(u => _dbContext.SkeletonHotspots.Add(u));
            _dbContext.SaveChanges();

            return hotspots;
        }

        //HOW TO DO RELATIONSHIP DATA
        //private List<ReferenceGenome> AddReferenceGenomes(WatsonDbContext context, List<Species> species, List<ReferenceProvider> provdiders)
        //{
        //    var refGenomes = new List<ReferenceGenome>()
        //        {
        //            new ReferenceGenome() {GenomeBuild = "HG19", Species = species[0], Provider = provdiders[1]},
        //            new ReferenceGenome() {GenomeBuild = "37.1", Species = species[0], Provider = provdiders[0]}
        //        };

        //    refGenomes.ForEach(p => context.ReferenceGenomes.Add(p));
        //    context.SaveChanges();

        //    return refGenomes;
        //}

    }
}
