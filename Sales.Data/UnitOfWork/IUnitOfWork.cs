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

        public IRepository<User> Users { get; }
        public IRepository<Role> Roles { get; }
        public IRepository<Access> Accesses { get; }
        public IRepository<UserRole> UserRoles { get; }
        public IRepository<RoleAccess> RoleAccesses { get; }

        public void Save();
    }
}
