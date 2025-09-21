using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RawPrj.Domain.Entities;

namespace RawPrj.Data.Contexts
{
    public class TravelDbContext : DbContext
    {

        public TravelDbContext(DbContextOptions<TravelDbContext> options) : base(options)
        { }


        public DbSet<TourList> tourLists { get; set; }
        public DbSet<TourPackage> tourPackages { get; set; }

    }
}





