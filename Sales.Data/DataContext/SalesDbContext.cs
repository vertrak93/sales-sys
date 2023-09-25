using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Sales.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Data.DataContext
{
    public class SalesDbContext : DbContext
    {
        public DbSet<Access> Access { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<Bank> Bank { get; set; }
        public DbSet<BankAccount> BankAccount { get; set; }
        public DbSet<Brand> Brand { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Phone> Phone { get; set; }
        public DbSet<Presentation> Presentation { get; set; }
        public DbSet<PriceType> PriceType { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<RoleAccess> RoleAccess { get; set; }
        public DbSet<SubCategory> SubCategory { get; set; }
        public DbSet<Telephony> Telephony { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<UserRole> UserRole { get; set; }
        public DbSet<Vendor> Vendor { get; set; }
        public DbSet<VendorAddress> VendorAddress { get; set; }
        public DbSet<VendorBankAccount> VendorBankAccount { get; set; }
        public DbSet<VendorPhone> VendorPhone { get; set; } 


        public SalesDbContext(DbContextOptions<SalesDbContext> options)
            : base(options) 
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Product>().HasIndex(x => x.ProductId).IsUnique();

            var guidRole = Guid.NewGuid();
            modelBuilder.Entity<Role>().HasData(
                new Role() { RoleId = guidRole, RoleName = "Administrator", CreatedBy = "Admin", CreatedDate = DateTime.Now, Active = true });

            var guidUser = Guid.NewGuid();
            modelBuilder.Entity<User>().HasData(
                new User() { UserId = guidUser, FisrtName = "Admin", LastName = "Admin", Username = "Admin", Password = "39dc14dc1feac6be2702abb4e486f2bc755b0777c827457b24dae658f6266494", Email = "admin@admin", CreatedBy = "Admin", CreatedDate = DateTime.Now, Active = true });

            var guidUserRole = Guid.NewGuid();
            modelBuilder.Entity<UserRole>().HasData(
                new UserRole() { UserRoleId = guidUserRole, UserId = guidUser, RoleId = guidRole, CreatedBy = "Admin", CreatedDate = DateTime.Now, Active = true });
        }

    }
}
