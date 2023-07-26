using Course.Core.Entities;
using Course.Core.Repositories;
using Course.Service.Dtos.StudentDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Course.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IGroupRepository _groupRepository;

        public StudentsController(IStudentRepository studentRepository, IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
            _studentRepository = studentRepository;
        }

        [HttpPost("")]
        public IActionResult Create(StudentCreateDto studentDto)
        {
            if (!_studentRepository.IsExist(x => x.Id == studentDto.GroupId))
            {
                ModelState.AddModelError("GroupId", $"Group not found by Id {studentDto.GroupId}");
                return BadRequest(ModelState);
            }
            Student student = new Student
            {
                GroupId = studentDto.GroupId,
                FullName = studentDto.FullName,
                Point = studentDto.Point,
            };
            _studentRepository.Add(student);
            _studentRepository.Commit();
            return StatusCode(201, new { Id = student.Id });
        }

        [HttpGet("{id}")]
        public ActionResult<StudentGetDto> Get(int id)
        {
            Student student = _studentRepository.Get(x => x.Id == id, "Group");
            if (student == null)
            {
                return NotFound();
            }
            StudentGetDto studentDto = new StudentGetDto
            {
                FullName = student.FullName,
                Group = new GroupInStudentGetDto
                {
                    Id = student.GroupId,
                    Name = student.Group.Name
                }
            };
            return Ok(studentDto);
        }

        [HttpGet("all")]
        public ActionResult<List<StudentGetAllItemDto>> GetAll()
        {
            var studentDtos = _studentRepository.GetQueryable(x => true, "Group").Select(x =>
            new StudentGetAllItemDto { Id = x.Id, FullName = x.FullName, GroupName = x.Group.Name }).ToList();
            return Ok(studentDtos);
        }
    }
}
