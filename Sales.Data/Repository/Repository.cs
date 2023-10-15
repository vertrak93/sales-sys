using Microsoft.EntityFrameworkCore;
using Sales.Data.DataContext;
using Sales.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Data.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntityModel
    {
        private SalesDbContext _context;
        private DbSet<TEntity> _dbSet;

        private string _UserLogged = string.Empty;
        public string UserLogged { get { return _UserLogged; } }

        public Repository(SalesDbContext context, string UserLogged)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
            _UserLogged = UserLogged;
        }

        public async Task<TEntity> Add(TEntity obj)
        {
            await SetCreator(obj);
            var result = await _dbSet.AddAsync(obj);
            return result.Entity;
        }

        public async Task<bool> Delete(int id)
        {
            var obj = await _dbSet.FindAsync(id);

            if (obj == null)
                return true;
            
            await ApplySoftDelete(obj);
            await SetModifierAsync(obj);

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
            _dbSet.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;

            return true;
        }

        private async Task SetModifierAsync(TEntity obj)
        {
            obj.ModifiedBy = UserLogged;
            obj.ModifiedDate = DateTime.Now;
        }

        private void SetModifier(TEntity obj)
        {
            obj.ModifiedBy = UserLogged;
            obj.ModifiedDate = DateTime.Now;
        }

        private async Task SetCreator(TEntity obj)
        {
            obj.CreatedBy = UserLogged;
            obj.CreatedDate = DateTime.Now;
            obj.Active = true;
        }

        private async Task ApplySoftDelete(TEntity obj)
        {
            obj.Active = false;
        }
    }
}
