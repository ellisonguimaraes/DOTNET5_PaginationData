using System;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PagProj.Business.Interface;
using PagProj.Models;
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
        
        /// <summary> Obter estudantes de forma paginada </summary>
        /// <remarks>
        /// A quantidade de elementos por página (PageSize) não pode ultrapassar 50 
        /// </remarks>
        /// <param name="paginationParameters">Número da página (PageNumber) e quantidade de elementos por página (PageSize)</param>
        /// <returns>PaginateData</returns>
        /// <response code="200">OK - Estudantes retornados</response>
        /// <response code="400">BadRequest - Requisição do Cliente é Inválida</response>
        [HttpGet("{PageNumber}/{PageSize}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type=typeof(PagedList<Student>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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