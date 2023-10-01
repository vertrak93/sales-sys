using Microsoft.EntityFrameworkCore;
using Sales.Data.DataContext;
using Sales.Data.Repository;
using Sales.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Data.UnitOfWork
{
    public interface IUnitOfWork
    {

        public string UserName { get; set; }

        public IRepository<Access> Accesses { get; }
        public IRepository<Address> Address { get; }
        public IRepository<Bank> Banks { get; }
        public IRepository<BankAccount> BankAccounts { get; }
        public IRepository<Brand> Brands { get; }
        public IRepository<Category> Categories { get; }
        public IRepository<Invoice> Invoices { get; }
        public IRepository<Phone> Phones { get; }  
        public IRepository<Presentation> Presentations { get; }    
        public IRepository<PriceType> PriceTypes { get; }
        public IRepository<Product> Products { get; }
        public IRepository<Purchase> Purchases { get; }
        public IRepository<PurchaseDetail> PurchaseDetails { get; }
        public IRepository<PurchaseType> PurchaseTypes { get; }
        public IRepository<Role> Roles { get; }
        public IRepository<RoleAccess> RoleAccesses { get; }
        public IRepository<SubCategory> SubCategories { get; }
        public IRepository<Telephony> Telephonies { get; }
        public IRepository<User> Users { get; }
        public IRepository<UserRole> UserRoles { get; }
        public IRepository<Vendor> Vendors { get; }
        public IRepository<VendorAddress> VendorAddresses { get; }
        public IRepository<VendorBankAccount> VendorBankAccounts { get; }
        public IRepository<VendorPhone> VendorPhones { get; }
        public IRepository<VendorProduct> VendorProducts { get; }

        public Task<int> SaveAsync();
        public int Save();
    }
}
