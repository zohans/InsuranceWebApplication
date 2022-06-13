﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsuranceWebApplication.Data
{
    public static class DbInitializer
    {
        public static void Initialize(InsuranceDBContext context)
        {
            context.Database.EnsureCreated();

            // Look for any users.
            if (context.Members.Any())
            {
                return;   // DB has been seeded
            }

            var occupationRatings = new OccupationRating[]
            {
                new OccupationRating{Name="Professional", Factor = 1, CreatedDate=DateTime.Now},
                new OccupationRating{Name="White Collar", Factor = 1.25m, CreatedDate=DateTime.Now},
                new OccupationRating{Name="Light Manual", Factor = 1.50m, CreatedDate=DateTime.Now},
                new OccupationRating{Name="Heavy Manual", Factor = 1.75m, CreatedDate=DateTime.Now},
            };

            foreach (OccupationRating e in occupationRatings)
            {
                context.OccupationRatings.Add(e);
            }
            context.SaveChanges();


            var occupations = new Occupation[]
            {
                new Occupation{Name="Cleaner",CreatedDate=DateTime.Now, ModifiedDate=DateTime.Now, OccupationRatingId=1},
            };

            foreach (Occupation e in occupations)
            {
                context.Occupations.Add(e);
            }
            context.SaveChanges();


            var members = new Member[]
            {
            new Member{FirstName="Carson",LastName="Alexander", Age=30, DateOfBirth=new DateTime(2000,10,01), CreatedDate=DateTime.Now, OccupationId=1},
            };
            foreach (Member s in members)
            {
                context.Members.Add(s);
            }
            context.SaveChanges();

        }
    }
}
