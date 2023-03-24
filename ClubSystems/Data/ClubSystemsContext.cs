using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ClubSystems.Models;

namespace ClubSystems.Data
{
    public class ClubSystemsContext : DbContext
    {
        public ClubSystemsContext (DbContextOptions<ClubSystemsContext> options)
            : base(options)
        {
        }

        public DbSet<ClubSystems.Models.Person> Person { get; set; } = default!;

        public DbSet<ClubSystems.Models.MemberShip>? MemberShip { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Person>().HasData(new Person(1, "John", "Cooper", "johnc@gmail.com", "7894562310", "IG11PT"));
            modelBuilder.Entity<MemberShip>().HasData(new MemberShip(1, MemberShipType.Primary, 10000.34, 1, false));

        }
    }
}
