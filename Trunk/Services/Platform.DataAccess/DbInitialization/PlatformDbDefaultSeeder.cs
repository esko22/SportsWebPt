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
        }

        private List<User> AddUsers()
        {
            var users = new List<User>()
                {
                    new User() {EmailAddress = "nunya@swpt.com", FirstName = "Fat", LastName = "Jon", Password = "123456", UserName = "fatty j"},
                    new User() {EmailAddress = "anut@swpt.com", FirstName = "Alexander", LastName = "Nut", Password = "123456", UserName = "anut"},
                    new User() {EmailAddress = "jdee@swpt.com", FirstName = "Jay", LastName = "Dee", Password = "123456", UserName = "jdilla"}
                };

            users.ForEach(u => _dbContext.Users.Add(u));
            _dbContext.SaveChanges();

            return users;
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
