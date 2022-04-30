using Rasmus_labb3_Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rasmus_labb3_API.Contexts
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {
        }

        public DbSet<Interest> Interests { get; set; }
        public DbSet<Link> Links { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<PersonInterest> PersonInterests { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //SEEDING DATA TO DB

            modelBuilder.Entity<Person>().HasData(new List<Person>()
            {
                new Person() { PersonId = 1, Name = "Rasmus Ekbom", PhoneNr = 706899532 },
                new Person() { PersonId = 2, Name = "Anitra Ngoensuwan", PhoneNr = 701234567 },
                new Person() { PersonId = 3, Name = "Fredrik Olsson", PhoneNr = 709876645 },
            });


            modelBuilder.Entity<Interest>().HasData(new List<Interest>()
            {
                new Interest() { InterestId = 1, Title = "Scuba Diving", Description = "Amazing world down there." },
                new Interest() { InterestId = 2, Title = "Golf", Description = "Compete against yourself!" },
                new Interest() { InterestId = 3, Title = "Hiking", Description = "The earth is your backyard." },
                new Interest() { InterestId = 4, Title = "Football", Description = "Gotta score em' goals." },
            });


            modelBuilder.Entity<Link>().HasData(new List<Link>()
            {
                new Link() { LinkId = 1, LinkUrl = "www.scubadiving.se", InterestId = 1},
                new Link() { LinkId = 2, LinkUrl = "www.golf.se", InterestId = 2},
                new Link() { LinkId = 3, LinkUrl = "www.hiking.se", InterestId = 3},
                new Link() { LinkId = 4, LinkUrl = "www.football.se", InterestId = 4},
            });


            modelBuilder.Entity<PersonInterest>().HasData(new List<PersonInterest>()
            {
                new PersonInterest() { PersonInterestId = 1, PersonId = 1, InterestId = 1 },
                new PersonInterest() { PersonInterestId = 2, PersonId = 2, InterestId = 2 },
                new PersonInterest() { PersonInterestId = 3, PersonId = 3, InterestId = 3 },
                new PersonInterest() { PersonInterestId = 4, PersonId = 3, InterestId = 1 },
                new PersonInterest() { PersonInterestId = 5, PersonId = 2, InterestId = 1 },
                new PersonInterest() { PersonInterestId = 6, PersonId = 1, InterestId = 3 },
                new PersonInterest() { PersonInterestId = 7, PersonId = 3, InterestId = 2 },
            });
        }
    }
}
