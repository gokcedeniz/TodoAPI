using Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IRepository<T> where T : TBase
    {
        Task<List<T>> GetAllAsync(); 
        Task<T> GetByIdAsync(Guid id); 
        Task AddAsync(T item); 
        Task UpdateAsync(T item); 
        Task DeleteAsync(Guid id);
        
    }
}
