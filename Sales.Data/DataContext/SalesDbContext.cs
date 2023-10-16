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
        public DbSet<Invoice> Invoice { get; set; }
        public DbSet<Phone> Phone { get; set; }
        public DbSet<Presentation> Presentation { get; set; }
        public DbSet<PriceType> PriceType { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Purchase> Purchase { get; set; }
        public DbSet<PurchaseDetail> PurchaseDetail { get; set; }
        public DbSet<PurchaseType> PurchaseType { get; set; }
        public DbSet<RefreshToken> RefreshToken { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<RoleAccess> RoleAccess { get; set; }
        public DbSet<SubCategory> SubCategory { get; set; }
        public DbSet<Telephony> Telephony { get; set; }
        public DbSet<UnitOfMeasure> UnitOfMeasure { get; set; } 
        public DbSet<User> User { get; set; }
        public DbSet<UserRole> UserRole { get; set; }
        public DbSet<Vendor> Vendor { get; set; }
        public DbSet<VendorAddress> VendorAddress { get; set; }
        public DbSet<VendorBankAccount> VendorBankAccount { get; set; }
        public DbSet<VendorPhone> VendorPhone { get; set; } 
        public DbSet<VendorProduct> VendorProduct { get; set; } 

        public SalesDbContext(DbContextOptions<SalesDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasIndex(x => x.SKU).IsUnique();
            modelBuilder.Entity<User>().HasIndex(x => x.Username).IsUnique();
            modelBuilder.Entity<User>().HasIndex(x => x.Email).IsUnique();

            modelBuilder.Entity<Role>().HasData(
                new Role() { RoleId = -1, RoleName = "Administrator", CreatedBy = "Admin", CreatedDate = new DateTime(2000, 1, 1, 0, 0, 0), Active = true },
                new Role() { RoleId = 1, RoleName = "Seller", CreatedBy = "Admin", CreatedDate = new DateTime(2000, 1, 1, 0, 0, 0), Active = true },
                new Role() { RoleId = 2, RoleName = "Buyer", CreatedBy = "Admin", CreatedDate = new DateTime(2000, 1, 1, 0, 0, 0), Active = true });
            
            modelBuilder.Entity<User>().HasData(
                new User() { UserId = -1, FisrtName = "Admin", LastName = "Admin", Username = "Admin", Password = "39dc14dc1feac6be2702abb4e486f2bc755b0777c827457b24dae658f6266494", Email = "admin@admin", CreatedBy = "Admin", CreatedDate = new DateTime(2000, 1, 1, 0, 0, 0), Active = true });
            
            modelBuilder.Entity<UserRole>().HasData(
                new UserRole() { UserRoleId = -1, UserId = -1, RoleId = -1, CreatedBy = "Admin", CreatedDate = new DateTime(2000, 1, 1, 0, 0, 0), Active = true });
        }

    }
}
