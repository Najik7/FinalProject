using System.Collections.Generic;
using FinalProject.Context.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Context
{
    public class AppDbContext:IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions options):base(options)
        {
        }

        
        public DbSet<BodyType> BodyTypes { get; set; }
        public DbSet<Brand>Brands { get; set; }
        public DbSet<Car>Cars { get; set; }
        public DbSet<CarFuelType>CarFuelTypes { get; set; }
        public DbSet<Category>Categories { get; set; }
        public DbSet<City>Cities { get; set; }
        public DbSet<FuelType>FuelTypes { get; set; }
        public DbSet<Order>Orders { get; set; }
        public DbSet<CarCity>CarCities { get; set; }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>().ToTable("User");
            builder.Entity<IdentityRole>().ToTable("Role");

            builder.Entity<CarCity>().HasKey(x => new {x.CarId, x.CityId});
            builder.Entity<CarFuelType>().HasKey(x => new {x.CarId, x.FuelTypeId});

            builder.Entity<BodyType>().HasData(new List<BodyType>
            {
                new BodyType {Id = 1, Name = ""},
                new BodyType {Id = 2, Name = ""},
                new BodyType {Id = 3, Name = ""},
                new BodyType {Id = 4, Name = ""},
                new BodyType {Id = 5, Name = ""},
                new BodyType {Id = 6, Name = ""},
                new BodyType {Id = 7, Name = ""}
            });

            builder.Entity<FuelType>().HasData(new List<FuelType>
            {
                new FuelType{Id=1, Name=""},
                new FuelType{Id=2, Name=""},
                new FuelType{Id=3, Name=""}
            });
            
            
        }
    }
}