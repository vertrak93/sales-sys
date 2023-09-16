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

        public void Add(TEntity obj)
        {
            SetCreator(obj);
            _dbSet.Add(obj);
        }

        public void Delete(int id)
        {
            var obj = _dbSet.Find(id);
            if (obj != null)
            {
                ApplySoftDelete(obj);
                SetModifier(obj);
            }
        }

        public IQueryable<TEntity> Get()
        {
            return _dbSet.AsQueryable();
        }

        public TEntity Get(int id)
        {
            return _dbSet.Find(id);
        }

        public void Update(TEntity obj)
        {
            SetModifier(obj);
            _dbSet.Update(obj);
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
