﻿using System.Threading.Tasks;

namespace MyWorkout.Core.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task AddAsync(TEntity entity);
        void Remove(TEntity entity);
    }
}
