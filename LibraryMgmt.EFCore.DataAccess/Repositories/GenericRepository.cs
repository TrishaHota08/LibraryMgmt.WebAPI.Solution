using LibraryMgmt.EFCore.DataAccess.LibDbContext;
using LibraryMgmt.EFCore.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LibraryMgmt.EFCore.Domain.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly LibraryDbContext _libraryDbContext;
        public GenericRepository(LibraryDbContext libDbContext)
        {
            _libraryDbContext = libDbContext;
        }

        public async Task Add(T entity)
        {
            await _libraryDbContext.Set<T>().AddAsync(entity);
        }

        public async virtual Task<IEnumerable<T>> GetAll()
        {
            return await _libraryDbContext.Set<T>().ToListAsync();
        }


    public  virtual void Remove(T entity)
    {
         _libraryDbContext.Set<T>().Remove(entity);
    }
}
}
