using Microsoft.EntityFrameworkCore;
using Sales.Data.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Data.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private SalesDbContext _context;
        private DbSet<TEntity> _dbSet;
        private string _userName;

        public string UserName { get; }

        public Repository(SalesDbContext context, string UserName)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
            _userName = UserName;
        }

        public async Task<TEntity> Add(TEntity obj)
        {
            SetCreator(obj);
            var result = await _dbSet.AddAsync(obj);
            return result.Entity;
        }

        public async Task<bool> Delete(int id)
        {
            var obj = await _dbSet.FindAsync(id);

            if (obj == null)
                return false;
            
            ApplySoftDelete(obj);
            SetModifier(obj);

            _dbSet.Entry(obj).State = EntityState.Modified;     
            
            return true;
        }

        public  IQueryable<TEntity> Get()
        {
            return _dbSet.AsQueryable();
        }

        public async Task<TEntity> Get(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public bool Update(TEntity obj)
        {
            SetModifier(obj);
            _dbSet.Update(obj);

            return true;
        }

        private void SetModifier(TEntity obj)
        {
            Type type = typeof(TEntity);

            PropertyInfo mBy = type.GetProperty("ModifiedBy");
            PropertyInfo mDt = type.GetProperty("ModifiedDate");

            mBy.SetValue(obj, _userName, null);
            mDt.SetValue(obj, DateTime.Now, null);
        }

        private void SetCreator(TEntity obj)
        {
            Type type = typeof(TEntity);

            PropertyInfo cBy = type.GetProperty("CreatedBy");
            PropertyInfo cDt = type.GetProperty("CreatedDate");

            cBy.SetValue(obj, _userName, null);
            cDt.SetValue(obj, DateTime.Now, null);
        }

        private void ApplySoftDelete(TEntity obj)
        {
            Type type = typeof(TEntity);

            PropertyInfo active = type.GetProperty("Active");
            active.SetValue(obj, false, null);
        }
    }
}
