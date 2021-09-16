using System.Collections.Generic;
using PagProj.Models;

namespace PagProj.Repository.Interface
{
    public interface IRepository<T> where T : BaseEntity
    {
        PagedList<T> GetAll(PaginationParameters paginationParameters);
        T GetById(long id);
        T Create(T item);
        T Update(T item);
        bool Delete(long id);
    }
}