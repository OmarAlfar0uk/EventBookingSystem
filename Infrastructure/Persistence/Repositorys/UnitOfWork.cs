using DomainLayer.Contract;
using DomainLayer.Models;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositorys
{
    public class UnitOfWork(EventlyDbContext _dbContext) : IUnitOfWork
    {
        private readonly Dictionary<string, object> _repository = [];
        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            var typeName = typeof(TEntity).Name;
            //if(_repository.ContainsKey(typeName))
            //    return (IGenericRepository<TEntity, TKey>) _repository[typeName];

            if(_repository.TryGetValue(typeName , out object? value ))
                return (IGenericRepository<TEntity, TKey>) value;
            else
            {
                //Create Obj
                var Repo = new GenericRepository<TEntity, TKey>(_dbContext);    
                //Store Object in Dictionry
                _repository.Add(typeName, Repo);
                //Return Obj
                return Repo;
            }
        }

        public async Task<int> SaveChangesAsync() =>await _dbContext.SaveChangesAsync();
    }
}
