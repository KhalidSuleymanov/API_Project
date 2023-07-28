using Course.Core.Entities;
using Course.Core.Repositories;
using Course.Service.Dtos.Common;
using Course.Service.Dtos.StudentDtos;
using Course.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Course.Api.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpPost("")]
        public IActionResult Create(StudentCreateDto studentDto)
        {
            return StatusCode(201, _studentService.Create(studentDto));
        }
        [HttpGet("{id}")]
        public ActionResult<StudentGetDto> Get(int id)
        {
            return Ok(_studentService.GetById(id));
        }
        [HttpGet("all")]
        public ActionResult<List<StudentGetAllItemDto>> GetAll()
        {
            return Ok(_studentService.GetAll());
        }
        [HttpGet("")]
        public ActionResult<PaginatedListDto<PaginatedListItemDto>> GetAll(int page = 1)
        {
            return Ok(_studentService.GetAllPaginated(page));
        }
        [HttpPut("{id}")]
        public IActionResult Edit(int id, StudentEditDto studentDto)
        {
            _studentService.Edit(id, studentDto);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _studentService.Delete(id);
            return NoContent();
        }
    }
}

