using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Contract
{
    public interface IGenericRepository<TEntity , TKey> where TEntity : BaseEntity<TKey>
    {
        //GetALL
        Task<IEnumerable<TEntity>> GetAllAsync();
        //GetById
        Task<TEntity> GetByIdAsync(TKey id);
        //Add
        Task AddAsync(TEntity entity);
        //Update
        void Update(TEntity entity);
        //Remove
        void Remove(TEntity entity);
    }
}
