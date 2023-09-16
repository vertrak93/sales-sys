using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Data.Repository
{
    public interface IRepository<TEntity>
    {

        IQueryable<TEntity> Get();
        TEntity Get(int id);
        void Add(TEntity obj);
        void Delete(int id);
        void Update(TEntity obj);

    }
}
