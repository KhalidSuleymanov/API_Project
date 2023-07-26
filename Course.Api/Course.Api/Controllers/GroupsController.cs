using Course.Api.Dtos.GroupDtos;
using Course.Core.Entities;
using Course.Core.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Course.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly IGroupRepository _groupRepository;

        public GroupsController(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        [HttpGet("all")]
        public ActionResult<List<GroupGetAllItemDto>> GetAll()
        {
            var groupDtos = _groupRepository.GetQueryable(x => x.Students.Count > 0).Select(x => new GroupGetAllItemDto { Id = x.Id, Name = x.Name, }).ToList();
            return Ok(groupDtos);
        }

        [HttpGet("{id}")]
        public ActionResult<GroupGetDto> Get(int id)
        {
            Group group = _groupRepository.Get(b => b.Id == id);
            if (group == null)
            {
                return NotFound();
            }
            GroupGetDto groupDto = new GroupGetDto
            {
                Name = group.Name,
            };
            return Ok(groupDto);
        }

        [HttpPost("")]
        public IActionResult Create(GroupCreateDto groupDto)
        {
            if (_groupRepository.IsExist(x => x.Name == groupDto.Name))
            {
                ModelState.AddModelError("Name", "Name is already taken");
                return BadRequest(ModelState);
            }
            Group group = new Group
            {
                Name = groupDto.Name,
            };
            _groupRepository.Add(group);
            _groupRepository.Commit();
            return StatusCode(201, new { Id = group.Id });
        }

        [HttpPut("{id}")]
        public IActionResult Edit(int id, GroupEditDto groupDto)
        {
            Group group = _groupRepository.Get(x => x.Id == id);
            if (group == null)
            {
                return NotFound();
            }
            if (group.Name != groupDto.Name && _groupRepository.IsExist(x => x.Name == groupDto.Name))
            {
                ModelState.AddModelError("Name", "Name is already taken");
                return BadRequest(ModelState);
            }
            group.Name = groupDto.Name;
            _groupRepository.Commit();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Group group = _groupRepository.Get(b => b.Id == id);
            if (group == null)
            {
                return NotFound();
            }
            _groupRepository.Remove(group);
            _groupRepository.Commit();
            return NoContent();
        }
    }
}
