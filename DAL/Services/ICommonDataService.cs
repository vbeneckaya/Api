using Microsoft.EntityFrameworkCore;
using System;
using DAL.Models;

namespace DAL.Services
{
    public interface ICommonDataService
    {
        DbSet<TEntity> GetDbSet<TEntity>() where TEntity: class, IPersistable;

        TEntity GetById<TEntity>(Guid id) where TEntity : class, IPersistable;
        
        TEntity GetByIdOrNull<TEntity>(Guid? id) where TEntity : class, IPersistable;

        Guid? CreateIfNotExisted<TEntity>(string fieldName, string value) where TEntity : class, IPersistable;
        
        void Remove<TEntity>(TEntity entity) where TEntity : class, IPersistable;

        void SaveChanges();
    }
}
