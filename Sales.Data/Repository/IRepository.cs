using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Data.Repository
{
    public interface IRepository<TEntity>
    {
        public string UserLogged { get; }
        public IQueryable<TEntity> Get();
        public Task<TEntity> Get(int id);
        public Task<TEntity> Add(TEntity obj);
        public Task<bool> Delete(int id);
        public Task<bool> Activate(int id);
        public bool Update(TEntity obj);

    }
}
