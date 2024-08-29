﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HubWally.Infrastructure.Persistence
{ 
    public interface IBaseRepository<TEntity>
    {
        Task<int> Add(TEntity entity);
        Task<int> GetRecordCount(string columnName, object columnValue);
        Task<IEnumerable<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] includes);
        Task<TEntity> Get(int id, params Expression<Func<TEntity, object>>[] includes);
        Task<TEntity> Get(string columnName, object columnValue, params Expression<Func<TEntity, object>>[] includes);
        Task<int> Update(TEntity entity);
        Task<int> Delete(int id);
    }    
}
