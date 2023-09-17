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
        public string UserName { get; set; }

        private IRepository<User> _user;
        private IRepository<Role> _role;
        private IRepository<Access> _access;
        private IRepository<UserRole> _userRole;
        private IRepository<RoleAccess> _roleAccess;

        public IRepository<User> Users
        {
            get
            {
                return _user == null ? _user = new Repository<User>(_context, UserName) : _user;
            }
        }

        public IRepository<Role> Roles
        {
            get
            {
                return _role == null ? _role = new Repository<Role>(_context, UserName) : _role;
            }
        }

        public IRepository<Access> Accesses
        {
            get
            {
                return _access == null ? _access = new Repository<Access>(_context, UserName) : _access;
            }
        }

        public IRepository<UserRole> UserRoles
        {
            get
            {
                return _userRole == null ? _userRole = new Repository<UserRole>(_context, UserName) : _userRole;
            }
        }

        public IRepository<RoleAccess> RoleAccesses
        {
            get
            {
                return _roleAccess == null ? _roleAccess = new Repository<RoleAccess>(_context, UserName) : _roleAccess;
            }
        }

        public UnitOfWork(SalesDbContext context)
        {
            _context = context;
        }

        public async Task<int> Save()
        {
            return await _context.SaveChangesAsync();
        }

    }
}
