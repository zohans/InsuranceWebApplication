using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsuranceWebApplication.Data
{
    public class InsuranceDBContext : DbContext
    {
        public InsuranceDBContext(DbContextOptions<InsuranceDBContext> options) : base(options)
        {
        }

        public DbSet<Member> Members { get; set; }

        public DbSet<Occupation> Occupations { get; set; }

        public DbSet<OccupationRating> OccupationRatings { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //}
    }
}
