﻿using DomainLayer.Contract;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositorys
{
    public class GenericRepository<TEntity, TKey>(EventlyDbContext _dbContext) : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>

    {
        public async Task AddAsync(TEntity entity) =>await _dbContext.Set<TEntity>().AddAsync(entity);


        public async Task<IEnumerable<TEntity>> GetAllAsync() =>await _dbContext.Set<TEntity>().ToListAsync();


        public async Task<TEntity?> GetByIdAsync(TKey id) => await _dbContext.Set<TEntity>().FindAsync(id);     


        public void Remove(TEntity entity)=> _dbContext.Set<TEntity>().Remove(entity);

        public void Update(TEntity entity)=> _dbContext.Set<TEntity>().Update(entity);
    }
}
