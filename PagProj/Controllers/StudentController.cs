using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using PagProj.Business.Interface;
using PagProj.Models;
using PagProj.Models.Context;

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

        [HttpGet]
        public IActionResult Get([FromQuery] PaginationParameters paginationParameters) 
        {
            var students = _studentBusiness.GetAll(paginationParameters);

            var metadata = new {
                students.TotalCount,
                students.PageSize,
                students.CurrentPage,
                students.HasNext,
                students.HasPrevious
            };

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(metadata));

            return Ok(students);
        }
    }
}