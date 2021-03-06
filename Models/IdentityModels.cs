﻿using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace TrashCollector3.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<TrashCollector3.Models.Address> Addresses { get; set; }

        public System.Data.Entity.DbSet<TrashCollector3.Models.Charge> Charges { get; set; }

        public System.Data.Entity.DbSet<TrashCollector3.Models.Customer> Customers { get; set; }

        public System.Data.Entity.DbSet<TrashCollector3.Models.Pickup> Pickups { get; set; }

        public System.Data.Entity.DbSet<TrashCollector3.Models.CustomerAddress> CustomerAddresses { get; set; }

        public System.Data.Entity.DbSet<TrashCollector3.Models.Employee> Employees { get; set; }

        public System.Data.Entity.DbSet<TrashCollector3.Models.PickUpArea> PickUpAreas { get; set; }
    }
}