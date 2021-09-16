using PagProj.Business.Interface;
using PagProj.Models;
using PagProj.Models.Pagination;
using PagProj.Repository.Interface;

namespace PagProj.Business
{
    public class StudentBusiness : IStudentBusiness
    {
        private readonly IRepository<Student> _repository;
        
        public StudentBusiness(IRepository<Student> repository)
        {
            _repository = repository;
        }

        public PagedList<Student> GetAll(PaginationParameters paginationParameters)
        {
            return _repository.GetAll(paginationParameters);
        }
    }
}