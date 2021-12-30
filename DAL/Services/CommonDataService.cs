using Microsoft.EntityFrameworkCore;
using System;
using DAL.Models;

namespace DAL.Services
{
    public class CommonDataService : ICommonDataService
    {
        private readonly AppDbContext _context;

        public CommonDataService(AppDbContext context)
        {
            _context = context;
        }

        public TEntity GetById<TEntity>(Guid id) where TEntity : class, IPersistable
        {
            return GetDbSet<TEntity>().Find(id);
        }

        public TEntity GetByIdOrNull<TEntity>(Guid? id) where TEntity : class, IPersistable
        {
            return id != null ? GetById<TEntity>(id.Value) : null;
        }

        public DbSet<TEntity> GetDbSet<TEntity>() where TEntity : class, IPersistable
        {
            return _context.Set<TEntity>();
        }

        public Guid? CreateIfNotExisted<TEntity>(string fieldName, string value) where TEntity : class, IPersistable
        {
            var eType = typeof(TEntity);
            var entity = (TEntity)eType.Assembly.CreateInstance(eType.FullName);
            if (entity == null) return null;
            
            var newId = Guid.NewGuid();
            var propId = eType.GetProperty("Id");
            propId.SetValue(entity, newId);
                
            var propName = eType.GetProperty(fieldName);
            propName.SetValue(entity, value);
            _context.Set<TEntity>().Add(entity);
            _context.SaveChanges();
            return newId;
        }

        public virtual void SaveChanges()
        {
            _context.SaveChanges();
        }

        void ICommonDataService.Remove<TEntity>(TEntity entity)
        {
            this.GetDbSet<TEntity>().Remove(entity);
        }
    }
}
