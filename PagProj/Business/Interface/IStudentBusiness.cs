using PagProj.Models;

namespace PagProj.Business.Interface
{
    public interface IStudentBusiness
    {
        PagedList<Student> GetAll(PaginationParameters paginationParameters);
    }
}