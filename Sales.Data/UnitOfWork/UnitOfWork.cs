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
    public class UnitOfWork : IUnitOfWork
    {
        private SalesDbContext _context;
        private string _UserLogged = string.Empty;
        public string UserLogged { get { return _UserLogged; } set { _UserLogged = value; } }

        private IRepository<Access> _access;
        private IRepository<Address> _address;
        private IRepository<Bank> _bank;
        private IRepository<BankAccount> _bankAccount;
        private IRepository<Brand> _brand;
        private IRepository<Category> _category;
        private IRepository<Invoice> _invoice;
        private IRepository<Phone> _phone;
        private IRepository<Presentation> _presentation;
        private IRepository<PriceType> _priceType;
        private IRepository<Product> _product;
        private IRepository<Purchase> _purchase;
        private IRepository<PurchaseDetail> _purchaseDetail;
        private IRepository<PurchaseType> _purchaseType;
        private IRepository<RefreshToken> _refreshToken;
        private IRepository<Role> _role;
        private IRepository<RoleAccess> _roleAccess;
        private IRepository<SubCategory> _subCategory;
        private IRepository<Telephony> _telephony;
        private IRepository<User> _user;
        private IRepository<UserRole> _userRole;
        private IRepository<Vendor> _vendor;
        private IRepository<VendorAddress> _vendorAddress;
        private IRepository<VendorBankAccount> _vendorBankAccount;
        private IRepository<VendorPhone> _vendorPhone;
        private IRepository<VendorProduct> _vendorProduct;
        private IRepository<UnitOfMeasure> _unitOfMeasure;

        public IRepository<Access> Accesses
        {
            get
            {
                return _access == null ? _access = new Repository<Access>(_context, UserLogged) : _access;
            }
        }
        public IRepository<Address> Address
        {
            get
            {
                return _address == null ? _address = new Repository<Address>(_context, UserLogged) : _address;
            }
        }
        public IRepository<Bank> Banks
        {
            get 
            {
                return _bank == null ? _bank = new Repository<Bank>(_context, UserLogged) : _bank;
            }
        }
        public IRepository<BankAccount> BankAccounts 
        {
            get
            {
                return _bankAccount == null ? _bankAccount = new Repository<BankAccount>(_context, UserLogged) : _bankAccount;
            }
        }
        public IRepository<Brand> Brands 
        {
            get
            {
                return _brand == null ? _brand = new Repository<Brand>(_context, UserLogged) : _brand;
            }
        }
        public IRepository<Category> Categories 
        {
            get
            {
                return _category == null ? _category = new Repository<Category>(_context, UserLogged) : _category;
            }
        }
        public IRepository<Invoice> Invoices 
        {
            get
            {
                return _invoice == null ? _invoice = new Repository<Invoice>(_context, UserLogged) : _invoice;
            }
        }
        public IRepository<Phone> Phones 
        {
            get
            {
                return _phone == null ? _phone = new Repository<Phone>(_context, UserLogged) : _phone;
            }
        }
        public IRepository<Presentation> Presentations 
        {
            get
            {
                return _presentation == null ? _presentation = new Repository<Presentation>(_context, UserLogged) : _presentation;
            }
        }
        public IRepository<PriceType> PriceTypes 
        {
            get
            {
                return _priceType == null ? _priceType = new Repository<PriceType>(_context, UserLogged) : _priceType;
            }
        }
        public IRepository<Product> Products 
        {
            get
            {
                return _product == null ? _product = new Repository<Product>(_context, UserLogged) : _product;
            }
        }
        public IRepository<Purchase> Purchases 
        {
            get
            {
                return _purchase == null ? _purchase = new Repository<Purchase>(_context, UserLogged) : _purchase;
            }
        }
        public IRepository<PurchaseDetail> PurchaseDetails 
        {
            get
            {
                return _purchaseDetail == null ? _purchaseDetail = new Repository<PurchaseDetail>(_context, UserLogged) : _purchaseDetail;
            }
        }
        public IRepository<PurchaseType> PurchaseTypes 
        {
            get
            {
                return _purchaseType == null ? _purchaseType = new Repository<PurchaseType>(_context, UserLogged) : _purchaseType;
            }
        }
        public IRepository<RefreshToken> RefreshTokens
        {
            get
            {
                return _refreshToken == null ? _refreshToken = new Repository<RefreshToken>(_context, UserLogged) : _refreshToken;
            }
        }
        public IRepository<Role> Roles
        {
            get
            {
                return _role == null ? _role = new Repository<Role>(_context, UserLogged) : _role;
            }
        }
        public IRepository<RoleAccess> RoleAccesses
        {
            get
            {
                return _roleAccess == null ? _roleAccess = new Repository<RoleAccess>(_context, UserLogged) : _roleAccess;
            }
        }
        public IRepository<SubCategory> SubCategories 
        {
            get
            {
                return _subCategory == null ? _subCategory = new Repository<SubCategory>(_context, UserLogged) : _subCategory;
            }
        }
        public IRepository<Telephony> Telephonies 
        {
            get
            {
                return _telephony == null ? _telephony = new Repository<Telephony>(_context, UserLogged) : _telephony;
            }
        }
        public IRepository<UnitOfMeasure> UnitOfMeasures
        {
            get
            {
                return _unitOfMeasure == null ? _unitOfMeasure = new Repository<UnitOfMeasure>(_context, UserLogged) : _unitOfMeasure;
            }
        }
        public IRepository<User> Users
        {
            get
            {
                return _user == null ? _user = new Repository<User>(_context, UserLogged) : _user;
            }
        }
        public IRepository<UserRole> UserRoles
        {
            get
            {
                return _userRole == null ? _userRole = new Repository<UserRole>(_context, UserLogged) : _userRole;
            }
        }
        public IRepository<Vendor> Vendors 
        {
            get
            {
                return _vendor == null ? _vendor = new Repository<Vendor>(_context, UserLogged) : _vendor;
            }
        }
        public IRepository<VendorAddress> VendorAddresses 
        {
            get
            {
                return _vendorAddress == null ? _vendorAddress = new Repository<VendorAddress>(_context, UserLogged) : _vendorAddress;
            }
        }
        public IRepository<VendorBankAccount> VendorBankAccounts 
        {
            get
            {
                return _vendorBankAccount == null ? _vendorBankAccount = new Repository<VendorBankAccount>(_context, UserLogged) : _vendorBankAccount;
            }
        }
        public IRepository<VendorPhone> VendorPhones
        {
            get
            {
                return _vendorPhone == null ? _vendorPhone = new Repository<VendorPhone>(_context, UserLogged) : _vendorPhone;
            }
        }
        public IRepository<VendorProduct> VendorProducts 
        {
            get
            {
                return _vendorProduct == null ? _vendorProduct = new Repository<VendorProduct>(_context, UserLogged) : _vendorProduct;
            }
        }

        public UnitOfWork(SalesDbContext context)
        {
            _context = context;
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
