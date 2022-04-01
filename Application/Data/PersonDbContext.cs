using Application.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Data
{
    public class PersonDbContext : DbContext
    {
        public PersonDbContext(DbContextOptions<PersonDbContext> options) : base(options) { }

        public DbSet<Person> Persons { get; set; }

        public DbSet<Countries> Country { get; set; }

        public DbSet<Friend> Friends { get; set; }

        public DbSet<Friendship> Friendships { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           
            optionsBuilder.UseSqlServer("Server=tcp:vikserver.database.windows.net,1433;Initial Catalog=vikdatabase;Persist Security Info=False;User ID=vikmcr;Password=*Fluzudo12;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
           
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {


            builder.Entity<Friendship>()
                .HasOne(p => p.Person)
                .WithMany(f => f.Friendship_)
                .HasForeignKey(pi => pi.Id);

            builder.Entity<Friendship>()
                .HasOne(fr => fr.Friend)
                .WithMany(f => f.Friendship)
                .HasForeignKey(fi => fi.FriendId);

            base.OnModelCreating(builder);
        }


        
    }
}
