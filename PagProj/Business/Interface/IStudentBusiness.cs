using PagProj.Models;
using PagProj.Models.Pagination;

namespace PagProj.Business.Interface
{
    public interface IStudentBusiness
    {
        PagedList<Student> GetAll(PaginationParameters paginationParameters);
    }
}