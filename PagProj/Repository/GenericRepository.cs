using System.Data;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PagProj.Models;
using PagProj.Models.Pagination;
using PagProj.Models.Context;
using PagProj.Repository.Interface;

namespace PagProj.Repository
{
    public class GenericRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly ApplicationContext _context;
        private readonly DbSet<T> _dataset;

        public GenericRepository(ApplicationContext context)
        {
            _context = context;
            _dataset = _context.Set<T>();
        }

        public T GetById(long id) => _dataset.SingleOrDefault(i => i.Id.Equals(id));

        public PagedList<T> GetAll(PaginationParameters paginationParameters)
        {
            PagedList<T> pagedList = new PagedList<T>(
                _dataset.OrderBy(i => i.Id),
                paginationParameters.PageNumber,
                paginationParameters.PageSize
            );

            return pagedList;
        }

        public T Create(T item)
        {
            try {
                _dataset.Add(item);
                _context.SaveChanges();
                return item;

            } catch(Exception) {
                throw;
            }
        }

        public T Update(T item)
        {
            var result = _dataset.SingleOrDefault(i => i.Id.Equals(item.Id));

            if (result != null) {
                try {
                    _context.Entry(result).CurrentValues.SetValues(item);
                    _context.SaveChanges();
                    return result;

                } catch (Exception) {
                    throw;
                }
            }
            return null;
        }

        public bool Delete(long id)
        {
            var result = _dataset.SingleOrDefault(i => i.Id.Equals(id));

            if (result != null) {
                try {
                    _dataset.Remove(result);
                    _context.SaveChanges();
                    return true;

                } catch (Exception) {
                    throw;
                }
            }
            return false;
        }
    }
}