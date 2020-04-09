using System;
using System.Collections.Generic;
using System.Text;
using Foodies.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Foodies.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<CustomerModel> Customers { get; set; }
        public DbSet<RestaurantModel> Restaurants { get; set; }
        public DbSet<DislikeHistoryModel> Dislikes { get; set; }
        public DbSet<LikeHistoryModel> Likes { get; set; }
        public DbSet<AddressModel> Addresses { get; set; }
        public DbSet<CustomerLinkModel> Foodies { get; set; }
        public DbSet<CustomerFacebookLinkModel> CustomerFacebookLink { get; set; }
        public DbSet<APICalls> RegisteredApiCalls { get; set; }
        public DbSet<PhotosFromGoogle> PhotosFromGoogle { get; set; }
        public DbSet<ReviewsFromGoogle> ReviewsFromGoogle { get; set; }
        //TODO: 
        //ADD: USER REPORTS MODEL COLLECTION
        //ADD: EMPLOYEE MODEL, MODEL COLLECTION


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityRole>()
                .HasData(
                new IdentityRole
                {
                    Name = "Customer",
                    NormalizedName = "CUSTOMER"
                }
                );
            builder.Entity<IdentityRole>()
                .HasData(
                new IdentityRole 
                {
                    Name = "Employee",
                    NormalizedName = "EMPLOYEE"
                }
                );
        }
    }
}
