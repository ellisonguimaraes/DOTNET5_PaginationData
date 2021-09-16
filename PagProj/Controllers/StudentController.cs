using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using PagProj.Business.Interface;
using PagProj.Models.Pagination;

namespace PagProj.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentBusiness _studentBusiness;

        public StudentController(IStudentBusiness studentBusiness)
        {
            _studentBusiness = studentBusiness;
        }

        [HttpGet("{PageNumber}/{PageSize}")]
        public IActionResult Get([FromRoute] PaginationParameters paginationParameters) 
        {
            var students = _studentBusiness.GetAll(paginationParameters);

            var metadata = new {
                students.TotalCount,
                students.PageSize,
                students.CurrentPage,
                students.HasNext,
                students.HasPrevious,
                students.TotalPages
            };

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(metadata));

            return Ok(students);
        }
    }
}