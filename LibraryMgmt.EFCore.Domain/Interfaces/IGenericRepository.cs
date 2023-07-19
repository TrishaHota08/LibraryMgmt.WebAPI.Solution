using System.Linq.Expressions;

namespace LibraryMgmt.EFCore.Domain.Interfaces
{
    public interface IGenericRepository<T> where T:class
    {
        Task<IEnumerable<T>> GetAll();
        Task Add(T entity);
        void Remove(T entity);
    }
}
