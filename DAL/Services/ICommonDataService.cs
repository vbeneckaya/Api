using Microsoft.EntityFrameworkCore;
using System;
using DAL.Models;

namespace DAL.Services
{
    public interface ICommonDataService
    {
        DbSet<TEntity> GetDbSet<TEntity>() where TEntity: class;

        TEntity GetById<TEntity>(Guid id) where TEntity : class;
        
        TEntity GetByIdOrNull<TEntity>(Guid? id) where TEntity : class;

        Guid? CreateIfNotExisted<TEntity>(string fieldName, string value) where TEntity : class;
        
        void Remove<TEntity>(TEntity entity) where TEntity : class;

        void SaveChanges();
    }
}
